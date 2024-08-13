using WebServer.Models;

namespace WebServer.Dtos
{
    public class SeloDocumentDto
    {
        public Guid Id { get; set; }
        //public Guid? SeloFormId { get; set; }
        public required string KodNaselPunk { get; set; }
        public string? KodOblastPunk { get; set; }
        public string? KodRaiona { get; set; }
        public string? Login { get; set; }
        public int Year { get; set; }
    }
}
