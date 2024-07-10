using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class WasteCityForm1TableDto : FormKatoDto
    {
        public Guid? FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }

        [Comment("Объем воды в тысячах кубических метров.")]
        public double WaterVolume { get; set; }
        public bool HasStreets { get; set; }

    }
}
