namespace WebServer.Dtos
{
    public class AccountSignInResponseDto
    {
        public string token { get; set; }
        public string login { get; set; }
        public bool rem { get; set; } = true;
    }
}
