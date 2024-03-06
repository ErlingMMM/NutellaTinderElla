using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.ActiveUser;
using NutellaTinderElla.Data.Dtos.Matching;
using NutellaTinderElla.Services.ActiveUser;
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
        /// Creating a new user to the database
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


        /// <summary>
        /// Adding liked users for spesific userprofile from database using userprofiles id, expects code 204
        /// </summary>
        /// <param name="id"></param>
        /// <param name="likes"></param>
        /// <returns></returns>
        [HttpPut("{id}likes")]
        public async Task<IActionResult> PutLikes(int id, LikesPutDTO likedUser)
        {
            if (id != likedUser.Id)
            {
                return BadRequest();
            }

            try
            {
                await _currentUserService.UpdateAsync(_mapper.Map<CurrentUser>(likedUser));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

    }
}

