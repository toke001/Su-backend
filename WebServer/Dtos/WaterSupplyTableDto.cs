namespace WebServer.Dtos
{
    public class WaterSupplyTableDto
    {
        /// <summary>
        /// ИД Формы WaterSupplyFormID
        /// </summary>
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool HasStreets { get; set; }

    }
}
