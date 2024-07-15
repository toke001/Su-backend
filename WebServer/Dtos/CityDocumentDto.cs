using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Dtos
{
    public class CityDocumentDto
    {
        public Guid Id { get; set; }        
        public Guid? CityFormId { get; set; }
        public required string KodNaselPunk { get; set; }
        public string? KodOblast { get; set; }
        public string? KodRaiona { get; set; }
        public int? UserId { get; set; }
        public int Year { get; set; }
    }
}
