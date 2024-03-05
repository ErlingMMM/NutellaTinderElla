



using NutellaTinderElla.Data.Dtos.Matching;


namespace NutellaTinderElla.Data.Dtos.CurrentUser
{
    public class CurrentUserDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Picture { get; set; } = null!;

        public Enum Gender { get; set; } = null!;

        public Enum GenderPreference { get; set; } = null!;

        public Enum Seeking { get; set; } = null!;

        public string Bio { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int Age { get; set; }

        public List<LikesDTO>? Likes { get; set; }
        public List<DislikesDTO>? Dislikes { get; set; }
        public List<MatchesDTO>? Matches { get; set; }

    }
}

