using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityFormDto
    {
        public Guid Id { get; set; }        
        public int? TotalCountCityOblast { get; set; }        
        public int? TotalCountDomHoz { get; set; }        
        public int? TotalCountChel { get; set; }        
        public Guid? ObslPredpId { get; set; }
        public int? VodoSnabKolichAbonent { get; set; }
        public int? VodoSnabKolFizLic { get; set; }
        public int? VodoSnabKolYriLic { get; set; }
        public int? VodoSnabKolBydzhOrg { get; set; }
        public int? VodoSnabKolChelDostyp { get; set; }
        public decimal? VodoSnabObespechCentrlVodo { get; set; }
        public int? VodoSnabIndivUchetVodyVsego { get; set; }
        public int? VodoSnabIndivUchetVodyDistance { get; set; }
        public decimal? VodoSnabIndivUchetVodyPercent { get; set; }
        public int? VodoSnabObshePodlezhashKolZdan { get; set; }
        public int? VodoSnabObsheUstanKolZdan { get; set; }
        public int? VodoSnabObsheUstanPriborKol { get; set; }
        public int? VodoSnabObsheUstanDistanceKol { get; set; }
        public decimal? VodoSnabObsheOhvatPercent { get; set; }
        public int? VodoSnabAutoProccesVodoZabor { get; set; }
        public int? VodoSnabAutoProccesVodoPodgot { get; set; }
        public int? VodoSnabAutoProccesNasosStanc { get; set; }
        public int? VodoSnabAutoProccesSetVodosnab { get; set; }

        public int? VodoOtvKolAbonent { get; set; }
        public int? VodoOtvKolAbonFizLic { get; set; }
        public int? VodoOtvKolAbonYriLic { get; set; }
        public int? VodoOtvKolBydzhetOrg { get; set; }
        public int? VodoOtvKolChelOhvatCentrVodo { get; set; }
        public decimal? VodoOtvDostypCentrVodo { get; set; }
        public int? VodoOtvKolichKanaliz { get; set; }
        public int? VodoOtvKolichKanalizMechan { get; set; }
        public int? VodoOtvKolichKanalizMechanBiolog { get; set; }
        public int? VodoOtvProizvodKanaliz { get; set; }
        public decimal? VodoOtvIznosKanaliz { get; set; }
        public int? VodoOtvKolChelKanaliz { get; set; }
        public decimal? VodoOtvOhvatChelKanaliz { get; set; }
        public int? VodoOtvFactPostypKanaliz { get; set; }
        public int? VodoOtvFactPostypKanaliz1kv { get; set; }
        public int? VodoOtvFactPostypKanaliz2kv { get; set; }
        public int? VodoOtvFactPostypKanaliz3kv { get; set; }
        public int? VodoOtvFactPostypKanaliz4kv { get; set; }
        public int? VodoOtvObiemKanalizNormOchist { get; set; }
        public int? VodoOtvUrovenNormOchishVody { get; set; }
        public int? VodoOtvAutoProccesSetKanaliz { get; set; }
        public int? VodoOtvAutoProccesKanalizNasos { get; set; }
        public int? VodoOtvAutoProccesKanalizSooruzh { get; set; }

        public int? UrTarVodoSnabUsrednen { get; set; }
        public int? UrTarVodoSnabFizLic { get; set; }
        public int? UrTarVodoSnabYriLic { get; set; }
        public int? UrTarVodoSnabBydzhOrg { get; set; }
        public int? UrTarVodoOtvedUsred { get; set; }
        public int? UrTarVodoOtvedFizLic { get; set; }
        public int? UrTarVodoOtvedYriLic { get; set; }
        public int? UrTarVodoOtvedBydzhOrg { get; set; }

        public int? LengthVodoProvodTotal { get; set; }
        public int? LengthVodoProvodIznos { get; set; }
        public decimal? LengthVodoProvodIznosPercent { get; set; }
        public int? LengthKanalizTotal { get; set; }
        public int? LengthKanalizIznos { get; set; }
        public decimal? LengthKanalizIznosPercent { get; set; }
        public int? LengthObshNewSetiVodo { get; set; }
        public int? LengthObshNewSetiKanaliz { get; set; }
        public int? LengthObshZamenSetiVodo { get; set; }
        public int? LengthObshZamenSetiKanaliz { get; set; }
        public int? LengthObshRemontSetiVodo { get; set; }
        public int? LengthObshRemontSetiKanaliz { get; set; }
    }
}
