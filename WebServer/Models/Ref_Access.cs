using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class Ref_Access : Ref_Base
    {
        /// <summary>
        /// Наименование доступа 
        /// </summary>
        [Comment("Наименование доступа")]
        public new string NameRu { get; set; }
        /// <summary>
        /// Код доступа
        /// </summary>
        [Comment("Код доступа")]
        public string CodeAccessName { get; set; }
        /// <summary>
        /// Тип доступа 
        /// </summary>
        [Comment("Тип доступа")]
        public string TypeAccessName { get; set; }
        /// <summary>
        /// Действие
        /// </summary>
        [Comment("Действие")]
        public string ActionName { get; set; }
    }
}
