using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class ResponseCode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public required string NameCode { get; set; }
        public required string DescriptionCode { get; set; }    
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
