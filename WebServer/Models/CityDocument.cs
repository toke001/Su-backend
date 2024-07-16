using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class CityDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Главная форма город
        /// </summary>
        [Comment("Главная форма город")]
        public Guid? CityFormId { get; set; }
        public virtual CityForms? CityForm { get; set; }
        /// <summary>
        /// Код населенного пункта (КАТО)
        /// </summary>
        [Comment("Код населенного пункта (КАТО)")]
        public required string KodNaselPunk { get; set; }
        /// <summary>
        /// Код обалсти (КАТО)
        /// </summary>
        [Comment("Код обалсти (КАТО)")]
        public string? KodOblast { get; set; }
        /// <summary>
        /// Код района (КАТО)
        /// </summary>
        [Comment("Код района (КАТО)")]
        public string? KodRaiona { get; set; }
        public string? Login { get; set; }
        /// <summary>
        /// За какой год данные
        /// </summary>
        [Comment("За какой год данные")]
        public int Year { get; set; }
    }
}
