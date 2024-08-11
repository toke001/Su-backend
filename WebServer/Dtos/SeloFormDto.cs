namespace WebServer.Dtos
{
    public class SeloFormDto
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public bool? StatusOpor { get; set; }
        public bool? StatusSput { get; set; }
        public string? StatusProch { get; set; }
        public bool? StatusPrigran { get; set; }
        public int? ObshKolSelNasPun { get; set; }
        public int? ObshKolChelNasPun { get; set; }
        public int? ObshKolDomHoz { get; set; }
        public string? YearSystVodoSnab { get; set; }
        public Guid? ObslPredpId { get; set; }
        public Guid? SobstId { get; set; }
        public int? DosVodoSnabKolPunk { get; set; }
        public int? DosVodoSnabKolChel { get; set; }
        public decimal? DosVodoSnabPercent { get; set; }
        public int? CentrVodoSnabKolNasPun { get; set; }
        public int? CentrVodoSnabKolChel { get; set; }
        public decimal? CentrVodoSnabObesKolNasPunk { get; set; }
        public decimal? CentrVodoSnabObesKolChel { get; set; }
        public int? CentrVodoSnabKolAbon { get; set; }
        public int? CentrVodoSnabFizLic { get; set; }
        public int? CentrVodoSnabYriLic { get; set; }
        public int? CentrVodoSnabBudzhOrg { get; set; }
        public int? CentrVodoIndivPriborUchVodyVsego { get; set; }
        public int? CentrVodoIndivPriborUchVodyASYE { get; set; }
        public decimal? CentrVodoIndivPriborUchVodyOhvat { get; set; }
        public int? NeCtentrVodoKolSelsNasPunk { get; set; }
        public int? KbmKolSelsNasPunk { get; set; }
        public int? KbmKolChel { get; set; }
        public decimal? KbmObespNasel { get; set; }
        public int? PrvKolSelsNasPunk { get; set; }
        public int? PrvKolChel { get; set; }
        public decimal? PrvObespNasel { get; set; }
        public int? PrivVodaKolSelsNasPunk { get; set; }
        public int? PrivVodaKolChel { get; set; }
        public decimal? PrivVodaObespNasel { get; set; }
        public int? SkvazhKolSelsNasPunk { get; set; }
        public int? SkvazhKolChel { get; set; }
        public decimal? SkvazhObespNasel { get; set; }
        public int? SkvazhKolSelsNasPunkOtkaz { get; set; }
        public int? SkvazhKolChelOtkaz { get; set; }
        public decimal? SkvazhDolyaNaselOtkaz { get; set; }
        public decimal? SkvazhDolyaSelOtkaz { get; set; }
        public int? CentrVodOtvedKolSelsNasPunk { get; set; }
        public int? CentrVodOtvedKolChel { get; set; }
        public int? CentrVodOtvedKolAbonent { get; set; }
        public int? CentrVodOtvedFizLic { get; set; }
        public int? CentrVodOtvedYriLic { get; set; }
        public int? CentrVodOtvedBydzhOrg { get; set; }
        public decimal? CentrVodOtvedDostypKolNasPunk { get; set; }
        public decimal? CentrVodOtvedDostypKolChel { get; set; }
        public int? CentrVodOtvedNalich { get; set; }
        public int? CentrVodOtvedNalichMechan { get; set; }
        public int? CentrVodOtvedNalichMechanBiolog { get; set; }
        public int? CentrVodOtvedProizvod { get; set; }
        public decimal? CentrVodOtvedIznos { get; set; }
        public int? CentrVodOtvedOhvatKolChel { get; set; }
        public decimal? CentrVodOtvedOhvatNasel { get; set; }
        public int? CentrVodOtvedFactPostypStochVod { get; set; }
        public int? CentrVodOtvedFactPostypStochVod1 { get; set; }
        public int? CentrVodOtvedFactPostypStochVod2 { get; set; }
        public int? CentrVodOtvedFactPostypStochVod3 { get; set; }
        public int? CentrVodOtvedFactPostypStochVod4 { get; set; }
        public int? CentrVodOtvedObiemStochVod { get; set; }
        public decimal? CentrVodOtvedUrovenNorm { get; set; }
        public int? DecentrVodoOtvedKolSelsNasPunk { get; set; }
        public int? DecentrVodoOtvedKolChel { get; set; }
        public int? TarifVodoSnabUsred { get; set; }
        public int? TarifVodoSnabFizL { get; set; }
        public int? TarifVodoSnabYriL { get; set; }
        public int? TarifVodoSnabBudzh { get; set; }
        public int? TarifVodoOtvedUsred { get; set; }
        public int? TarifVodoOtvedFizL { get; set; }
        public int? TarifVodoOtvedYriL { get; set; }
        public int? TarifVodoOtvedBudzh { get; set; }
        public int? ProtyzhVodoSeteyObsh { get; set; }
        public int? ProtyzhVodoSeteyVtomIznos { get; set; }
        public decimal? ProtyzhVodoSeteyIznos { get; set; }

        public int? ProtyzhKanalSeteyObsh { get; set; }
        public int? ProtyzhKanalSeteyVtomIznos { get; set; }
        public decimal? ProtyzhKanalSeteyIznos { get; set; }

        public int? ProtyzhNewSeteyVodoSnab { get; set; }
        public int? ProtyzhNewSeteyVodoOtved { get; set; }

        public int? ProtyzhRekonSeteyVodoSnab { get; set; }
        public int? ProtyzhRekonSeteyVodoOtved { get; set; }

        public int? ProtyzhRemontSeteyVodoSnab { get; set; }
        public int? ProtyzhRemontSeteyVodoOtved { get; set; }
    }
}
