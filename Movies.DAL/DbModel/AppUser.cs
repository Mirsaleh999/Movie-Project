using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{
    public class AppUser: IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Img { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
    }
}
