﻿using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    //Протяженность сетей
    public class CityNetworkLength
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        /// <summary>
        /// Протяженность водопроводных сетей, км (по состоянию на конец отчетного года)		
        /// общая, км
        /// </summary>
        [Comment("Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - общая, км")]
        public int? VodoProvodLengthTotal { get; set; }
        /// <summary>
        /// Протяженность водопроводных сетей, км (по состоянию на конец отчетного года)		
        /// в том числе изношенных, км
        /// </summary>
        [Comment("Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - в том числе изношенных, км")]
        public int? VodoProvodLengthIznos { get; set; }
        /// <summary>
        /// Протяженность водопроводных сетей, км (по состоянию на конец отчетного года)		
        /// Износ, % гр.59/гр.58
        /// </summary>
        [Comment("Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - Износ, % гр.59/гр.58")]
        public decimal? VodoProvodIznosPercent { get; set; }

        /// <summary>
        /// Протяженность канализационных сетей, км (по состоянию на конец отчетного года)		
        /// общая, км
        /// </summary>
        [Comment("Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - общая, км")]
        public int? KanalizLengthTotal { get; set; }
        /// <summary>
        /// Протяженность канализационных сетей, км (по состоянию на конец отчетного года)		
        /// в том числе изношенных, км
        /// </summary>
        [Comment("Протяженность канализационных сетей, км (по состоянию на конец отчетного года) - в том числе изношенных, км")]
        public int? KanalizLengthIznos { get; set; }
        /// <summary>
        /// Протяженность канализационных сетей, км (по состоянию на конец отчетного года)		
        /// Износ, % гр.62/гр.61
        /// </summary>
        [Comment("Протяженность канализационных сетей, км (по состоянию на конец отчетного года) - Износ, % гр.62/гр.61")]
        public decimal? KanalizIznosPercent { get; set; }

        /// <summary>
        /// Общая протяженность построенных (новых) сетей в отчетном году, км	
        /// водоснабжения, км
        /// </summary>
        [Comment("Общая протяженность построенных (новых) сетей в отчетном году, км - водоснабжения, км")]
        public int? ObshNewSetiVodo { get; set; }
        /// <summary>
        /// Общая протяженность построенных (новых) сетей в отчетном году, км	
        /// водоотведения, км
        /// </summary>
        [Comment("Общая протяженность построенных (новых) сетей в отчетном году, км - водоотведения, км")]
        public int? ObshNewSetiKanaliz { get; set; }

        /// <summary>
        /// Общая протяженность реконструированных (замененных) сетей в отчетном году, км	
        /// водоснабжения, км
        /// </summary>
        [Comment("Общая протяженность реконструированных (замененных) сетей в отчетном году, км - водоснабжения, км")]
        public int? ObshZamenSetiVodo { get; set; }
        /// <summary>
        /// Общая протяженность реконструированных (замененных) сетей в отчетном году, км	
        /// водоотведения, км
        /// </summary>
        [Comment("Общая протяженность реконструированных (замененных) сетей в отчетном году, км - водоотведения, км")]
        public int? ObshZamenSetiKanaliz { get; set; }

        /// <summary>
        /// Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км
        /// водоснабжения, км
        /// </summary>
        [Comment("Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км - водоснабжения, км")]
        public int? ObshRemontSetiVodo { get; set; }
        /// <summary>
        /// Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км
        /// водоотведения, км
        /// </summary>
        [Comment("Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км - водоотведения, км")]
        public int? ObshRemontSetiKanaliz { get; set; }
    }
}
