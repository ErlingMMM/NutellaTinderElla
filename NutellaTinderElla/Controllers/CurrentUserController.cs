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
    [Route("api/v1/CurrentUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class CurrentUserController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILikeService _likeService;
        private readonly IDislikeService _dislikeService;
        private readonly IMapper _mapper;

        public CurrentUserController(ICurrentUserService currentUserService, ILikeService likeService, IDislikeService dislikeService, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _likeService = likeService;
            _dislikeService = dislikeService;
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
        [HttpPut("{id}/likes")]
        public async Task<IActionResult> PutLikes(int id, LikesPutDTO likedUser)
        {
            try
            {
                // Retrieve the liker user from the database based on the provided id
                var liker = await _currentUserService.GetByIdAsync(id);
                if (liker == null)
                {
                    return NotFound($"Liker with id {id} not found");
                }

                // Retrieve the liked user from the database based on the provided likedUserId
                var likedUserEntity = await _currentUserService.GetByIdAsync(likedUser.LikedUserId);
                if (likedUserEntity == null)
                {
                    return NotFound($"Liked user with id {likedUser.LikedUserId} not found");
                }

                // Create a new Like entity
                var like = new Like
                {
                    LikerId = liker.Id,
                    LikedUserId = likedUserEntity.Id
                };

                // Add the new like to the database
                await _likeService.AddAsync(like);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Adding liked users for spesific userprofile from database using userprofiles id, expects code 204
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dislikes"></param>
        /// <returns></returns>
        [HttpPut("{id}/dislikes")]
        public async Task<IActionResult> PutDislikes(int id, DislikesPutDTO dislikedUser)
        {
            try
            {
                // Retrieve the liker user from the database based on the provided id
                var disliker = await _currentUserService.GetByIdAsync(id);
                if (disliker == null)
                {
                    return NotFound($"Disliker with id {id} not found");
                }

                // Retrieve the liked user from the database based on the provided likedUserId
                var dislikedUserEntity = await _currentUserService.GetByIdAsync(dislikedUser.DislikedUserId);
                if (dislikedUserEntity == null)
                {
                    return NotFound($"Liked user with id {dislikedUser.DislikedUserId} not found");
                }

                // Create a new Like entity
                var dislike = new Dislike
                {
                    DislikerId = disliker.Id,
                    DislikedUserId = dislikedUserEntity.Id
                };

                // Add the new like to the database
                await _dislikeService.AddAsync(dislike);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


    }
}

