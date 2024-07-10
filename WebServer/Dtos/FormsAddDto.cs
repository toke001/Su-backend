namespace WebServer.Dtos
{
    public class FormsAddDto
    {
        public int RefKatoId { get; set; }
        public int? SupplierId { get; set; }
        public int ReportYearId { get; set; }
        public int ReportMonthId { get; set; }
        public String? Description { get; set; }
    }
}
