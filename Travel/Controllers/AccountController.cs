using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModels.Account;
using System.Threading.Tasks;

namespace Travel.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }
            AppUser user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(request);
            }
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url = Url.Action("ConfirmEmail", "Account", new {userId =user.Id, token}, Request.Scheme, Request.Host.ToString());

            string html = string.Empty;

            using(StreamReader  sr = new StreamReader("wwwroot/templates/Emailpage.html"))
            {
                html = sr.ReadToEnd();
            }

            html = html.Replace("{link}", url);

            _emailService.Send(user.Email, "Email confirmation", html);

            TempData["RegistrationSuccess"] = "true";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser existUser = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(existUser, token);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var user = await _userManager.FindByEmailAsync(request.EmailOrUsername);
            if(user == null)
            {
                user = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong!");
                return View(request);
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong!");
                return View(request);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "You must confirm your email to log in.");
                return View(request);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(EmailVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email was not found!");
                return View();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action("ChangePassWord", "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());
            string html = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/templates/PasswordReset.html"))
            {
                html = reader.ReadToEnd();
            }
            html = html.Replace("{link}", url);
            _emailService.Send(model.Email, "Change password", html);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassWord(string UserId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);
            if(user == null)
            {
                return null;
            }
            ForgetVM forgetVM = new ForgetVM()
            {
                UserId = UserId,
                Token = token
            };
            return View(forgetVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassWord(ForgetVM model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser user = await _userManager.FindByIdAsync(model.UserId);
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.PassWord);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
