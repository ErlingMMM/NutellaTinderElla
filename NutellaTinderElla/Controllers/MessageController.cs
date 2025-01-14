﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Services.UserData;
using NutellaTinderElla.Services.Matching;
using NutellaTinderEllaApi.Data.Exceptions;
using System.Net.Mime;
using NutellaTinderElla.Services.Messaging;
using NutellaTinderElla.Data.Dtos.Messaging;
using NutellaTinderElla.Services.Encryption;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/Message")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]


    public class MessageController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMatchService _matchService;
        private readonly IMessageService _messageService;
        private readonly AesEncryptionService _encryptionService;


        private readonly IMapper _mapper;

        public MessageController(IUserService userService, IMatchService matchService, IMessageService messageService, AesEncryptionService encryptionService,
            IMapper mapper)
        {
            _userService = userService;
            _matchService = matchService;
            _messageService = messageService;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Adding liked users for spesific userprofile from database using userprofiles id, expects code 204
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost("{senderId}/message/{receiverId}")]
        public async Task<IActionResult> PostMessage(int senderId, int receiverId, string content)
        {
            try
            {
                var sender = await _userService.GetByIdAsync(senderId);
                var receiver = await _userService.GetByIdAsync(receiverId);

                if (sender == null)
                    return NotFound($"Sender user with id {senderId} not found");
                if (receiver == null)
                    return NotFound($"Receiver user with id {receiverId} not found");
                if (!await _matchService.HasMatchAsync(sender.Id, receiver.Id))
                    return BadRequest("Users do not match");

                var encryptedContent = _encryptionService.Encrypt(content, out byte[] iv);

                await _messageService.SendMessageAsync(sender.Id, receiver.Id, encryptedContent, iv);
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
                var matchingUser = await _userService.GetByIdAsync(matchingUserId);

                if (user == null)
                    return NotFound($"User with id {userId} not found");

                if (matchingUser == null)
                    return NotFound($"Matching user with id {matchingUserId} not found");

                var allMessages = await _messageService.GetAllAsync();

                await _messageService.UpdateMessagesToRead(user.Id, matchingUser.Id);

                var filteredMessages = await _messageService.GetFilteredMessagesForTwoUsersAsync(userId, matchingUserId);

                var totalCount = filteredMessages.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (pageIndex < 0 || pageIndex >= totalPages)
                    return BadRequest("Invalid page index");


                var messagesToDisplay = filteredMessages
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();

                foreach (var message in messagesToDisplay)
                {
                    if (message.IV != null) // Add a null check
                    {
                        // Decrypt the content before mapping it to MessageDTO
                        message.Content = _encryptionService.Decrypt(message.Content, message.IV);
                    }
                }

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
                    return NotFound($"Message with id {messageId} not found");

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

