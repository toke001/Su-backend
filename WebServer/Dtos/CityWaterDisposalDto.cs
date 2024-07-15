using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityWaterDisposalDto
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        public int? KolAbonent { get; set; }
        public int? KolFizLic { get; set; }
        public int? KolYriLic { get; set; }
        public int? KolBydzhetOrg { get; set; }
        public int? KolChelOhvatCentrVodo { get; set; }
        public decimal? DostypCentrVodo { get; set; }
        public int? KolichKanaliz { get; set; }
        public int? KolichKanalizMechan { get; set; }
        public int? KolichKanalizMechanBiolog { get; set; }
        public int? ProizvodKanaliz { get; set; }
        public decimal? IznosKanaliz { get; set; }
        public int? KolChelKanaliz { get; set; }
        public decimal? OhvatChelKanaliz { get; set; }
        public int? FactPostypKanaliz { get; set; }
        public int? FactPostypKanaliz1kv { get; set; }
        public int? FactPostypKanaliz2kv { get; set; }
        public int? FactPostypKanaliz3kv { get; set; }
        public int? FactPostypKanaliz4kv { get; set; }
        public int? ObiemKanalizNormOchist { get; set; }
        public int? UrovenNormOchishVody { get; set; }
        public int? AutoProccesSetKanaliz { get; set; }
        public int? AutoProccesKanalizNasos { get; set; }
        public int? AutoProccesKanalizSooruzh { get; set; }
    }
}
