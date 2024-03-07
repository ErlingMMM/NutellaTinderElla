using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.ActiveUser;
using NutellaTinderElla.Data.Dtos.Matching;
using NutellaTinderElla.Data.Models;
using NutellaTinderElla.Services.ActiveUser;
using NutellaTinderElla.Services.Matching;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using System.Net.Mime;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/Matching")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class MatchingController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly ISwipeService _swipeService;
        private readonly IMapper _mapper;

        public MatchingController(ILikeService likeService, ISwipeService dislikeService, IMapper mapper)
        {
            _likeService = likeService;
            _swipeService = dislikeService;
            _mapper = mapper;
        }



        /// <summary>
        /// Get all characters in a movie in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        /*
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
        */




    }
}

