using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderElla.Data.Models
{
    public class Dislike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        // Foreign key to refer to the user who likes
        public int DislikerId { get; set; }
        public CurrentUser Disliker { get; set; }

        // Foreign key to refer to the user who is liked
        public int DislikedUserId { get; set; }
        public CurrentUser DislikedUser { get; set; }
    }
}
