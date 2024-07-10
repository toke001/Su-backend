using WebServer.Models;

namespace WebServer.Dtos
{
    public class SupplyCityForm4TableDto : FormKatoDto
    {
        public Guid FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }
        public int CoverageHouseholdNeedNumberBuildings { get; set; } = 0;
        public int CoverageHouseholdInstalledBuildings { get; set; } = 0;
        public int CoverageHouseholdInstalledCount { get; set; } = 0;
        public int CoverageHouseholdRemoteData { get; set; } = 0;
        public bool HasStreets { get; set; }        
    }
}
