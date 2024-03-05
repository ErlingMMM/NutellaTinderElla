using System.ComponentModel.DataAnnotations;

namespace NutellaTinderElla.Data.Dtos.ActiveUser
{
    public class CurrentUserPostDTO
    {
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
    }
}
