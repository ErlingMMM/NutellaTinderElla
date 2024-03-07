using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderElla.Data.Models
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        // Foreign key to refer to the user who likes
        public int LikerId { get; set; }
        public CurrentUser Liker { get; set; }

        // Foreign key to refer to the user who is liked
        public int LikedUserId { get; set; }
        public CurrentUser LikedUser { get; set; }
    }
}
