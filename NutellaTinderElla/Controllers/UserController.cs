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
    [Route("api/v1/User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly ISwipeService _swipeService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILikeService likeService, ISwipeService dislikeService, IMapper mapper)
        {
            _userService = userService;
            _likeService = likeService;
            _swipeService = dislikeService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get a spesific user from database using their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                var currentUser = await _userService.GetByIdAsync(id);
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
        public async Task<ActionResult<UserDTO>> PostUser(UserPostDTO currentUser)
        {
            var newUser = await _userService.AddAsync(_mapper.Map<User>(currentUser));

            return CreatedAtAction("GetUser",
                new { id = newUser.Id },
                _mapper.Map<UserDTO>(newUser));
        }


        /// <summary>
        /// Get a random user from database for a new swipe. Not yet swiped on by the user. 
        /// </summary>
        /// <param name="swipingUserId"></param>
        /// <returns></returns>
        [HttpGet("{swipingUserId}/newSwipe")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersNotSwiped(int swipingUserId)
        {
            // Get all users from the database
            var allUsers = await _userService.GetAllAsync();

            // Get the IDs of users already swiped by the given user
            var swipedUserIds = await _userService.GetSwipedUserIdsAsync(swipingUserId);

            // Filter out users who have already been swiped and the users own profile
            var usersToDisplay = allUsers.Where(u => u.Id != swipingUserId && !swipedUserIds.Contains(u.Id)).ToList();

            // If there are no users left after filtering, return an empty list
            if (usersToDisplay.Count == 0)
            {
                return Ok(new List<UserDTO>());
            }

            // Get a random index within the range of filtered users
            var randomIndex = new Random().Next(0, usersToDisplay.Count);

            // Get the random user profile
            var randomUser = usersToDisplay[randomIndex];

            // Map the user profile to DTO and return
            return Ok(_mapper.Map<UserDTO>(randomUser));
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
                var liker = await _userService.GetByIdAsync(id);
                if (liker == null)
                {
                    return NotFound($"Liker with id {id} not found");
                }

                // Retrieve the liked user from the database based on the provided likedUserId
                var likedUserEntity = await _userService.GetByIdAsync(likedUser.LikedUserId);
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
        /// Adding swiped users for spesific userprofile from database using userprofiles id, expects code 204
        /// </summary>
        /// <param name="id"></param>
        /// <param name="swipes"></param>
        /// <returns></returns>
        [HttpPut("{id}/swipes")]
        public async Task<IActionResult> PutSwipes(int id, SwipePutDTO swipedUser)
        {
            try
            {
                // Retrieve the swiped user from the database based on the provided id
                var swiper = await _userService.GetByIdAsync(id);
                if (swiper == null)
                {
                    return NotFound($"Swiper with id {id} not found");
                }

                // Retrieve the swiped user from the database based on the provided likedUserId
                var swipedUserEntity = await _userService.GetByIdAsync(swipedUser.SwipedUserId);
                if (swipedUserEntity == null)
                {
                    return NotFound($"Swiped user with id {swipedUser.SwipedUserId} not found");
                }

                // Create a new swipe entity
                var swipe = new Swipes
                {
                    SwiperId = swiper.Id,
                    SwipedUserId = swipedUserEntity.Id
                };

                // Add the new swipe to the database
                await _swipeService.AddAsync(swipe);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


    }
}

