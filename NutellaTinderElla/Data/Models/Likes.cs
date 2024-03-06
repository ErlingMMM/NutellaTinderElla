using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderElla.Data.Models
{
    [Table(nameof(Likes))]

    public class Likes
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public CurrentUser? LoggedInUser { get; set; }
        public int LikedUserId { get; set; }
        public CurrentUser? LikedUser { get; set; }

    }
}
