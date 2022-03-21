using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omnivus.Helpers;
using Omnivus.Models;
using Omnivus.Models.Data;

namespace Omnivus.Controllers
{
    public class authController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAddressManager _addressManager;

        public authController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IAddressManager addressManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addressManager = addressManager;
        }

        #region SignUp

        public IActionResult SignUp(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var form = new SignUpForm();

            if (returnUrl != null)
                form.ReturnUrl = returnUrl;


            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm formModel)
        {
            if (ModelState.IsValid)
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                
                if (!_userManager.Users.Any())
                    formModel.RoleName = "admin";

                var user = new AppUser()
                {
                    FirstName = formModel.FirstName,
                    LastName = formModel.LastName,
                    Email = formModel.Email,

                    UserName = formModel.Email
                };

                var userRes = await _userManager.CreateAsync(user, formModel.Password);

                if (userRes.Succeeded)
                {
                    var address = new AppAddress(formModel.StreetName, formModel.PostalCode, formModel.City);

                    await _addressManager.CreateUserAddressAsync(user, address);
                    await _userManager.AddToRoleAsync(user, formModel.RoleName);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (formModel.ReturnUrl == null ||formModel.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");

                    else
                        return LocalRedirect(formModel.ReturnUrl);

                }

                foreach(var error in userRes.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }

            }

            return View();
        }
        #endregion

        #region SignIn

        public IActionResult SignIn(string returnUrl = null)
        {
            if(_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var form = new SignInForm();

            if (returnUrl != null)
                form.ReturnUrl = returnUrl;

            else
                form.ReturnUrl = "/";

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm modelForm)
        {
            if (ModelState.IsValid)
            {
                var userRes = await _signInManager.PasswordSignInAsync(modelForm.Email, modelForm.Password, isPersistent: false, false);

                if (userRes.Succeeded)
                {
                    if (modelForm.ReturnUrl == null || modelForm.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    
                    else
                        return LocalRedirect(modelForm.ReturnUrl);
                }               
            }

            
            ModelState.AddModelError(String.Empty, "Epostadressen eller lösenordet är felaktigt");
            modelForm.Password = "";

            return View(modelForm);
        }
        #endregion

        #region SignOut
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
