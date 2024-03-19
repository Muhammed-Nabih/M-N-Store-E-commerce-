using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_N_Store.Domain.Entities
{
    public class AppUser :IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
