namespace WebServer.Dtos
{
    public class TariffInfoDto
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        public int? TarifVodoSnabUsred { get; set; }
        public int? TarifVodoSnabFizL { get; set; }
        public int? TarifVodoSnabYriL { get; set; }
        public int? TarifVodoSnabBudzh { get; set; }
        public int? TarifVodoOtvedUsred { get; set; }
        public int? TarifVodoOtvedFizL { get; set; }
        public int? TarifVodoOtvedYriL { get; set; }
        public int? TarifVodoOtvedBudzh { get; set; }
    }
}
