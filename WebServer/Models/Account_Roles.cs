namespace WebServer.Models
{
    public class Account_Roles : Base
    {
        public required int RoleId { get; set; }
        public virtual Ref_Role Role { get; set; }
        public required Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
