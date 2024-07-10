namespace WebServer.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Bin { get; set; } = "";
        public string FullName { get; set; } = "";
        public int KatoId { get; set; }
        public Ref_Kato Kato { get; set; }
    }
}
