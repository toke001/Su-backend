using WebServer.Emuns;

namespace WebServer.Dtos
{
    public class DataTableDto
    {
        public Guid ApprovedFormId { get; set; }
        public Guid ApprovedFormItemId { get; set; }
        public Guid ApproverFormColumnId { get; set; }
        public Guid Id { get; set; }
        public int ValueType { get; set; }
        public string ValueJson { get; set; }
    }
}
