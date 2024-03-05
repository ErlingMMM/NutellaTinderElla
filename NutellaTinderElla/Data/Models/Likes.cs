using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace NutellaTinderElla.Data.Models
{
    public class Likes
    {
        //for good practice
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public Enum GenderPreference { get; set; } = null!;

        public Enum Gender { get; set; } = null!;

        public Enum Seeking { get; set; } = null!;


        [StringLength(255)]
        public string Bio { get; set; } = null!;
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string Picture { get; set; } = null!;
        public int Age { get; set; }

        public ICollection<CurrentUser>? CurrentUser { get; set; }

    }
}
