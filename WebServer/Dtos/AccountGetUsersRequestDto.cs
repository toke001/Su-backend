namespace WebServer.Dtos
{
    public class AccountGetUsersRequestDto
    {
        public string? Login { get; set; }
        public string? KatoCode { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }        
    }
}
