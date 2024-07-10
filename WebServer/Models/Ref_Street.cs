namespace WebServer.Models
{
    public class Ref_Street:Ref_Base
    {
        public int RefKatoId { get; set; }
        public virtual Ref_Kato? RefKato { get; set; }
    }
}
