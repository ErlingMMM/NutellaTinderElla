
using NutellaTinderElla.Data.Dtos.Matching;


namespace NutellaTinderElla.Data.Dtos.ActiveUser
{
    public class CurrentUserDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Picture { get; set; } = null!;

        public int GenderPreference { get; set; }

        public int Gender { get; set; }

        public int Seeking { get; set; }

        public string Bio { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int Age { get; set; }

        public List<LikesDTO>? Likes { get; set; }
        public List<DislikesDTO>? Dislikes { get; set; }
        public List<MatchesDTO>? Matches { get; set; }

    }
}

