using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class ApprovedForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Comment("Информация о создании группы форм")]
        public String Description { get; set; } = "";

        [Comment("Дата утверждения")]
        public DateTime ApprovalDate { get; set; } = DateTime.Now;

        [Comment("Дата завершения утвержденной формы")]
        public DateTime? CompletionDate { get; set; }

        [Comment("Идентификатор удаления")]
        public bool IsDel { get; set; } = false;

        [Comment("Идентификатор пользователя удалившего форму")]
        public Guid? DeletedById { get; set; }
        public ICollection<ApprovedFormItem> Items { get; set; }
    }
}
