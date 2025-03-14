using Microsoft.AspNetCore.Identity;

namespace HotelApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}
