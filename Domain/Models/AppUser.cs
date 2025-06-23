using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<WishList> WishLists { get; set; }
    }
}
