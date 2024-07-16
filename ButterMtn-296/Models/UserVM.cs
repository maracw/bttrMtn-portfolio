using ButterMtn_296.Models;
using Microsoft.AspNetCore.Identity;

namespace ButterMtn_296.Models
{
    public class UserVM
    {
        public IEnumerable<AppUser> Users { get; set; } = null!;
        public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}
