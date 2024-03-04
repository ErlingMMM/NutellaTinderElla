using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebMovieApi.Data.Dtos.Franchise;
using WebMovieApi.Data.Dtos.Movie;
using WebMovieApi.Data.Dtos.Character;
using WebMovieApi.Data.Exceptions;
using WebMovieApi.Data.Models;
using WebMovieApi.Services.Franchises;
using System.Net.Mime;


namespace WebMovieApi.Controllers
{
    [ApiController]
    [Route("api/v1/franchise")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _fraService;
        private readonly IMapper _mapper;

        public FranchiseController(IFranchiseService fraService, IMapper mapper)
        {
            _fraService = fraService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all franchises from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDTO>>> GetFranchise()
        {
            return Ok(_mapper
                .Map<IEnumerable<FranchiseDTO>>(
                    await _fraService.GetAllAsync()));
        }

        /// <summary>
        /// Get a spesific franchise from the database using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDTO>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<FranchiseDTO>(
                        await _fraService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updating a franchise from database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            try
            {
                await _fraService.UpdateAsync(_mapper.Map<Franchise>(franchise));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Creating a new franchise to the database
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseDTO>> PostFranchise(FranchisePostDTO franchise)
        {
            var newFra = await _fraService.AddAsync(_mapper.Map<Franchise>(franchise));

            return CreatedAtAction("GetFranchise",
                new { id = newFra.Id },
                _mapper.Map<FranchiseDTO>(newFra));
        }

        /// <summary>
        /// Deleting a franchise from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrachise(int id)
        {
            try
            {
                await _fraService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Unique for franchise

        /// <summary>
        /// Get all movies in a franchise in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAllMoviesInFranchise(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<MovieDTO>>(
                        await _fraService.GetAllMoviesInFranchiseAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        /// <summary>
        /// Update all movies in a franchise in database
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <param name="movieIds"></param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesInFranchiseAsync(int franchiseId, int[] movieIds) 
        {
            try
            {
                await _fraService.UpdateMoviesInFranchiseAsync(franchiseId, movieIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get all character from a franchise in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetAllCharactersInFranchiseAsync(int id) 
        {
            try
            {
                //await _fraService.GetAllCharactersInFranchiseAsync(id);
                //return NoContent();
                return Ok(_mapper
                    .Map<IEnumerable<CharacterDTO>>(
                        await _fraService.GetAllCharactersInFranchiseAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
