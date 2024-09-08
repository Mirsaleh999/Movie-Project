using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Movies.WebAdmin.Controllers
{
    [Authorize(Roles = "Operator")]
    /// <summary>
    /// Controller for handling contact-related actions.
    /// </summary>
    public class ContactController : Controller
    {
        private readonly IGenericService<ContactDto, Contact> _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class.
        /// </summary>
        /// <param name="service">The contact service.</param>
        public ContactController(IGenericService<ContactDto, Contact> service)
        {
            _service = service;
        }

        /// <summary>
        /// Displays a list of contacts.
        /// </summary>
        /// <returns>A view containing the contact list.</returns>
        public async Task<IActionResult> Index()
        {
            var contact = await _service.GetListAsync();
            return View(contact);
        }

        /// <summary>
        /// Displays the contact to delete.
        /// </summary>
        /// <param name="id">The ID of the contact to delete.</param>
        /// <returns>A view displaying the contact to delete.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);
            return View(category);
        }

        /// <summary>
        /// Deletes the contact.
        /// </summary>
        /// <param name="itemDto">The contact data to delete.</param>
        /// <returns>A redirection to the Index action.</returns>
        [HttpPost]
        public IActionResult Delete(ContactDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Kateqoriya uğurla silindi.";
            return RedirectToAction("Index");
        }
    }
}
