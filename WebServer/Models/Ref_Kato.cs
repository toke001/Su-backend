using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class Ref_Kato : Ref_Base
    {
        public int ParentId { get; set; }

        /// <summary>
        /// код
        /// </summary>
        public long Code { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Данные заполненные пользователем
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Возможность создания отчета для данного н.п.
        /// </summary>
        public bool IsReportable { get; set; } = false;
        [Comment("Категории населенных пунктов. 0-область,1-город,2-село,3-район")]
        public int? KatoLevel { get; set; }

        [Comment("Если это район, он смотрит сам на себя, если это село, код района,")]
        public int? ParentRaion { get; set; }

        [Comment("Область Астана,Алматы...сам на себя ссылка, если район код области")]
        public int? ParentObl { get; set; }
    }
}
