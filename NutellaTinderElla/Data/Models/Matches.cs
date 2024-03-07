using NutellaTinderEllaApi.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace NutellaTinderElla.Data.Models
{
    public class Matches
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public GenderPreferenceEnum GenderPreference { get; set; }

        public enum GenderPreferenceEnum
        {
            Male,
            Female,
            Other
        }

        public GenderEnum Gender { get; set; }

        public enum GenderEnum
        {
            Male,
            Female,
            Other
        }

        public SeekingEnum Seeking { get; set; }

        public enum SeekingEnum
        {
            Casual,
            Relationship,
            Friendship,
            Networking,
            ActivityPartner,
            Experimenting
        }


        [StringLength(255)]
        public string Bio { get; set; } = null!;
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string Picture { get; set; } = null!;
        public int Age { get; set; }

        public ICollection<User>? User { get; set; }
    }
}
