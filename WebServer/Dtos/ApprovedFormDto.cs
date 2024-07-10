using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class ApprovedFormDto
    {
        public Guid Id { get; set; }

        [Comment("Информация о создании группы форм")]
        public String Description { get; set; } = "";
        [Comment("Дата утверждения")]
        public DateTime ApprovalDate { get; set; } = DateTime.Now;
        [Comment("Дата завершения утвержденной формы")]
        public DateTime? CompletionDate { get; set; }
    }
}
