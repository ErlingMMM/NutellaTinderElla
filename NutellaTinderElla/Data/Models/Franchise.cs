using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderEllaApi.Data.Models
{
    //Define the structure of the data that will be stored in the database. 
    [Table(nameof(Franchise))]
    public class Franchise
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        public string Description { get; set; } = null!;

        //Navigation
        public ICollection<Movie> Movies { get; set; }
    }
}
