using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmnivusMvcWebsite.Models;
using OmnivusMvcWebsite.Models.ViewModels;
using OmnivusMvcWebsite.Services;

namespace OmnivusMvcWebsite.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProfileManager _profileManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IProfileManager profileManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _profileManager = profileManager;
        }

        #region SignUp

        [Route("SignUp")]
        [HttpGet]
        public IActionResult SignUp(string returnUrl = null)
        {

            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var form = new SignUpForm();

            if (returnUrl != null)
                form.ReturnUrl = returnUrl;

            return View(form);
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm formModel)
        {
            if (ModelState.IsValid)
            {
                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (!await _userManager.Users.AnyAsync())
                    formModel.RoleName = "Admin";

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == formModel.Email);
                if (user == null)
                {
                    user = new IdentityUser()
                    {
                        Email = formModel.Email,
                        UserName = formModel.Email
                    };

                    var userRes = await _userManager.CreateAsync(user, formModel.Password);

                    if (userRes.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, formModel.RoleName);

                        var profile = new UserProfile
                        {
                            FirstName = formModel.FirstName,
                            LastName = formModel.LastName,
                            Email = formModel.Email,
                            StreetName = formModel.StreetName,
                            PostalCode = formModel.PostalCode,
                            City = formModel.City,
                            Country = formModel.Country
                        };

                        var profileRes = await _profileManager.CreateAsync(user, profile);
                        if (profileRes.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            if (formModel.ReturnUrl == null || formModel.ReturnUrl == "/")
                                return RedirectToAction("Index", "Home");

                            else
                                return LocalRedirect(formModel.ReturnUrl);
                        }
                        else
                            return RedirectToAction("auth", "SignUp");
                    }
             
                }

                else
                    return RedirectToAction("Index", "Home");
                
            }
            
            return View();
        }
        #endregion

        #region SignIn

        [Route("SignIn")]
        public IActionResult SignIn(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var form = new SignInForm();

            if (returnUrl != null)
                form.ReturnUrl = returnUrl;

            return View(form);
        }

        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm formModel)
        {
            if (ModelState.IsValid)
            {
                var userRes = await _signInManager.PasswordSignInAsync(formModel.Email, formModel.Password, isPersistent: false, false);

                if (userRes.Succeeded)
                {
                    if (formModel.ReturnUrl == null || formModel.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");

                    else
                        return LocalRedirect(formModel.ReturnUrl);
                }
            }

            formModel.ErrorMessage = "Password or email is incorrect";
            formModel.Password = "";

            return View(formModel);
        }
        #endregion

        #region SignOut

        [Route("signout")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}

