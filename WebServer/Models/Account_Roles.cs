namespace WebServer.Models
{
    public class Account_Roles : Base
    {
        public int RoleId { get; set; }
        public virtual Ref_Role Role { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
