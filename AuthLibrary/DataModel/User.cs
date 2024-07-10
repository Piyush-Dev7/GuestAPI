using System.ComponentModel.DataAnnotations;

namespace DataAccess.Core.DataModel
{
    public class ApplicationUser
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
