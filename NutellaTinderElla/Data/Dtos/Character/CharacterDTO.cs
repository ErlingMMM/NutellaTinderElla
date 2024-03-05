using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using NutellaTinderEllaApi.Data.Dtos.Movie;

namespace NutellaTinderEllaApi.Data.Dtos.Character
{
    public class CharacterDTO
    {
        //Used for transferring data between the client and the server.
        //Allow you to control what data is exposed to clients and provide a clear structure for data exchange.
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }

        public List<MovieDTO>? Movies { get; set; }

    }
}
