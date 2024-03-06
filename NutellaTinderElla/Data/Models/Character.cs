using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderEllaApi.Data.Models
{
    //Create table name
    [Table(nameof(Character))]
    //Define the structure of the data that will be stored in the database. 
    public class Character
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string FullName { get; set; } = null!;
        [StringLength(100)]
        public string Alias { get; set; } = null!;
        [StringLength(25)]
        public string Gender { get; set; } = null!;
        [StringLength(255)]
        public string Picture { get; set; } = null!;

        ///Navigation / Foreign Keys
        public ICollection<Movie>? Movies { get; set; }

    }
}
