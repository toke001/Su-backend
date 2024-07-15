using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class CityForms
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Общее количество		
        /// городов в области (единиц)
        /// </summary>
        [Comment("Общее количество - городов в области (единиц)")]
        public int? TotalCountCityOblast { get; set; }
        /// <summary>
        /// Общее количество		
        /// домохозяйств (кв, ИЖД)
        /// </summary>
        [Comment("Общее количество - домохозяйств (кв, ИЖД)")]
        public int? TotalCountDomHoz { get; set; }
        /// <summary>
        /// Общее количество		
        /// проживающих в городских населенных пунктах (человек)
        /// </summary>
        [Comment("Общее количество - проживающих в городских населенных пунктах (человек)")]
        public int? TotalCountChel { get; set; }
        /// <summary>
        /// Обслуживающее предприятие
        /// </summary>
        [Comment("Обслуживающее предприятие")]
        public Guid? ObslPredpId { get; set; }
    }
}
