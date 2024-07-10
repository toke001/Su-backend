using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class WasteCityForm3TableDto : FormKatoDto
    {
        public Guid? FormId { get; set; }
        public int? RefStreetId { get; set; }

        [Comment("Сети канализации (0 или 1)")]
        public bool HasSewerNetworks { get; set; }

        [Comment("Канализационные насосные станции (0 или 1)")]
        public bool HasSewagePumpingStations { get; set; }

        [Comment("Канализационно-очистные сооружения (0 или 1)")]
        public bool HasSewageTreatmentPlants { get; set; }
        public bool HasStreets { get; set; }

    }
}
