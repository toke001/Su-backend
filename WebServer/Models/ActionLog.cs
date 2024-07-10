namespace WebServer.Models
{
    public class ActionLog
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid FormId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public string? Error { get; set; }
    }
}
