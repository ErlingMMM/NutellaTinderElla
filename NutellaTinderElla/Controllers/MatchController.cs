using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.UserData;
using NutellaTinderElla.Services.UserData;
using NutellaTinderElla.Services.Matching;
using System.Net.Mime;
using NutellaTinderElla.Services.Messaging;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/Match")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class MatchController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly ISwipeService _swipeService;
        private readonly IMatchService _matchService;
        private readonly IMessageService _messageService;

        private readonly IMapper _mapper;

        public MatchController(IUserService userService, ILikeService likeService, ISwipeService swipeService, IMatchService matchService, IMessageService messageService,
            IMapper mapper)
        {
            _userService = userService;
            _likeService = likeService;
            _swipeService = swipeService;
            _matchService = matchService;
            _messageService = messageService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get a all matches for a spesific user by the users id.  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/matches")]
        public async Task<ActionResult<IEnumerable<UserPublicDataDTO>>> GetMatchesForUser(int id)
        {
            var allUsers = await _userService.GetAllAsync();

            var rowsWithUsersMatches = await _userService.GetUsersMatchByUsersIdsAsync(id);

            var usersToDisplay = allUsers.Where(u =>
                rowsWithUsersMatches.Contains(u.Id)
            ).ToList();

            var userDTOs = usersToDisplay.Select(u => _mapper.Map<UserPublicDataDTO>(u)).ToList();

            return Ok(userDTOs);
        }




        /// <summary>
        /// Delete a match by the user's id and match user's id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="matchedUserId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/match/{matchedUserId}")]
        public async Task<ActionResult> DeleteMatchForUser(int userId, int matchedUserId)
        {
            await _matchService.DeleteMatchByIdAsync(userId, matchedUserId);
            await _likeService.DeleteLikeByIdAsync(userId, matchedUserId);
            await _swipeService.DeleteSwipeByIdAsync(userId, matchedUserId);
            await _messageService.DeleteMessagesById(userId, matchedUserId);
            return NoContent();
        }
    }
}

