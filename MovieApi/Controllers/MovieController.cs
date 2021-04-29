using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Business;
using MovieAPI.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApi.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        public IMovieService _MovieService { get; }
        public MovieController(IMovieService movieService)
        {
            _MovieService =  movieService;
        }

       
        [HttpPost]
        public IActionResult metadata([FromBody] Movie movieData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                //No validation for this movie is already exist or not
                _MovieService.metadata(movieData);
                return Created("metadata", movieData);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("[action]/{movieId}")]
        [HttpGet]
        public IActionResult metadata(long movieId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                //No validation for this movie is already exist or not
                var movies = _MovieService.metadata(movieId); 
                return Ok(movies);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult stats()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var movies = _MovieService.stats();
                return Ok(movies);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


    }
}
