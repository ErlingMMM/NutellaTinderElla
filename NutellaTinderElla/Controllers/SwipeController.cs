using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.UserData;
using NutellaTinderElla.Data.Dtos.Matching;
using NutellaTinderElla.Data.Models;
using NutellaTinderElla.Services.UserData;
using NutellaTinderElla.Services.Matching;
using NutellaTinderEllaApi.Data.Exceptions;
using System.Net.Mime;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/Swipe")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class SwipeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISwipeService _swipeService;

        private readonly IMapper _mapper;

        public SwipeController(IUserService userService, ISwipeService swipeService,
            IMapper mapper)
        {
            _userService = userService;
            _swipeService = swipeService;
            _mapper = mapper;
        }



        /// <summary>
        /// Get a random user from database for a new swipe. Not yet swiped on by the user. 
        /// </summary>
        /// <param name="swipingUserId"></param>
        /// <returns></returns>
        [HttpGet("{swipingUserId}/newSwipe")]
        public async Task<ActionResult<IEnumerable<UserPublicDataDTO>>> GetUsersNotSwiped(int swipingUserId)
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

                u.AgePreferenceMaximum >= swipingUser.Age &&
                u.AgePreferenceMinimum <= swipingUser.Age &&
                u.Age >= swipingUser.AgePreferenceMinimum &&
                u.Age <= swipingUser.AgePreferenceMaximum &&

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
                return Ok(new List<UserPublicDataDTO>());

            // Get a random index within the range of filtered users
            var randomIndex = new Random().Next(0, usersToDisplay.Count);

            // Get the random user profile
            var randomUser = usersToDisplay[randomIndex];

            // Map the user profile to DTO and return
            return Ok(_mapper.Map<UserPublicDataDTO>(randomUser));
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
                    return NotFound($"Swiper with id {id} not found");

                // Retrieve the swiped user from the database based on the provided likedUserId
                var swipedUserEntity = await _userService.GetByIdAsync(swipedUser.SwipedUserId);
                if (swipedUserEntity == null)
                    return NotFound($"Swiped user with id {swipedUser.SwipedUserId} not found");

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

