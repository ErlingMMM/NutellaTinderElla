using System.ComponentModel.DataAnnotations;

namespace WebMovieApi.Data.Dtos.Franchise
{
    public class FranchisePutDTO
    {
        //Used for updating resources
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        public string Description { get; set; } = null!;
    }
}
