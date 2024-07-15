using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    //Уровень тарифов
    public class CityTarif
    {
        public Guid Id { get; set; }
        public Guid IdForm { get; set; }
        /// <summary>
        /// водоснабжение			
        /// усредненный, тенге/м3
        /// </summary>
        [Comment("водоснабжение усредненный, тенге/м3")]
        public int? VodoSnabUsrednen { get; set; }
        /// <summary>
        /// водоснабжение			
        /// физическим лицам/населению, тенге/м3
        /// </summary>
        [Comment("водоснабжение физическим лицам/населению, тенге/м3")]
        public int? VodoSnabFizLic { get; set; }
        /// <summary>
        /// водоснабжение			
        /// юридическим лицам, тенге/м3
        /// </summary>
        [Comment("водоснабжение юридическим лицам, тенге/м3")]
        public int? VodoSnabYriLic { get; set; }
        /// <summary>
        /// водоснабжение			
        /// бюджетным организациям, тенге/м3
        /// </summary>
        [Comment("водоснабжение бюджетным организациям, тенге/м3")]
        public int? VodoSnabBydzhOrg { get; set; }

        /// <summary>
        /// водоотведение		
        /// усредненный, тенге/м3
        /// </summary>
        [Comment("водоотведение - усредненный, тенге/м3")]
        public int? VodoOtvedUsred{ get; set; }
        /// <summary>
        /// водоотведение		
        /// физическим лицам/населению, тенге/м3
        /// </summary>
        [Comment("водоотведение - физическим лицам/населению, тенге/м3")]
        public int? VodoOtvedFizLic { get; set; }
        /// <summary>
        /// водоотведение		
        /// юридическим лицам, тенге/м3
        /// </summary>
        [Comment("водоотведение - юридическим лицам, тенге/м3")]
        public int? VodoOtvedYriLic { get; set; }
        /// <summary>
        /// водоотведение		
        /// бюджетным организациям, тенге/м3
        /// </summary>
        [Comment("водоотведение - бюджетным организациям, тенге/м3")]
        public int? VodoOtvedBydzhOrg { get; set; }
    }
}
