using System.ComponentModel.DataAnnotations;

namespace NutellaTinderElla.Data.Dtos.Messaging
{
    public class MessagePostDTO
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty;

    }
}
