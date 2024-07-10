using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class Ref_Role_Access : Ref_Base
    {
        /// <summary>
        /// Айди roles
        /// </summary>
        [Comment("Айди roles")]
        public int RoleId { get; set; }
        public virtual Ref_Role Role { get; set; } = new Ref_Role();
        public int AccessId { get; set; }
        public virtual Ref_Access Access { get; set; } = new Ref_Access();
    }
}
