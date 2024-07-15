using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityFormsDto
    {
        public Guid Id { get; set; }
        
        public int? TotalCountCityOblast { get; set; }
        
        public int? TotalCountDomHoz { get; set; }
        
        public int? TotalCountChel { get; set; }
        
        public Guid? ObslPredpId { get; set; }
    }
}
