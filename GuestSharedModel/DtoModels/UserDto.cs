using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GuestSharedModel.DtoModels
{
    public class UserDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; } = true;//By default new user is active user
    }
}
