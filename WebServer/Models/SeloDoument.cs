namespace WebServer.Models
{
    public class SeloDoument
    {
        public Guid Id { get; set; }
        public Guid? SeloFormId { get; set; }
        public virtual SeloForms SeloForm { get; set; }
        public required string KodNaselPunk { get; set; }
        public string NameNaselPunk { get; set; }
        public string? KodOblast { get; set; }
        public string? KodRaiona { get; set; }
        public int? UserId { get; set; }
        public int Year { get; set; }
    }
}
