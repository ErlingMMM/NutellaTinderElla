namespace NutellaTinderElla.Data.Dtos.User
{
    public class UserPublicDataDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Picture { get; set; } = null!;

        public int Gender { get; set; }

        public int Seeking { get; set; }

        public string Bio { get; set; } = null!;

        public int Age { get; set; }

    }
}

