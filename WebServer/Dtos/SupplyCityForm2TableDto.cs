using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class SupplyCityForm2TableDto : FormKatoDto
    {
        public Guid FormId { get; set; }
        public int? RefStreetId { get; set; }
        public int? RefBuildingId { get; set; }
        public bool HasStreets { get; set; }

        public bool IsVillage { get; set; } = false;

        #region City
        /// <summary>
        /// Охваченные централизованным водоснабжением (0-1)
        /// </summary>
        [Comment("Охваченные централизованным водоснабжением (0-1)")]
        public bool? CoverageWater { get; set; } = false;

        /// <summary>
        /// Количество населения имеющих доступ к централизованному водоснабжению (человек)
        /// </summary>
        [Comment("Количество населения имеющих доступ к централизованному водоснабжению (человек)")]
        public int? CentralizedWaterNumber { get; set; } = 0;
        #endregion
        #region Village
        public int? RuralPopulation { get; set; } = 0;

        [Comment("Кол-во абонентов, охваченных централизованным водоснабжением (единиц)")]
        public int? CentralWaterSupplySubscribers { get; set; }

        [Comment("Всего установлено индивидуальных приборов учета воды по состоянию на конец отчетного года (единиц)")]
        public int? IndividualWaterMetersInstalled { get; set; }

        [Comment("в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)")]
        public int? RemoteDataTransmissionMeters { get; set; }
        #endregion
    }
}
