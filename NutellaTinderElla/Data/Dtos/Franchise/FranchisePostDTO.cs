using System.ComponentModel.DataAnnotations;


namespace NutellaTinderEllaApi.Data.Dtos.Franchise
{
    public class FranchisePostDTO
    {
        //Used for creating resources
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        public string Description { get; set; } = null!;
    }
}







