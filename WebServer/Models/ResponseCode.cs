using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class ResponseCode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string NameCode { get; set; } = string.Empty;
        public string DescriptionCode { get; set; } = string.Empty ;
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
