using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movies.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenericService<GenresCategoryDto, GenresCategory> _service;

        public GenresController(IGenericService<GenresCategoryDto, GenresCategory> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenresCategoryDto>>> GetList()
        {
            var response = await _service.GetListAsync();
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenresCategoryDto>> GetByIdAsync(int id)

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
        public async Task<ActionResult<GenresCategoryDto>> Create(GenresCategoryDto itemDto)
        {

            var response = await _service.AddAsync(itemDto);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<GenresCategoryDto> Update(int id, [FromBody] GenresCategoryDto obj)
        {
            if (id == 0 || id != obj.Id)
            {
                return BadRequest();
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
