using System.ComponentModel.DataAnnotations;

namespace WebMovieApi.Data.Dtos.Character
{
    public class CharacterPostDTO
    {
        //Used for creating resources
        [StringLength(100)]
        public string FullName { get; set; } = null!;
        [StringLength(100)]
        public string Alias { get; set; } = null!;
        [StringLength(25)]
        public string Gender { get; set; } = null!;
        [StringLength(255)]
        public string Picture { get; set; } = null!;
    }
}
