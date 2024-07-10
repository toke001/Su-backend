using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class WasteCityForm2TableDto : FormKatoDto
    {
        public Guid? FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }

        [Comment("Охваченные централизованным водоотведением (0-1)")]
        public bool? IsConnectedToCentralizedWastewaterSystem { get; set; }

        [Comment("Наличие канализационно-очистных сооружений, (1-0)")]
        public bool? HasSewageTreatmentFacilities { get; set; }

        [Comment("С механичес-кой очисткой (1-0)")]
        public bool? HasMechanicalTreatment { get; set; }

        [Comment("С механической и биологической очист-кой (1-0)")]
        public bool? HasMechanicalAndBiologicalTreatment { get; set; }

        [Comment("Численность населения, охваченного централизованным водоотведением, (человек)")]
        public int? PopulationCoveredByCentralizedWastewater { get; set; } = 0;
        public bool HasStreets { get; set; }

        #region Село
        [Comment("Кол-во сельских населенных пунктов (единиц)")]
        public int? RuralSettlementsWithCentralizedWastewater { get; set; } = 0;

        [Comment("Численность населения, проживающего в данных сельских населенных пунктах (человек)")]
        public int? PopulationInRuralSettlements { get; set; } = 0;

        [Comment("Кол-во абонентов, проживающих в данных сельских населенных пунктах (единиц)")]
        public int? SubscribersInRuralSettlements { get; set; } = 0;

        [Comment("в том числе физических лиц/население (единиц)")]
        public int? IndividualSubscribers { get; set; } = 0;

        [Comment("в том числе юридических лиц (единиц)")]
        public int? CorporateSubscribers { get; set; } = 0;
        [Comment("в том числе GovernmentOrganizations")]
        public int? GovernmentOrganizations { get; set; } = 0;

        [Comment("Наличие канализационно- очистных сооружений (единиц)")]
        public int? SewageTreatmentFacilitiesCount { get; set; } = 0;

        [Comment("в том числе только с механичес-кой очисткой (еди-ниц)")]
        public int? MechanicalTreatmentFacilitiesCount { get; set; } = 0;

        [Comment("в том числе с механической и биологической очист-кой (еди-ниц)")]
        public int? MechanicalAndBiologicalTreatmentFacilitiesCount { get; set; } = 0;

        [Comment("Производительность канализационно-очистных сооружений (проектная)")]
        public decimal? SewageTreatmentCapacity { get; set; } = 0;

        [Comment("Износ канализационно- очистных сооружений, в %")]
        public decimal? SewageTreatmentFacilitiesWearPercentage { get; set; } = 0;

        [Comment("Числен-ность населе-ния, охваченного действующими канализационно- очистными сооружениями (человек)")]
        public int? PopulationServedBySewageTreatmentFacilities { get; set; } = 0;

        [Comment("Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3)")]
        public decimal? ActualWastewaterInflux { get; set; } = 0;

        [Comment("Объем сточных вод, соответствующей нормативной очистке по собственному лабораторному мониторингу за отчетный период (тыс.м3)")]
        public decimal? NormativelyTreatedWastewaterVolume { get; set; } = 0;
        #endregion
    }
}
