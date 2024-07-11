using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class SeloFormsDto
    {
        public Guid Id { get; set; }
        public bool? StatusOpor { get; set; }
        public bool? StatusSput { get; set; }
        public string? StatusProch { get; set; }
        public bool? StatusPrigran { get; set; }
        public int? ObshKolSelNasPun { get; set; }
        public int? ObshKolChelNasPun { get; set; }
        public int? ObshKolDomHoz { get; set; }
        public DateTime? YearSystVodoSnab { get; set; }
        public Guid? ObslPredpId { get; set; }
        public Guid? SobstId { get; set; }
    }
}
