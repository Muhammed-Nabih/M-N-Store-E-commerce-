using M_N_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_N_Store.Domain.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(AppUser appUser);
    }
}
