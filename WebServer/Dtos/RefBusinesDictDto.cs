namespace WebServer.Dtos
{
    public class RefBusinesDictDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public required string Code { get; set; }
        public required string Type { get; set; }
        public string? BusinessDecription { get; set; }
        public string? NameRu { get; set; }
        public string? NameKk { get; set; }
        public string? DescriptionKk { get; set; }
        public string? DescriptionRu { get; set; }
        public bool IsDel { get; set; }
    }
}
