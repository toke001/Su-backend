using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class SeloForms
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Статус села опорное
        /// </summary>
        [Comment("Статус села опорное")]
        public bool? StatusOpor { get; set; }
        /// <summary>
        /// Статус села спутниковое
        /// </summary>
        [Comment("Статус села спутниковое")]
        public bool? StatusSput { get; set; }
        /// <summary>
        /// Статус села прочие 
        /// </summary>
        [Comment("Статус села прочие")]
        public string? StatusProch { get; set; }
        /// <summary>
        /// Статус села приграничные
        /// </summary>
        [Comment("Статус села приграничные")]
        public bool? StatusPrigran { get; set; }
        /// <summary>
        /// Общее количество сельских населенных пунктов в области(единиц)
        /// </summary>
        [Comment("Общее количество сельских населенных пунктов в области(единиц)")]
        public int? ObshKolSelNasPun { get; set; }
        /// <summary>
        /// Общая численность населения в сельских населенных пунктах (человек)
        /// </summary>
        [Comment("Общая численность населения в сельских населенных пунктах (человек)")]
        public int? ObshKolChelNasPun { get; set; }
        /// <summary>
        /// Общее количество домохозяйств (квартир, ИЖД)
        /// </summary>
        [Comment("Общее количество домохозяйств (квартир, ИЖД)")]
        public int? ObshKolDomHoz { get; set; }
        /// <summary>
        /// Год постройки системы водоснабжения
        /// </summary>
        [Comment("Год постройки системы водоснабжения")]
        public DateTime? YearSystVodoSnab { get; set; }
        /// <summary>
        /// Обслуживающее предприятие
        /// </summary>
        [Comment("Обслуживающее предприятие")]
        public Guid? ObslPredpId { get; set; }
        /// <summary>
        /// в чьей собственности находится
        /// </summary>
        [Comment("в чьей собственности находится")]
        public Guid? SobstId { get; set; }
    }
}
