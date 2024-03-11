using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderElla.Data.Models
{
    public class Swipes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        // Foreign key to refer to the user who likes
        public int SwiperId { get; set; }
        public User? Swiper { get; set; }

        // Foreign key to refer to the user who is liked
        public int SwipedUserId { get; set; }
        public User? SwipedUser { get; set; }
    }
}
