namespace WebServer.Models
{
    public class Ref_Building:Ref_Base
    {
        public int RefStreetId { get; set; }
        public virtual Ref_Street? RefStreet { get; set; }
        public string Building { get; set; }
    }
}
