using System.ComponentModel.DataAnnotations;

namespace WebServer.Dtos
{
    public class AccountSignUpRequestDto
    {
        [Required]
        public required string Login { get; set; }

        [Required]
        public required long KatoCode { get; set; }

        //[Required]
        //public required string Description { get; set; }               
        public required List<int> Roles { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
