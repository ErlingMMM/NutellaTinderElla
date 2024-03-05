using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NutellaTinderElla.Data.Models;

namespace NutellaTinderEllaApi.Data.Models
{
    //Define the structure of the data that will be stored in the database. 
    [Table(nameof(CurrentUser))]
    public class CurrentUser
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int GenderPreference { get; set; }

        public int Gender { get; set; }

        public int Seeking { get; set; }

        [StringLength(255)]
        public string Bio { get; set; } = null!;
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string Picture { get; set; } = null!;
        public int Age { get; set; }

        public ICollection<Likes>? Likes { get; set; }
        public ICollection<Dislikes>? Dislikes { get; set; }
        public ICollection<Matches>? Matches { get; set; }
    }
}
