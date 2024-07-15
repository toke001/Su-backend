using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityWaterSupplyDto
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        public int? KolichAbonent { get; set; }
        public int? KolFizLic { get; set; }
        public int? KolYriLic { get; set; }
        public int? KolBydzhOrg { get; set; }
        public int? KolChelDostyp { get; set; }
        public decimal? ObespechCentrlVodo { get; set; }
        public int? IndivUchetVodyVsego { get; set; }
        public int? IndivUchetVodyDistance { get; set; }
        public decimal? IndivUchetVodyPercent { get; set; }
        public int? ObshePodlezhashKolZdan { get; set; }
        public int? ObsheUstanKolZdan { get; set; }
        public int? ObsheUstanPriborKol { get; set; }
        public int? ObsheUstanDistanceKol { get; set; }
        public decimal? ObsheOhvatPercent { get; set; }
        public int? AutoProccesVodoZabor { get; set; }
        public int? AutoProccesVodoPodgot { get; set; }
        public int? AutoProccesNasosStanc { get; set; }
        public int? AutoProccesSetVodosnab { get; set; }
    }
}
