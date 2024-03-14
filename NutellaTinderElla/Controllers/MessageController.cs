using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Services.UserData;
using NutellaTinderElla.Services.Matching;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using System.Net.Mime;
using NutellaTinderElla.Services.Messaging;
using NutellaTinderElla.Data.Dtos.Messaging;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/Message")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.

    public class MessageController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMatchService _matchService;
        private readonly IMessageService _messageService;

        private readonly IMapper _mapper;

        public MessageController(IUserService userService, IMatchService matchService, IMessageService messageService,
            IMapper mapper)
        {
            _userService = userService;
            _matchService = matchService;
            _messageService = messageService;
            _mapper = mapper;
        }


        [HttpPost("{senderId}/message/{receiverId}")]
        public async Task<IActionResult> PostMessage(int senderId, int receiverId, string content)
        {
            try
            {
                var sender = await _userService.GetByIdAsync(senderId);
                var receiver = await _userService.GetByIdAsync(receiverId);

                if (sender == null) return NotFound($"Sender user with id {senderId} not found");
                
                if (receiver == null) return NotFound($"Receiver user with id {receiverId} not found");

                if (!await _matchService.HasMatchAsync(sender.Id, receiver.Id)) return BadRequest("Users do not match");
               
                await _messageService.SendMessageAsync(sender.Id, receiver.Id, content);

                return Ok("Message sent");
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }




        /// <summary>
        /// Get a spesific users messages between a spesific match using their id's.
        /// </summary>
        /// <param name="userId"></param>
        ///<param name="matchingUserId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/messages/{matchingUserId}")]
        public async Task<ActionResult<MessageDTO>> GetMessagesForTwoMatchingUsers(int userId, int matchingUserId, int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                var user = await _userService.GetByIdAsync(userId);
                if (user == null)
                {
                    return NotFound($"User with id {userId} not found");
                }

                var matchingUser = await _userService.GetByIdAsync(matchingUserId);
                if (matchingUser == null)
                {
                    return NotFound($"Matching user with id {matchingUserId} not found");
                }

                var allMessages = await _messageService.GetAllAsync();

                await _messageService.UpdateMessagesToRead(user.Id, matchingUser.Id);

                var filteredMessages = allMessages.Where(m =>
                    (m.SenderId == user.Id && m.ReceiverId == matchingUser.Id) ||
                    (m.SenderId == matchingUser.Id && m.ReceiverId == user.Id)
                ).OrderByDescending(m => m.Timestamp);

                var totalCount = filteredMessages.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (pageIndex < 0 || pageIndex >= totalPages)
                {
                    return BadRequest("Invalid page index");
                }

                var messagesToDisplay = filteredMessages
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();

                var messageDTOs = messagesToDisplay.Select(m => _mapper.Map<MessageDTO>(m)).ToList();

                return Ok(new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageIndex,
                    PageSize = pageSize,
                    Messages = messageDTOs
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        /// <summary>
        /// Set a message to be liked
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpPut("{messageId}/likedMessage/")]
        public async Task<ActionResult<MessageLikedDTO>> PutLikeToMessage(int messageId)
        {
            try
            {
                var message = await _messageService.GetByIdAsync(messageId);
                if (message == null)
                {
                    return NotFound($"Message with id {messageId} not found");
                }

                message.IsLiked = !message.IsLiked;

                await _messageService.UpdateAsync(message);

                return Ok(new MessageLikedDTO { IsLiked = message.IsLiked });

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}

