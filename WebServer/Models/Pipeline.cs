using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class Pipeline : Base
    {
        public Guid FormId { get; set; }
        public virtual Report_Form Form { get; set; }

        [Comment("Протяженность водопроводных сетей, км (по состоянию на конец отчетного года),общая, км")]
        public decimal TotalPipelineLength { get; set; } = 0;

        [Comment("в том числе изношенных, км")]
        public decimal WornPipelineLength { get; set; } = 0;

        [Comment("Протяженность канализационных сетей, км (по состоянию на конец отчетного года),общая, км")]
        public decimal TotalSewerNetworkLength { get; set; } = 0;

        [Comment("в том числе изношенных, км")]
        public decimal WornSewerNetworkLength { get; set; } = 0;

        [Comment("Общая протяженность построенных (новых) сетей в отчетном году, км, водоснабжения, км")]
        public decimal NewWaterSupplyNetworkLength { get; set; } = 0;

        [Comment("водоотведения, км")]
        public decimal NewWastewaterNetworkLength { get; set; } = 0;

        [Comment("Общая протяженность реконструированных (замененных) сетей в отчетном году, км, водоснабжения, км")]
        public decimal ReconstructedNetworkLength { get; set; } = 0;

        [Comment("водоотведения, км")]
        public decimal ReconstructedWastewaterNetworkLength { get; set; } = 0;

        [Comment("Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км, водоснабжения, км")]
        public decimal RepairedWaterSupplyNetworkLength { get; set; } = 0;

        [Comment("водоотведения, км")]
        public decimal RepairedWastewaterNetworkLength { get; set; } = 0;

        [Comment("численность населения (вся)")]
        public decimal TotalPopulation { get; set; } = 0;
    }
}
