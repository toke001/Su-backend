namespace WebServer.Models
{
    public class Ref_Base
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string? NameKk { get; set; }
        public bool IsDel { get; set; } = false;
        public string? Description { get; set; }
    }
}
