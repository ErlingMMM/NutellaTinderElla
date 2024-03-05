using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.ActiveUser;
using NutellaTinderElla.Services.ActiveUser;
using NutellaTinderEllaApi.Data.Dtos.Character;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using System.Net.Mime;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/CurrentUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class CurrentUserController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CurrentUserController(ICurrentUserService currentUserService, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get a spesific user from database using their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentUserDTO>> GetUser(int id)
        {
            try
            {
                var currentUser = await _currentUserService.GetByIdAsync(id);
                return Ok(currentUser);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Creating a new character to the database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CurrentUserDTO>> PostCurrentUser(CurrentUserPostDTO currentUser)
        {
            var newUser = await _currentUserService.AddAsync(_mapper.Map<CurrentUser>(currentUser));

            return CreatedAtAction("GetUser",
                new { id = newUser.Id },
                _mapper.Map<CurrentUserDTO>(newUser));
        }

    }
}

