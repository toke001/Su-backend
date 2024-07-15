namespace WebServer.Dtos
{
    public class SeloWaterSupplyDto
    {
        /// <summary>
        /// ИД Формы WaterSupplyFormID
        /// </summary>
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
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
    }
}
