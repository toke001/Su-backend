using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class Business_Dictionary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// ключ на ИД (своего типа или стороннего)
        /// </summary>
        [Comment("ключ на ИД (своего типа или стороннего)")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Код*
        /// </summary>
        [Comment("Код*")]
        public required string Code { get; set; }
        /// <summary>
        /// Тип*
        /// </summary>
        [Comment("Тип*")]
        public required string Type { get; set; }
        /// <summary>
        /// Бизнес описание
        /// </summary>
        [Comment("Бизнес описание")]
        public string? BusinessDecription { get; set; }
        /// <summary>
        /// Наименование на каз
        /// </summary>
        [Comment("Наименование на каз")]
        public string? NameKk { get; set; }
        /// <summary>
        /// Наименование на рус
        /// </summary>
        [Comment("Наименование на рус")]
        public string? NameRu { get; set; }
        /// <summary>
        /// Пояснение на каз
        /// </summary>
        [Comment("Пояснение на каз")]
        public string? DescriptionKk { get; set; }
        /// <summary>
        /// Пояснение на рус
        /// </summary>
        [Comment("Пояснение на рус")]
        public string? DescriptionRu { get; set; }
        /// <summary>
        /// Удален
        /// </summary>
        [Comment("Удален")]
        public bool IsDel { get; set; } = false;
    }
}
