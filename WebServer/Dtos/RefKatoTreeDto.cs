using WebServer.Models;

namespace WebServer.Dtos
{
    public class RefKatoTreeDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        /// <summary>
        /// код
        /// </summary>
        public long Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Возможность создания отчета для данного н.п.
        /// </summary>
        public bool IsReportable { get; set; } = false;
        public int? KatoLevel { get; set; }

        public string? Description { get; set; }
        public virtual List<RefKatoTreeDto> Children { get; set; } = new List<RefKatoTreeDto>();
    }
}
