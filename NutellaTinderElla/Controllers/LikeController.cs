﻿using Microsoft.AspNetCore.Mvc;
using NutellaTinderElla.Data.Dtos.Matching;
using NutellaTinderElla.Data.Models;
using NutellaTinderElla.Services.UserData;
using NutellaTinderElla.Services.Matching;
using NutellaTinderEllaApi.Data.Exceptions;
using System.Net.Mime;

namespace NutellaTinderElla.Controllers

{
    [ApiController]
    [Route("api/v1/Likes")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class LikeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly ISwipeService _swipeService;
        private readonly IMatchService _matchService;
        public LikeController(IUserService userService, ILikeService likeService, ISwipeService swipeService, IMatchService matchService)
        {
            _userService = userService;
            _likeService = likeService;
            _swipeService = swipeService;
            _matchService = matchService;
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
                    return NotFound($"Liker with id {id} not found");


                // Retrieve the liked user from the database based on the provided likedUserId
                var likedUserEntity = await _userService.GetByIdAsync(likedUser.LikedUserId);
                if (likedUserEntity == null)
                    return NotFound($"Liked user with id {likedUser.LikedUserId} not found");

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
    }
}

