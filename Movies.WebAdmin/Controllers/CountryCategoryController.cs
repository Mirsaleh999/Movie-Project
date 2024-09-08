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
    /// The CountryCategoryController is responsible for handling country category-related actions.
    /// </summary>
    public class CountryCategoryController : Controller
    {

        private readonly IGenericService<CountryCategoryDto, CountryCategory> _service;

        /// <summary>
        /// Initializes a new instance of the CountryCategoryController class.
        /// </summary>
        /// <param name="service">The service for managing country categories.</param>
        public CountryCategoryController(IGenericService<CountryCategoryDto, CountryCategory> service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of country categories.
        /// </summary>
        /// <returns>A view containing the list of country categories.</returns>
        public async Task<IActionResult> Index()
        {
            var countryCategories = await _service.GetListAsync();
            return View(countryCategories);
        }

        /// <summary>
        /// Shows the form to update a country category.
        /// </summary>
        /// <param name="id">The ID of the country category.</param>
        /// <returns>A view containing the country category to update.</returns>
        public async Task<IActionResult> Update(int id)
        {
            var countryCategories = await _service.GetByIdAsync(id);
            return View(countryCategories);
        }

        /// <summary>
        /// Updates the specified country category.
        /// </summary>
        /// <param name="itemDto">The country category data to update.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the update view with the provided data.</returns>
        [HttpPost]
        public IActionResult Update(CountryCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var countryCategories = _service.Update(itemDto);
                if (countryCategories != null)
                {
                    TempData["success"] = "Country have been successfully changed.";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to create a new country category.
        /// </summary>
        /// <returns>A view to create a new country category.</returns>
        public async Task<IActionResult> Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new country category.
        /// </summary>
        /// <param name="itemDto">The country category data to create.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the create view with the provided data.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CountryCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var category = await _service.AddAsync(itemDto);
                if (category != null)
                {
                    TempData["success"] = "Country added successfully. ";
                    return RedirectToAction("Index");
                }
            }
            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to delete a country category.
        /// </summary>
        /// <param name="id">The ID of the country category.</param>
        /// <returns>A view containing the country category to delete.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);
            return View(category);
        }

        /// <summary>
        /// Deletes the specified country category.
        /// </summary>
        /// <param name="itemDto">The country category data to delete.</param>
        /// <returns>A redirection to the Index action.</returns>
        [HttpPost]
        public IActionResult Delete(CountryCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Country have been successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}