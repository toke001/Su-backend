namespace WebServer.Dtos
{
    public class NetworkLengthInfoDto
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        public int? ProtyzhVodoSeteyObsh { get; set; }
        public int? ProtyzhVodoSeteyVtomIznos { get; set; }
        public decimal? ProtyzhVodoSeteyIznos { get; set; }

        public int? ProtyzhKanalSeteyObsh { get; set; }
        public int? ProtyzhKanalSeteyVtomIznos { get; set; }
        public decimal? ProtyzhKanalSeteyIznos { get; set; }

        public int? ProtyzhNewSeteyVodoSnab { get; set; }
        public int? ProtyzhNewSeteyVodoOtved { get; set; }

        public int? ProtyzhRekonSeteyVodoSnab { get; set; }
        public int? ProtyzhRekonSeteyVodoOtved { get; set; }

        public int? ProtyzhRemontSeteyVodoSnab { get; set; }
        public int? ProtyzhRemontSeteyVodoOtved { get; set; }
    }
}
