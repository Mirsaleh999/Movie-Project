using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Movies.Controllers
{
    [Authorize(Roles = "Operator")]
    /// <summary>
    /// Controller for Movie-related actions.
    /// </summary>
    public class MoviesController : Controller
    {
        private readonly IMoviesService _movieService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        private readonly string _videoPath = @"Video/";
        private readonly string _trailerPath = @"Video/";

        /// <summary>
        /// Initializes a new instance of the MoviesController class.
        /// </summary>
        /// <param name="movieService">The movies service for handling movie-related data.</param>
        /// <param name="webHostEnvironment">The web host environment for accessing file paths.</param>
        public MoviesController(IMoviesService movieService, IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Displays a list of movies.
        /// </summary>
        /// <returns>The movie list view.</returns>
        public async Task<IActionResult> Index()
        {
            var movie = await _movieService.GetListAsync();
            return View(movie);
        }

        /// <summary>
        /// Displays the movie creation form.
        /// </summary>
        /// <returns>The movie creation view.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MovieCDto model = new()
            {
                CountryCategoryDtos = await _movieService.GetCountryCategoriesAsync(),
                GenresCategoryDtos = await _movieService.GetGenresCategoriesAsync(),
                LanguageCategoryDtos = await _movieService.GetLangaugeCategoriesAsync(),
                TrandCategoryDtos = await _movieService.GeTTrandsCategoriesAsync()
            };
            return View(model);
        }


        /// <summary>
        /// Handles the movie creation POST request.
        /// </summary>
        /// <param name="movie">The movie data from the view model.</param>
        /// <param name="imageFile">The uploaded image file.</param>
        /// <param name="videoFile">The uploaded video file.</param>
        /// <param name="trailerFile">The uploaded trailer file.</param>
        /// <returns>A redirection to the movie list on success, or the movie creation view with errors on failure.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(MovieCDto movie, IFormFile imageFile, IFormFile videoFile, IFormFile trailerFile)
        {

            //if (ModelState.IsValid)
            //{

            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    movie.Img = imagePath;
                }
            }




            if (videoFile != null && videoFile.Length > 0)
            {
                if (videoFile == null || videoFile.Length == 0)
                {
                    return BadRequest("Invalid file.");
                }
                var videoPath = _videoPath + videoFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, videoPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                    movie.MovieVideo = videoPath;
                }
            }


            if (trailerFile != null && trailerFile.Length > 0)
            {
                var trailerPath = _trailerPath + trailerFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, trailerPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await trailerFile.CopyToAsync(stream);
                    movie.Trailer = trailerPath;
                }
            }


            await _movieService.AddAsync(movie);

            TempData["success"] = "Movie added successfully.";
            return RedirectToAction("Index");
            //}

            return View(movie);
        }

        /// <summary>
        /// Displays the movie editing form.
        /// </summary>
        /// <param name="id">The ID of the movie to edit.</param>
        /// <returns>The movie editing view if the movie is found, NotFound otherwise.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            movie.CountryCategoryDtos = await _movieService.GetCountryCategoriesAsync();
            movie.GenresCategoryDtos = await _movieService.GetGenresCategoriesAsync();
            movie.LanguageCategoryDtos = await _movieService.GetLangaugeCategoriesAsync();
            movie.TrandCategoryDtos = await _movieService.GeTTrandsCategoriesAsync();



            return View(movie);

        }

        /// <summary>
        /// Handles the movie editing POST request.
        /// </summary>
        /// <param name="id">The ID of the movie to edit.</param>
        /// <param name="movie">The movie data from the view model.</param>
        /// <param name="imageFile">The uploaded image file.</param>
        /// <param name="videoFile">The uploaded video file.</param>
        /// <param name="trailerFile">The uploaded trailer file.</param>
        /// <returns>A redirection to the movie list on success, or the movie editing view with errors on failure.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieCDto movie, IFormFile imageFile, IFormFile videoFile, IFormFile trailerFile)
        {

            if (id != movie.Id)
            {
                return NotFound();
            }
   
            //if (ModelState.IsValid)
            //{

            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    movie.Img = imagePath;
                }
            }

            if (videoFile != null && videoFile.Length > 0)
            {
                var videoPath = _videoPath + videoFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, videoPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                    movie.MovieVideo = videoPath;
                }
            }

            if (trailerFile != null && trailerFile.Length > 0)
            {
                var trailerPath = _trailerPath + trailerFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, trailerPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await trailerFile.CopyToAsync(stream);
                    movie.Trailer = trailerPath;
                }
            }

            _movieService.UpdateMovie(movie);
            TempData["success"] = "Movie have been successfully changed.";
            return RedirectToAction("Index");
            //}


            return View(movie);
        }

        /// <summary>
        /// Displays movie details.
        /// </summary>
        /// <param name="id">The ID of the movie to display.</param>
        /// <returns>The movie detail view if the movie is found, NotFound otherwise.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetDetailByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            movie.CountryCategoryDtos = await _movieService.GetCountryCategoriesAsync();
            movie.GenresCategoryDtos = await _movieService.GetGenresCategoriesAsync();
            movie.LanguageCategoryDtos = await _movieService.GetLangaugeCategoriesAsync();

            return View(movie);
        }

        /// <summary>
        /// Handles the movie deletion request.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>A redirection to the movie list on success, NotFound if the movie is not found.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _movieService.Delete(id);
            return RedirectToAction("Index");
        }

    }

}