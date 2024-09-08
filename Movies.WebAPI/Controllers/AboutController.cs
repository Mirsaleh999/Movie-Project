using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Movies.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IGenericService<AboutDto, About> _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        public AboutController(IGenericService<AboutDto, About> service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<AboutDto>>> GetList()
        {
            var response = await _service.GetListAsync();
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AboutDto>> GetByIdAsync(int id)

        {
            if (id == 0)
            {
                return BadRequest();
            }

            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AboutDto>> Create([FromForm] AboutDto itemDto, [FromForm] IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    itemDto.Img = imagePath;
                }
            }

            var response = await _service.AddAsync(itemDto);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<AboutDto> Update(int id, AboutDto obj, IFormFile imageFile)
        {
            if (id == 0 || id != obj.Id)
            {
                return BadRequest();
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                     imageFile.CopyToAsync(stream);
                    obj.Img = imagePath;
                }
            }

            var response = _service.GetByIdAsync(id).Result;
            if (response == null)
            {
                return NotFound();
            }
            response = _service.Update(obj);
            return Ok(response); ;
        }

        [HttpDelete("{id:int}")]

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var response = _service.GetByIdAsync(id).Result;
            if (response == null)
            {
                return NotFound();
            }

            _service.Delete(id);
            return NoContent();
        }
    }
}
