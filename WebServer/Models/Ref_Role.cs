using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class Ref_Role : Ref_Base
    {
        /// <summary>
        /// Код роли
        /// </summary>
        [Comment("Код роли")]
        public string Code { get; set; }
        /// <summary>
        /// Тип роли
        /// </summary>
        [Comment("Тип роли")]
        public string TypeName { get; set; }
    }
}
