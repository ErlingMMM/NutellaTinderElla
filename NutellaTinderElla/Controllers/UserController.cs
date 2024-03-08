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
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILikeService likeService, ISwipeService swipeService, IMatchService matchService, IMapper mapper)
        {
            _userService = userService;
            _likeService = likeService;
            _swipeService = swipeService;
            _matchService = matchService;
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
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
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
        public async Task<ActionResult<UserDTO>> PostUser(UserPostDTO user)
        {
            var newUser = await _userService.AddAsync(_mapper.Map<User>(user));

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

            var swipingUser = await _userService.GetByIdAsync(swipingUserId);


            var usersToDisplay = allUsers.Where(u =>
                // Exclude the swiping user's own profile
                u.Id != swipingUserId &&

                // Match users with the same seeking preference
                u.Seeking == swipingUser.Seeking &&

                // Match users based on gender preferences
                (
                    // Match users with compatible gender preferences
                    (u.Gender == swipingUser.GenderPreference && swipingUser.Gender == u.GenderPreference) ||
                    (u.GenderPreference == 2 && swipingUser.GenderPreference == 2) ||
                    (swipingUser.GenderPreference == 2 && u.GenderPreference == swipingUser.Gender) ||
                    (u.GenderPreference == 2 && swipingUser.GenderPreference == u.Gender)
                ) &&

                // Exclude users who have already been swiped by the swiping user
                !swipedUserIds.Contains(u.Id)
            ).ToList();


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
        [HttpPost("{id}/likes")]
        public async Task<IActionResult> PostLikes(int id, LikesPostDTO likedUser)
        {
            try
            {

                // Retrieve the liker user from the database based on the provided id
                var liker = await _userService.GetByIdAsync(id);
                if (liker == null)
                {
                    return NotFound($"Liker with id {id} not found");
                }

                var allLikes = await _likeService.GetAllAsync();


                // Retrieve the liked user from the database based on the provided likedUserId
                var likedUserEntity = await _userService.GetByIdAsync(likedUser.LikedUserId);
                if (likedUserEntity == null)
                {
                    return NotFound($"Liked user with id {likedUser.LikedUserId} not found");
                }

                var like = new Like
                {
                    LikerId = liker.Id,
                    LikedUserId = likedUserEntity.Id
                };

                var swipe = new Swipes
                {
                    SwiperId = liker.Id,
                    SwipedUserId = likedUserEntity.Id
                };

                await _likeService.AddAsync(like);
                await _swipeService.AddAsync(swipe);


                var hasMatch = await _likeService.HasMatchAsync(liker.Id, likedUserEntity.Id);
                if (hasMatch)
                {
                    var match = new Match
                    {
                        MacherId = liker.Id,
                        MatchedUserId = likedUserEntity.Id
                    };

                    await _matchService.AddAsync(match);

                    return Ok("Users has matched");
                }

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
        [HttpPost("{id}/swipes")]
        public async Task<IActionResult> PostSwipes(int id, SwipePostDTO swipedUser)
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

