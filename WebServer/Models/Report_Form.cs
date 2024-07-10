using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    /// <summary>
    /// отчет водоснабжение город
    /// </summary>
    public class Report_Form : Base
    {
        public Guid ApprovedFormId { get; set; }
        public ApprovedForm ApprovedForm { get; set; }
        public int RefKatoId { get; set; }
        public virtual Ref_Kato? RefKato { get; set; }
        public int ReportYearId { get; set; }
        public int ReportMonthId { get; set; }
        public int RefStatusId { get; set; }
        public virtual Ref_Status? RefStatus { get; set; }

        [Comment("Наличие улиц в селе")]
        public bool HasStreets { get; set; } = false;
    }
}