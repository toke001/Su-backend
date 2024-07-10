using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Dtos
{
    public class ReportsDto
    {
        public Guid Id { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDel { get; set; } = false;

        [Comment("Примечания")]
        public String Description { get; set; } = "";
        public int RefKatoId { get; set; }
        public int ReportYearId { get; set; }
        public int ReportMonthId { get; set; }
        public int RefStatusId { get; set; }
        public string RefStatusLabel { get; set; }

        [Comment("Наличие улиц в селе")]
        public bool HasStreets { get; set; } = false;
    }
}
