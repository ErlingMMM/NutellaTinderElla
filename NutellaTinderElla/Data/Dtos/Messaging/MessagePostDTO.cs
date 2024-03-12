using System.ComponentModel.DataAnnotations;

namespace NutellaTinderElla.Data.Dtos.Messaging
{
    public class MessagePostDTO
    {
        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty;

    }
}
