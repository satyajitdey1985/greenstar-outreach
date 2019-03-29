using GreenStar.Entity.Academy;
using GreenStar.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    public interface IUserRepository : IRepository<UserDetails>
    {
        UserDetails LoginCheck(string email,string password);
    }
}
