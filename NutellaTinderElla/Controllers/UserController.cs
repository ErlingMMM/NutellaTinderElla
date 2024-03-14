using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.UserData;
using NutellaTinderElla.Services.UserData;
using NutellaTinderElla.Services.Matching;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using System.Net.Mime;
using NutellaTinderElla.Services.Messaging;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly ISwipeService _swipeService;
        private readonly IMatchService _matchService;
        private readonly IMessageService _messageService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILikeService likeService, ISwipeService swipeService, IMatchService matchService, IMessageService messageService,
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
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _likeService.DeleteByIdAsync(id);
            await _matchService.DeleteByIdAsync(id);
            await _swipeService.DeleteByIdAsync(id);
            await _messageService.DeleteByIdAsync(id);
            await _userService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}

