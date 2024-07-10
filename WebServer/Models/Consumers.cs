using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebServer.Models;

namespace Business.Entities
{
    public class Consumers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public int? Ref_KatoId { get; set; }
        public virtual Ref_Kato? Ref_Kato { get; set; }
    }
}
