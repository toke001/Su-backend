using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityNetworkLengthDto
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }        
        public int? VodoProvodLengthTotal { get; set; }
        public int? VodoProvodLengthIznos { get; set; }
        public decimal? VodoProvodIznosPercent { get; set; }
        public int? KanalizLengthTotal { get; set; }
        public int? KanalizLengthIznos { get; set; }
        public decimal? KanalizIznosPercent { get; set; }
        public int? ObshNewSetiVodo { get; set; }
        public int? ObshNewSetiKanaliz { get; set; }
        public int? ObshZamenSetiVodo { get; set; }
        public int? ObshZamenSetiKanaliz { get; set; }
        public int? ObshRemontSetiVodo { get; set; }
        public int? ObshRemontSetiKanaliz { get; set; }
    }
}
