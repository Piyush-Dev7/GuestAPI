using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Core.DataModel
{
    public class GuestContact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid GuestId { get; set; }
        [Required]
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [ForeignKey("GuestId")]
        public virtual Guest Guest { get; set; }

    }
}
