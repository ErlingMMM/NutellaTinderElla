namespace NutellaTinderElla.Data.Dtos.Messaging
{
    public class MessageDTO
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; } = string.Empty;

        public bool IsViewed { get; set; }
        public bool IsLiked { get; set; }

        public DateTime Timestamp { get; set; }
        public DateTime TimeViewed { get; set; }

    }
}
