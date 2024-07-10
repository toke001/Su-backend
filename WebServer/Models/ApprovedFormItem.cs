using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class ApprovedFormItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid ApprovedFormId { get; set; }
        public virtual ApprovedForm? ApprovedForm { get; set; }

        [Comment("Сервис. 0 - водоснабжение, 1- водоотведение, 2- водопровод")]
        public int ServiceId { get; set; } = 0;

        [Comment("Заголовок Формы (короткий)")]
        public String Title { get; set; } = "";

        [Comment("Порядок отображения")]
        public int DisplayOrder { get; set; } = 1;

        [Comment("Признак села")]
        public bool IsVillage { get; set; } = false;

        [Comment("Идентификатор удаления")]
        public bool IsDel { get; set; } = false;
        public ICollection<ApprovedFormItemColumn> Columns { get; set; }

    }
}
