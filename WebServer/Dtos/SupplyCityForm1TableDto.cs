namespace WebServer.Dtos
{
    public class SupplyCityForm1TableDto: FormKatoDto
    {
        public Guid FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }
        public decimal Volume { get; set; }
        public bool? HasStreets { get; set; }
        
    }
}
