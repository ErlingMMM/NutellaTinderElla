
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutellaTinderEllaApi.Data.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SenderId { get; set; }
        public User? Sender { get; set; }

        public int ReceiverId { get; set; }
        public User? Receiver { get; set; }
        public byte[]? IV { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
        public bool IsViewed { get; set; } = false;

        public DateTime TimeViewed { get; set; }

        public bool IsLiked { get; set; } = false;

    }
}
