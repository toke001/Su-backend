namespace WebServer.Dtos
{
    public class AccountSignInRequestDto
    {
        public string login { get; set; }
        public string pwd { get; set; }
        public bool rem { get; set; } = true;
    }
}
