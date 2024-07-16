using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ButterMtn_296.Models
{
	
	
        public class AppUser : IdentityUser
        {
            public string? Name { get; set; }
            public string? About { get; set; }
            public string? UserIconURL { get; set; }
            public DateTime DateAdded { get; set; }

            [NotMapped]
            public IList<string> RoleNames { get; set; }
        }
    
}

