using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System.Data;

namespace Movies.WebAdmin.Controllers
{
    [Authorize(Roles = "Operator")]
    /// <summary>
    /// Controller for handling about-related actions.
    /// </summary>
    public class AboutController : Controller
    {
        private readonly IGenericService<AboutDto, About> _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutController"/> class.
        /// </summary>
        /// <param name="service">The about service.</param>
        /// <param name="webHostEnvironment">The web host environment.</param>
        public AboutController(IGenericService<AboutDto, About> service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Displays a list of about entries.
        /// </summary>
        /// <returns>A view containing the about list.</returns>
        public async Task<IActionResult> Index()
        {
            var about = await _service.GetListAsync();
            return View(about);
        }

        /// <summary>
        /// Displays the create about view.
        /// </summary>
        /// <returns>A view for creating a new about entry.</returns>
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new about entry.
        /// </summary>
        /// <param name="aboutDto">The about data to create.</param>
        /// <param name="imageFile">The image file to be uploaded.</param>
        /// <returns>A redirection to the Index action on success, or the view with validation errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AboutDto aboutDto, IFormFile imageFile)
        {
            ModelState.Remove("Img");
            if (ModelState.IsValid)
            {

                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = _imgPath + imageFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                        aboutDto.Img = imagePath;
                    }
                }

                var about = await _service.AddAsync(aboutDto);
                if (about != null)
                {
                    TempData["success"] = "About added successfully. ";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            return View(aboutDto);

        }

        /// <summary>
        /// Displays the update about view.
        /// </summary>
        /// <param name="id">The ID of the about entry to update.</param>
        /// <returns>A view for updating the about entry.</returns>
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var about = await _service.GetByIdAsync(id);
            return View(about);
        }

        /// <summary>
        /// Updates an existing about entry.
        /// </summary>
        /// <param name="id">The ID of the about entry to update.</param>
        /// <param name="about">The updated about data.</param>
        /// <param name="imageFile">The image file to be uploaded.</param>
        /// <returns>A redirection to the Index action on success, or the view with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutDto about, IFormFile imageFile)
        {
            if (id != about.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Img");

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imagePath = _imgPath + imageFile.FileName;
                        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                            about.Img = imagePath;
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "About have been successfully changed.";
                _service.Update(about);
                return RedirectToAction(nameof(Index));
            }

            return View(about);
        }

        /// <summary>
        /// Displays the delete about view.
        /// </summary>
        /// <param name="id">The ID of the about entry to delete.</param>
        /// <returns>A view for deleting the about entry.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var about = await _service.GetByIdAsync(id);
            return View(about);
        }

        /// </summary>
        /// <param name="itemDto">The about data to delete.</param>
        /// <returns>A redirection to the Index action on success.</returns>
        [HttpPost]
        public IActionResult Delete(AboutDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "About have been successfully deleted.";
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Checks if an about entry exists with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the about entry to check.</param>
        /// <returns>True if the about entry exists, otherwise false.</returns>
        private bool AboutExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
