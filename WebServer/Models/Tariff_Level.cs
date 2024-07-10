using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class Tariff_Level : Base
    {
        public Guid FormId { get; set; }
        public virtual Report_Form Form { get; set; }

        /// <summary>
        /// усредненный, тенге/м3
        /// </summary>
        [Comment("усредненный, тенге/м3")]
        public decimal TariffAverage { get; set; }

        /// <summary>
        /// физическим лицам/населению, тенге/м3
        /// </summary>
        [Comment("физическим лицам/населению, тенге/м3")]
        public decimal TariffIndividual { get; set; }

        /// <summary>
        /// юридическим лицам, тенге/м3
        /// </summary>
        [Comment("юридическим лицам, тенге/м3")]
        public decimal TariffLegal { get; set; }

        /// <summary>
        /// бюджетным организациям, тенге/м3
        /// </summary>
        [Comment("бюджетным организациям, тенге/м3")]
        public decimal TariffBudget { get; set; }
    }
}
