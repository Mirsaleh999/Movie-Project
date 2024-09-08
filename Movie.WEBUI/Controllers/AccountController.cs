using Abp.Runtime.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.WEBUI.ViewModels;
using Movie.WEBUI.wwwroot.ViewModels;
using Movies.DAL.DbModel;
using System.Security.Claims;

namespace Movie.WEBUI.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(HomeViewModel homeViewModel, SignInViewModel signInViewModel)
        {
            signInViewModel = homeViewModel.SignInViewModel;
            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            var appUser = await _userManager.FindByNameAsync(signInViewModel.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(homeViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, signInViewModel.Password, signInViewModel.RememberMe, false);

            if (result.Succeeded)
            {
                string? redirect = Request.Query["returnUrl"];
                if (string.IsNullOrWhiteSpace(redirect))
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                
            }
            return RedirectToAction("Index", "Home", homeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(HomeViewModel homeViewModel, SignUpViewModel model)
        {
            model = homeViewModel.SignUpViewModel;

            //if (ModelState.IsValid)
            //{
            AppUser user = new AppUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {


                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            //}

            return View(homeViewModel);
        }

        public async Task<IActionResult> ProfileSettings(string id)
        {

            AppUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            ProfileViewModel viewModel = new ProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Country = user.Country,
                Img = user.Img,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                UserName = user.UserName,
                Gender = user.Gender,
            };

            HomeViewModel homeViewModel = new HomeViewModel
            {
                ProfileViewModel = viewModel
            };
            return View(homeViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> ProfileSettings(HomeViewModel homeViewModel, ProfileViewModel viewModel, IFormFile imageFile)
        {
            viewModel = homeViewModel.ProfileViewModel;
            //if (ModelState.IsValid)
            //{
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    viewModel.Img = imagePath;
                }
            }


            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Update user properties
                user.Name = viewModel.Name;
                user.Surname = viewModel.Surname;
                user.Country = viewModel.Country;
                user.Img = viewModel.Img;
                user.DateOfBirth = viewModel.DateOfBirth;
                user.Email = viewModel.Email;
                user.UserName = viewModel.UserName;
                user.Gender = viewModel.Gender;



                if (!string.IsNullOrEmpty(viewModel.NewPassword))
                {
                    // Check if the current password is correct
                    if (await _userManager.CheckPasswordAsync(user, viewModel.Password))
                    {
                        // Update the password
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, viewModel.NewPassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(viewModel);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect current password.");
                        return View(viewModel);
                    }
                }

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }
            //}

            return View(homeViewModel);
        }




        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
