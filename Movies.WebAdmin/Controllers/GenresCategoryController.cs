using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Services;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using Serilog;
using System.Data;
using static System.Formats.Asn1.AsnWriter;

namespace Movies.WebAdmin.Controllers
{
    [Authorize(Roles = "Operator")]
    /// <summary>
    /// The GenresCategoryController is responsible for handling genre category-related actions.
    /// </summary>
    public class GenresCategoryController : Controller
    {
        private readonly IGenericService<GenresCategoryDto, GenresCategory> _service;

        /// <summary>
        /// Initializes a new instance of the GenresCategoryController class.
        /// </summary>
        /// <param name="service">The service for managing genre categories.</param>
        public GenresCategoryController(IGenericService<GenresCategoryDto, GenresCategory> service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of genre categories.
        /// </summary>
        /// <returns>A view containing the list of genre categories.</returns>
        public async Task<IActionResult> Index()
        {
            var genresCategories = await _service.GetListAsync();
            return View(genresCategories);
        }

        /// <summary>
        /// Shows the form to update a genre category.
        /// </summary>
        /// <param name="id">The ID of the genre category.</param>
        /// <returns>A view containing the genre category to update.</returns>
        public async Task<IActionResult> Update(int id)
        {
            var genresCategories = await _service.GetByIdAsync(id);
            return View(genresCategories);
        }

        /// <summary>
        /// Updates the specified genre category.
        /// </summary>
        /// <param name="itemDto">The genre category data to update.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the update view with the provided data.</returns>
        [HttpPost]
        public async Task<IActionResult> Update(GenresCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var genresCategories = _service.Update(itemDto);

                if (genresCategories != null)
                {
                    TempData["success"] = "Genres have been successfully changed.";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to create a new genre category.
        /// </summary>
        /// <returns>A view to create a new genre category.</returns>
        public async Task<IActionResult> Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new genre category.
        /// </summary>
        /// <param name="itemDto">The genre category data to create.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the create view with the provided data.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(GenresCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var genresCategories = await _service.AddAsync(itemDto);
                if (genresCategories != null)
                {
                    TempData["success"] = "Genres added successfully. ";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to delete a genre category.
        /// </summary>
        /// <param name="id">The ID of the genre category.</param>
        /// <returns>A view containing the genre category to delete.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var genresCategories = await _service.GetByIdAsync(id);
            return View(genresCategories);
        }
        /// <summary>
        /// Deletes the specified genre category.
        /// </summary>
        /// <param name="itemDto">The genre category data to delete.</param>
        /// <returns>A redirection to the Index action.</returns>
        [HttpPost]
        public IActionResult Delete(GenresCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Genres have been successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}
