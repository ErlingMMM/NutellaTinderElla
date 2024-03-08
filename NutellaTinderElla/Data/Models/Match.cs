using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderElla.Data.Models
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        // Foreign key to refer to the user who likes
        public int MacherId { get; set; }
        public User Matcher { get; set; }

        // Foreign key to refer to the user who is liked
        public int MatchedUserId { get; set; }
        public User MatchedUser { get; set; }
    }
}