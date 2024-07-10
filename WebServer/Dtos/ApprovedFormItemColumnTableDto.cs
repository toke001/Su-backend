namespace WebServer.Dtos
{
    public class ApprovedFormItemColumnTableDto
    {
        public Guid Id { get; set; }
        public int DataType { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public string Data { get; set; }
    }
}
