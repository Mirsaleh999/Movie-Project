using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System.Data;

namespace Movies.WebAdmin.Controllers
{
    [Authorize(Roles = "Operator")]
    /// <summary>
    /// The LanguageCategoryController is responsible for handling language category-related actions.
    /// </summary>
    public class LanguageCategoryController : Controller
    {
        private readonly IGenericService<LanguageCategoryDto, LanguageCategory> _service;

        /// <summary>
        /// Initializes a new instance of the LanguageCategoryController class.
        /// </summary>
        /// <param name="service">The service for managing language categories.</param>
        public LanguageCategoryController(IGenericService<LanguageCategoryDto, LanguageCategory> service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of language categories.
        /// </summary>
        /// <returns>A view containing the list of language categories.</returns>
        public async Task<IActionResult> Index()
        {
            var languageCategories = await _service.GetListAsync();
            return View(languageCategories);
        }

        /// <summary>
        /// Shows the form to update a language category.
        /// </summary>
        /// <param name="id">The ID of the language category.</param>
        /// <returns>A view containing the language category to update.</returns>
        public async Task<IActionResult> Update(int id)
        {
            var languageCategories = await _service.GetByIdAsync(id);
            return View(languageCategories);
        }

        /// <summary>
        /// Updates the specified language category.
        /// </summary>
        /// <param name="itemDto">The language category data to update.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the update view with the provided data.</returns>
        [HttpPost]
        public IActionResult Update(LanguageCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var languageCategories = _service.Update(itemDto);
                if (languageCategories != null)
                {
                    TempData["success"] = "Language have been successfully changed.";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to create a new language category.
        /// </summary>
        /// <returns>A view to create a new language category.</returns>
        public async Task<IActionResult> Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new language category.
        /// </summary>
        /// <param name="itemDto">The language category data to create.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the create view with the provided data.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(LanguageCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var languageCategories = await _service.AddAsync(itemDto);
                if (languageCategories != null)
                {
                    TempData["success"] = "Language added successfully. ";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to delete a language category.
        /// </summary>
        /// <param name="id">The ID of the language category.</param>
        /// <returns>A view containing the language category to delete.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var languageCategories = await _service.GetByIdAsync(id);
            return View(languageCategories);
        }

        /// <summary>
        /// Deletes the specified language category.
        /// </summary>
        /// <param name="itemDto">The language category data to delete.</param
        /// <returns>A redirection to the Index action.</returns>
        [HttpPost]
        public IActionResult Delete(LanguageCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Language have been successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}