using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using NutellaTinderEllaApi.Data.Dtos.Character;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Services.Characters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NutellaTinderEllaApi.Controllers
{
    [ApiController]
    [Route("api/v1/Character")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _chaService;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService chaService, IMapper mapper)
        {
            _chaService = chaService;
            _mapper = mapper;
        }


        /// <summary>
        /// Gets a list of all characters in the database. No params.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CharacterDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return Ok(_mapper
                .Map<IEnumerable<CharacterDTO>>(
                    await _chaService.GetAllAsync()));
        }
        /// <summary>
        /// Get a spesific character from database using their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<CharacterDTO>(
                        await _chaService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        /// <summary>
        /// Updating a spesific character from database using their id, expects code 204
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPutDTO character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            try
            {
                await _chaService.UpdateAsync(_mapper.Map<Character>(character));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Creating a new character to the database
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CharacterDTO>> PostCharacter(CharacterPostDTO character)
        {
            var newCha = await _chaService.AddAsync(_mapper.Map<Character>(character));

            return CreatedAtAction("GetCharacter",
                new { id = newCha.Id },
                _mapper.Map<CharacterDTO>(newCha));
        }

        /// <summary>
        /// Deleting a character from the database using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _chaService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
