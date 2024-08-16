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
        public int? CentrVodoSnabIndivPriborUchVodyVsego { get; set; }
        public int? CentrVodoSnabIndivPriborUchVodyASYE { get; set; }
        public decimal? CentrVodoSnabIndivPriborUchVodyOhvat { get; set; }
        
        public int? NecVodosnabKolSelsNasPunk { get; set; }
        public int? NecVodosnabKbmKolSelsNasPunk { get; set; }
        public int? NecVodosnabKbmKolChel { get; set; }
        public decimal? NecVodosnabKbmObespNasel { get; set; }
        public int? NecVodosnabPrvKolSelsNasPunk { get; set; }
        public int? NecVodosnabPrvKolChel { get; set; }
        public decimal? NecVodosnabPrvObespNasel { get; set; }
        public int? NecVodosnabPrivVodaKolSelsNasPunk { get; set; }
        public int? NecVodosnabPrivVodaKolChel { get; set; }
        public decimal? NecVodosnabPrivVodaObespNasel { get; set; }
        public int? NecVodosnabSkvazhKolSelsNasPunk { get; set; }
        public int? NecVodosnabSkvazhKolChel { get; set; }
        public decimal? NecVodosnabSkvazhObespNasel { get; set; }
        public int? NecVodosnabSkvazhKolSelsNasPunkOtkaz { get; set; }
        public int? NecVodosnabSkvazhKolChelOtkaz { get; set; }
        public decimal? NecVodosnabSkvazhDolyaNaselOtkaz { get; set; }
        public decimal? NecVodosnabSkvazhDolyaSelOtkaz { get; set; }
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
