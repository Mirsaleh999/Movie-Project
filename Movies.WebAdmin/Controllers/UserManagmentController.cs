using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.DbModel;
using Movies.WebAdmin.ViewModels;
using System.Text;

/// <summary>
/// A controller for managing users and roles in the Movies.WebAdmin application.
/// </summary>
namespace Movies.WebAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagmentController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        
        /// <summary>
        /// Initializes a new instance of the UserManagmentController class.
        /// </summary>
        /// <param name="userManager">An instance of UserManager for managing users.</param>
        /// <param name="roleManager">An instance of RoleManager for managing roles.</param>
        /// <param name="webHostEnvironment">An instance of IWebHostEnvironment for managing the hosting environment.</param>
        public UserManagmentController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }

        
        #region UserOperation
        /// <summary>
        /// Displays a list of users.
        /// </summary>
        /// <returns>A view with a list of users.</returns>
        public IActionResult UserIndex()
        {

            List<UserViewModel> viewModels = new List<UserViewModel>();

            List<AppUser> appUsers = _userManager.Users.ToList();
            foreach (var item in appUsers)
            {
                UserViewModel viewModel = new UserViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Country = item.Country,
                    Img = item.Img,
                    DateOfBirth = item.DateOfBirth,
                    Email = item.Email,
                    UserName = item.UserName,
                    Gender = item.Gender,

                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        /// <summary>
        /// Displays the form for creating a new user.
        /// </summary>
        /// <returns>A view with the form for creating a new user.</returns>
        public IActionResult UserCreate()
        {

            return View();

        }

        /// <summary>
        /// Handles the submission of the form for creating a new user.
        /// </summary>
        /// <param name="viewModel">A UserViewModel containing the data for the new user.</param>
        /// <param name="imageFile">An IFormFile containing the user's profile picture.</param>
        /// <returns>A redirection to the user list on success, the form view with errors otherwise.</returns>
        [HttpPost]
        public async Task<IActionResult> UserCreate(UserViewModel viewModel, IFormFile imageFile)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Img");
            ModelState.Remove("DateOfBirth");
            if (ModelState.IsValid)
            {

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

                AppUser user = new AppUser()
                {
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    UserName = viewModel.UserName,
                    Country = viewModel.Country,
                    Img = viewModel.Img,
                    DateOfBirth = viewModel.DateOfBirth,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender
                };
                IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "User added successfully. ";
                    return RedirectToAction("UserIndex");
                }

            }
            return View(viewModel);

        }

        /// <summary>
        /// Displays the form for updating an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns>A view with the form for updating the user, NotFound if the user is not found.</returns>
        public async Task<IActionResult> UserUpdate(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Country = user.Country,
                Img = user.Img,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                UserName = user.UserName,
                Gender = user.Gender
            };

            return View(viewModel);
        }

        /// <summary>
        /// Handles the submission of the form for updating an existing user.
        /// </summary>
        /// <param name="viewModel">A UserViewModel containing the updated data for the user.</param>
        /// <param name="imageFile">An IFormFile containing the updated profile picture for the user.</param>
        /// <returns>A redirection to the user list on success, the form view with errors otherwise.</returns>
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserViewModel viewModel, IFormFile imageFile)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Img");
            ModelState.Remove("DateOfBirth");
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
                //}

                AppUser user = await _userManager.FindByIdAsync(viewModel.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Name = viewModel.Name;
                user.Surname = viewModel.Surname;
                user.Country = viewModel.Country;
                user.Img = viewModel.Img;
                user.DateOfBirth = viewModel.DateOfBirth;
                user.Email = viewModel.Email;
                user.UserName = viewModel.UserName;
                user.Gender = viewModel.Gender;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["success"] = "User have been successfully changed.";
                    return RedirectToAction("UserIndex");
                }
            }

            return View(viewModel);
        }

        /// <summary>
        /// Handles the request to delete a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A redirection to the user list on success, NotFound if the user is not found.</returns>
        public async Task<IActionResult> UserDelete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["success"] = "User have been successfully deleted.";
                    return RedirectToAction("UserIndex");
                }
            }
            return RedirectToAction("UserIndex");
        }

        #endregion

        #region RoleOperation

        /// <summary>
        /// Retrieves the roles of a user.
        /// </summary>
        /// <param name="id">The ID of the user whose roles are to be retrieved.</param>
        /// <returns>A string containing the roles of the user separated by semicolons.</returns>
        public async Task<string> UserRole(string id)
        {

            AppUser user = await _userManager.FindByIdAsync(id);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            StringBuilder builder = new StringBuilder();
            foreach (var item in roles)
            {
                builder.Append(item + "; ");
            }
            return builder.ToString();
        }


        /// <summary>
        /// Displays a list of roles.
        /// </summary>
        /// <returns>A view with a list of roles.</returns>
        public IActionResult RoleIndex()
        {

            List<RoleViewModel> viewModels = new List<RoleViewModel>();

            List<AppRole> appRoles = _roleManager.Roles.ToList();
            foreach (var item in appRoles)
            {
                RoleViewModel viewModel = new RoleViewModel
                {
                    Id = item.Id,
                    Name = item.Name

                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        /// <summary>
        /// Displays the form for creating a new role.
        /// </summary>
        /// <returns>A view with the form for creating a new role.</returns>
        public IActionResult RoleCreate()
        {

            return View();

        }

        /// <summary>
        /// Handles the submission of the form for creating a new role.
        /// </summary>
        /// <param name="viewModel">A RoleViewModel containing the data for the new role.</param>
        /// <returns>A redirection to the role list on success, the form view with errors otherwise.</returns>
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{
            AppRole role = new AppRole()
            {
                Name = viewModel.Name
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["success"] = "Role added successfully. ";
                return RedirectToAction("RoleIndex");
            }

            //}
            return View(viewModel);

        }

        /// <summary>
        /// Displays the form for assigning a role to a user.
        /// </summary>
        /// <param name="Id">The ID of the user to whom a role is to be assigned.</param>
        /// <returns>A view with the form for assigning a role to the user.</returns>
        public async Task<IActionResult> RoleAssign(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);

            UserRoleViewModel viewModel = new UserRoleViewModel()
            {
                UserFullName = user.Name + " " + user.Surname,
                UserId = user.Id
            };
            List<AppRole> roles = _roleManager.Roles.ToList();

            List<RoleViewModel> roleViewModels = new List<RoleViewModel>();
            foreach (var item in roles)
            {
                RoleViewModel roleView = new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                roleViewModels.Add(roleView);
            }
            viewModel.Roles = roleViewModels;
            return View(viewModel);

        }

        /// <summary>
        /// Handles the submission of the form for assigning a role to a user.
        /// </summary>
        /// <param name="viewModel">A UserRoleViewModel containing the user ID and role name to assign.</param>
        /// <returns>A redirection to the user list on success, the form view with errors otherwise.</returns>
        [HttpPost]
        public async Task<IActionResult> RoleAssign(UserRoleViewModel viewModel)
        {
            AppUser user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user != null)
            {
                IdentityResult result = await _userManager.AddToRoleAsync(user, viewModel.RoleName);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserIndex");
                }
            }


            return View(viewModel);

        }

        /// <summary>
        /// Displays the form for updating an existing role.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <returns>A view with the form for updating the role, NotFound if the role is not found.</returns>
        public async Task<IActionResult> RoleUpdate(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel viewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewModel);
        }

        /// <summary>
        /// Handles the submission of the form for updating an existing role.
        /// </summary>
        /// <param name="viewModel">A RoleViewModel containing the updated data for the role.</param>
        /// <returns>A redirection to the role list on success, the form view with errors otherwise.</returns>
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            AppRole role = await _roleManager.FindByIdAsync(viewModel.Id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = viewModel.Name;
            IdentityResult result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                TempData["success"] = "Role have been successfully changed.";
                return RedirectToAction("RoleIndex");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Handles the request to delete a role.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <returns>A redirection to the role list on success, NotFound if the role is not found.</returns>
        [HttpGet]
        public async Task<IActionResult> RoleDelete(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["success"] = "Role have been successfully deleted.";
                return RedirectToAction("RoleIndex");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        #endregion
    }

}
