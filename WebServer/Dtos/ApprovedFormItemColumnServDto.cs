using Microsoft.EntityFrameworkCore;
using WebServer.Models;
using static WebServer.Emuns.Enums;

namespace WebServer.Dtos
{
    public class ApprovedFormItemColumnServDto
    {
        public Guid Id { get; set; }
        public Guid ApprovedFormItemId { get; set; }
        public DataTypeEnum DataType { get; set; }
        public int Length { get; set; }
        public bool Nullable { get; set; }
        public string? Name { get; set; }
        public string? NameKk { get; set; }
        public string? NameRu { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public ColumnLayout? Layout { get; set; }
        public string? ReportCode { get; set; }
    }
}
