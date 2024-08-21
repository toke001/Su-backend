using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class Account:Base
    {
        [Required]
        public string Login { get; set; } = string.Empty;
        public string? Bin { get; set; }
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string? FullNameRu { get; set; }
        public string? FullNameKk { get; set; }
        public string? StreetName { get; set; }
        public string? BuildingNumber { get; set; }
        [Required]
        public long KatoCode { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
