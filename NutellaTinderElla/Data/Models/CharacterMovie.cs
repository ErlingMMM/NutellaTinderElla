namespace NutellaTinderEllaApi.Data.Models
{
    //Define the structure of the data that will be stored in the database. 
    public class CharacterMovie
    {
        public int CharacterId { get; set; }
        public Character? Characters { get; set; }
        public int MoviesId { get; set; }
        public Movie? Movies { get; set; }
    }
}
