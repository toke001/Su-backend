namespace WebServer.Dtos
{
    public class FormDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public string Status { get; set; }
        public string? LinkToForm { get; set; }
        public bool HasStreets { get; set; }
    }
}
