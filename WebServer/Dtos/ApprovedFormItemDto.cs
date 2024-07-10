using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Dtos
{
    public class ApprovedFormItemDto
    {
        public Guid Id { get; set; }
        public Guid ApprovedFormId { get; set; }
        
        public int ServiceId { get; set; } = 0;

        public String Title { get; set; } = "";
                
        public int DisplayOrder { get; set; } = 1;
                
        public bool IsDel { get; set; } = false;
        public string ServiceName { get; set; }
        public bool IsVillage { get; set; } = false;
        public string Url { get; set; }
    }
}
