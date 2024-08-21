using System.ComponentModel.DataAnnotations;

namespace WebServer.Dtos
{
    public class AccountSignUpRequestDto
    {
        public string Login { get; set; } = string.Empty;
        public long KatoCode { get; set; } = 0;
        public List<int> Roles { get; set; } = new List<int>();              
        public string Email { get; set; } = string.Empty;
                
        public string Password { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
    }
}
