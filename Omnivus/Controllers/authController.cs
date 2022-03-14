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

                    await _userManager.AddToRoleAsync(user, formModel.RoleName);

                }

            }

            return View();
        }
        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInForm modelForm)
        {
            return View();
        }
        #endregion

        #region SignOut
        public IActionResult SignOut()
        {
            return View();
        }
        #endregion
    }
}
