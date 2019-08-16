using System.Security.Claims;
using System.Threading.Tasks;
using Blog161.Models;
using Blog161.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog161.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Senha, loginModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Faild to Login");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Nome = registerModel.Nome,
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };

                var result = await _userManager.CreateAsync(user, registerModel.Senha);
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(registerModel.Grupo);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registerModel.Grupo));
                    }

                    if (!await _userManager.IsInRoleAsync(user, registerModel.Grupo))
                    {
                        await _userManager.AddToRoleAsync(user, registerModel.Grupo);
                    }

                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        Claim claim = new Claim(ClaimTypes.Email, user.Email);
                        await _userManager.AddClaimAsync(user, claim);
                    }

                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.Username, registerModel.Senha, registerModel.RememberMe, false);
                    if (resultSignIn.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}