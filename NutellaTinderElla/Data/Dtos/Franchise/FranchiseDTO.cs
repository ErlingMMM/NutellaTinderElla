using NutellaTinderEllaApi.Data.Dtos.Movie;

namespace NutellaTinderEllaApi.Data.Dtos.Franchise
{
    public class FranchiseDTO
    {
        //Used for transferring data between the client and the server.
        //Allow you to control what data is exposed to clients and provide a clear structure for data exchange.
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<MovieDTO>? Movies { get; set; }
    }
}
