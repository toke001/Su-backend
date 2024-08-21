using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Dtos
{
    public class CityDocumentDto
    {
        public Guid Id { get; set; }        
        public Guid? CityFormId { get; set; }
        public string KodNaselPunk { get; set; } = string.Empty;
        public string? KodOblast { get; set; }
        public string? KodRaiona { get; set; }
        public string? Login { get; set; }
        public int Year { get; set; }
    }
}
