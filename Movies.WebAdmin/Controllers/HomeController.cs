using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.DbModel;
using Movies.WebAdmin.Models;
using Movies.WebAdmin.ViewModels;
using System.Diagnostics;

namespace Movies.WebAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        /// <param name="logger">The logger for HomeController.</param>
        /// <param name="userManager">The user manager for authentication.</param>
        /// <param name="signInManager">The sign-in manager for authentication.</param>
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The home page view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <param name="ReturnUrl">The URL to return to after successful login.</param>
        /// <returns>The login view.</returns>
        public IActionResult LogIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        /// <summary>
        /// Handles the login POST request.
        /// </summary>
        /// <param name="loginViewModel">The login data from the view model.</param>
        /// <returns>A redirection to the specified return URL or the home page on success, or the login view with errors on failure.</returns>
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız bir müddətdir ki, kilidlənib. Zəhmət olmasa bir az sonra yenə cəhd edin.");
                    }
                    else
                    {
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                        if (result.Succeeded)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            if (TempData["ReturnUrl"] != null)
                            {
                                return Redirect(TempData["ReturnUrl"].ToString());
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                            await _userManager.AccessFailedAsync(user);

                            int fail = await _userManager.GetAccessFailedCountAsync(user);

                            ModelState.AddModelError("", $"{fail} uğursuz giriş");
                            if (fail == 3)
                            {
                                await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
                                ModelState.AddModelError("", "Hesabınız 3 uğursuz girişə görə 20 dəqiqə kilidlənib. Zəhmət olmasa bir az sonra yenə cəhd edin.");
                            }
                            else
                            {
                                ModelState.AddModelError("", "E-poçt ünvanınız və ya parolunuz səhvdir.");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bu e-poçt ünvanı üçün qeydiyyatdan keçmiş istifadəçi tapılmadı.");
                }

            }
            return View(loginViewModel);
        }

        /// <summary>
        /// Displays the access denied page.
        /// </summary>
        /// <returns>The access denied view.</returns>
        public IActionResult AccessDenied()
        {

            return View();
        }

        /// <summary>
        /// Logs out the user.
        /// </summary>
        /// <returns>A redirection to the home page.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Displays the privacy page.
        /// </summary>
        /// <returns>The privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The error view with error details.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}