using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityTarifDto
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        public int? VodoSnabUsrednen { get; set; }
        public int? VodoSnabFizLic { get; set; }
        public int? VodoSnabYriLic { get; set; }
        public int? VodoSnabBydzhOrg { get; set; }
        public int? VodoOtvedUsred { get; set; }
        public int? VodoOtvedFizLic { get; set; }
        public int? VodoOtvedYriLic { get; set; }
        public int? VodoOtvedBydzhOrg { get; set; }
    }
}
