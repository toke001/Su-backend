namespace WebServer.Models
{
    public class ReportSupplier:Base
    {
        public Guid Report_FormId { get; set; }
        public Report_Form Report_Form { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int ServiceId { get; set; } = 0;
    }
}
