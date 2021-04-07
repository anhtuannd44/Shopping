using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Web.DataMapper;
using ShoppingProject.Web.Models.Account;
using System.Threading.Tasks;

namespace ShoppingProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Mapper _mapper;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = new Mapper();
        }

        [Route("Login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            if (_signInManager.IsSignedIn(User))
                return Redirect(ReturnUrl);
            ReturnUrl = ReturnUrl.Replace("%2F", "/");
            var model = new AccountLoginModel()
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<IActionResult> Login(AccountLoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(login.ReturnUrl);
                }
            }
            ModelState.AddModelError("IncorrectInput", "Tên đăng nhập hoặc mật khẩu không chính xác");
            return View(login);
        }

        public IActionResult Register(string returnUrl = "/")
        {
            returnUrl = returnUrl.Replace("%2F", "/");

            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(returnUrl);
            }

            var model = new AccountRegisterModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterModel register)
        {
            var u = await _userManager.FindByEmailAsync(register.Email ?? "");
            if (u != null)
            {
                ModelState.AddModelError("Email", "Email is already taken");
            }
            else if (ModelState.IsValid)
            {
                var user = _mapper.AccountRegisterModelToApplicationUser(register);
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var loginModel = new AccountLoginModel
                    {
                        Email = register.Email,
                        Password = register.Password,
                    };
                    await _userManager.AddToRoleAsync(user, "Customer");
                    if (!_signInManager.IsSignedIn(User))
                    {
                        await _signInManager.PasswordSignInAsync(user, register.Password, false, false);
                    }
                    if (!string.IsNullOrEmpty(register.ReturnUrl))
                    {
                        return Redirect(register.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(register);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}