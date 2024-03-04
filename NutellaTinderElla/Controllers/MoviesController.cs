using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebMovieApi.Data.Models;
using WebMovieApi.Data.Dtos.Movie;
using WebMovieApi.Data.Dtos.Character;
using WebMovieApi.Services.Movies;
using WebMovieApi.Data.Exceptions;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMovieApi.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.

    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movService, IMapper mapper)
        {
            _movService = movService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all movies from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return Ok(_mapper
                .Map<IEnumerable<MovieDTO>>(
                    await _movService.GetAllAsync()));
        }

        /// <summary>
        /// Get a movie from the database using id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<MovieDTO>(
                        await _movService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updating a movie from the database using id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MoviePutDTO movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movService.UpdateAsync(_mapper.Map<Movie>(movie));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        /// <summary>
        /// Creating a new movie to the database
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MoviePostDTO movie)
        {
            var newMov = await _movService.AddAsync(_mapper.Map<Movie>(movie));

            return CreatedAtAction("GetMovie",
                new { id = newMov.Id },
                _mapper.Map<MovieDTO>(newMov));
        }

        /// <summary>
        /// Delete a movie in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Unique for movies

        /// <summary>
        /// Get all characters in a movie in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetAllCharactersInMovie(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<CharacterDTO>>(
                        await _movService.GetAllCharactersInMovieAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        /// <summary>
        /// Update characters in a movie in database
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="characterIds"></param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersInMovie(int movieId, int[] characterIds)
        {
            try
            {
                await _movService.UpdateCharactersInMovieAsync(movieId, characterIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
