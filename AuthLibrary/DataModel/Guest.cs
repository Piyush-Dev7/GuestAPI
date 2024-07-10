using GuestSharedModel.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Core.DataModel
{
    public class Guest
    {
        [Key]
        public Guid Id { get; set; }
        public Title Title { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateOnly BirthDate { get; set; }
        [MaxLength(64)]
        public string Email { get; set; }
        public virtual ICollection<GuestContact> GuestContacts { get; set; }
    }
}
