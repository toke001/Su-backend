using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class SupplyCityForm3TableDto : FormKatoDto
    {
        public Guid FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }
        [Comment("всего с нарастающим (единиц)")]
        public int? CoverageMetersTotalCumulative { get; set; } = 0;
        [Comment("в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)")]
        public int? CoverageMetersRemoteData { get; set; } = 0;
        public bool HasStreets { get; set; }
        public bool IsVillage { get; set; }
        #region Село
        public int? RuralPopulation { get; set; }

        [Comment("Количество сельских населенных пунктов (единиц)")]
        public int? RuralSettlementsCount { get; set; } = 0;

        [Comment("Численность населения, проживающего в сельских населенных пунктах, где установлены КБМ (человек)")]
        public int? PopulationWithKBM { get; set; } = 0;

        [Comment("Численность населения, проживающего в сельских населенных пунктах, где установлены ПРВ (человек)")]
        public int? PopulationWithPRV { get; set; } = 0;

        [Comment("Численность населения, проживающего в сельских населенных пунктах, где используют привозную воду")]
        public int? PopulationUsingDeliveredWater { get; set; } = 0;

        [Comment("Численность населения, проживающего в сельских населенных пунктах, где используют воду из скважин и колодцев")]
        public int? PopulationUsingWellsAndBoreholes { get; set; } = 0;

        [Comment("Количество сельских населенных пунктов, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ (наличие протоколов отказа)")]
        public int? RuralSettlementsWithConstructionRefusalProtocols { get; set; } = 0;

        [Comment("Численность населения, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ (наличие протоколов отказа)")]
        public int? PopulationWithConstructionRefusalProtocols { get; set; } = 0;
        #endregion
    }
}
