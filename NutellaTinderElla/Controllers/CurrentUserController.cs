using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.CurrentUser;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Services.Characters;
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

        public CurrentUserController(ICharacterService currentUserService, IMapper mapper)
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
                return Ok(_mapper
                    .Map<CurrentUserDTO>(
                        await _currentUserService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

