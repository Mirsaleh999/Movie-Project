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
    /// The TrandsController is responsible for handling trend category-related actions.
    /// </summary>
    public class TrandsController : Controller
    {
       
        private readonly IGenericService<TrandCategoryDto, Trends> _service;

        /// <summary>
        /// Initializes a new instance of the TrandsController class.
        /// </summary>
        /// <param name="service">The service for managing trend categories.</param>
        public TrandsController(IGenericService<TrandCategoryDto, Trends> service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of trend categories.
        /// </summary>
        /// <returns>A view containing the list of trend categories.</returns>
        public async Task<IActionResult> Index()
        {
            var trendsCategories = await _service.GetListAsync();
            return View(trendsCategories);
        }

        /// <summary>
        /// Shows the form to update a trend category.
        /// </summary>
        /// <param name="id">The ID of the trend category.</param>
        /// <returns>A view containing the trend category to update.</returns>
        public async Task<IActionResult> Update(int id)
        {
            var trendsCategories = await _service.GetByIdAsync(id);
            return View(trendsCategories);
        }

        /// <summary>
        /// Updates the specified trend category.
        /// </summary>
        /// <param name="itemDto">The trend category data to update.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the update view with the provided data.</returns>
        [HttpPost]
        public IActionResult Update(TrandCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var trendsCategories = _service.Update(itemDto);
                if (trendsCategories != null)
                {
                    TempData["success"] = "Trends have been successfully changed.";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to create a new trend category.
        /// </summary>
        /// <returns>A view to create a new trend category.</returns>
        public async Task<IActionResult> Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new trend category.
        /// </summary>
        /// <param name="itemDto">The trend category data to create.</param>
        /// <returns>A redirection to the Index action if successful; otherwise, the create view with the provided data.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(TrandCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var trendsCategories = await _service.AddAsync(itemDto);
                if (trendsCategories != null)
                {
                    TempData["success"] = "Trands added successfully. ";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        /// <summary>
        /// Shows the form to delete a trend category.
        /// </summary>
        /// <param name="id">The ID of the trend category.</param>
        /// <returns>A view containing the trend category to delete.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var trendsCategories = await _service.GetByIdAsync(id);
            return View(trendsCategories);
        }

        /// <summary>
        /// Deletes the specified trend category.
        /// </summary>
        /// <param name="itemDto">The trend category data to delete.</param>
        /// <returns>A redirection to the Index action.</returns>
        [HttpPost]
        public IActionResult Delete(TrandCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Trends have been successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}
