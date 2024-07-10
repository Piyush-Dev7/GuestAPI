using GuestSharedModel.DtoModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Core.DataModel
{
    public class UserGuestsMapping
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CreatedDate { get; set; }

        //Lazy Loading
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("GuestId")]
        public virtual Guest Guest { get; set; }
    }
}
