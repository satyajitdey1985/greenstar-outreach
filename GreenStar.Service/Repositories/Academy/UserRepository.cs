using GreenStar.Entity.Academy;
using GreenStar.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    public class UserRepository : Repository<UserDetails>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public UserDetails LoginCheck(string email, string password)
        {
            var user = AppContext.User.Where(t => t.EmailID == email && t.Password == password).FirstOrDefault();

            return user;
        }
    }
}
