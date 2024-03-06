using NutellaTinderEllaApi.Data.Models;

namespace NutellaTinderElla.Data.Dtos.Matching
{
    public class LikesPutDTO
    {
        public int LikerId { get; set; }
        public int LikedUserId { get; set; }

    }
}
