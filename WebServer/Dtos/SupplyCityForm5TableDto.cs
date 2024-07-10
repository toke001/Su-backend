namespace WebServer.Dtos
{
    public class SupplyCityForm5TableDto : FormKatoDto
    {
        public Guid FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }
        public bool ScadaWaterIntake { get; set; } = false;
        public bool ScadaWaterTreatment { get; set; } = false;
        public bool ScadaStations { get; set; } = false;
        public bool ScadaSupplyNetworks { get; set; } = false;
        public bool HasStreets { get; set; }

    }
}
