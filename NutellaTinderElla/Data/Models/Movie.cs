using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMovieApi.Data.Models
{
    //Define the structure of the data that will be stored in the database. 
    [Table(nameof(Movie))]
    public class Movie
    {
        //for good practice
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(50)]
        public string Genre { get; set; } = null!;
        [StringLength(4)]
        public string ReleaseYear { get; set; } = null!;
        [StringLength(50)]
        public string Director { get; set; } = null!;
        [StringLength(255)]
        public string Picture { get; set; } = null!;
        [StringLength (255)]
        public string Trailer { get; set; } = null!;

        //Navigation / Foreign Keys
        [ForeignKey("Franchise")]
        public int? FranchiseId { get; set; }
        public Franchise? Franchise { get; set; }
        public ICollection<Character>? Characters { get; set; }
    }
}
