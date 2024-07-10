namespace WebServer.Models
{
    public class Account:Base
    {
        public string Login { get; set; }
        public string? Bin { get; set; }
        public string PasswordHash { get; set; }
        public string? FullNameRu { get; set; }
        public string? FullNameKk { get; set; }
        public string? StreetName { get; set; }
        public string? BuildingNumber { get; set; }
        public long KatoCode { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
