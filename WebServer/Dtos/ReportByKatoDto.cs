using WebServer.Models;

namespace WebServer.Dtos
{
    public class ReportByKatoDto
    {
        public int Kato { get; set; }
        public string Settlement { get;set; }
        public ICollection<Models.Data> Values { get; set; }
        public ICollection<ReportByKatoDto> Childs { get; set; }
    }
}
