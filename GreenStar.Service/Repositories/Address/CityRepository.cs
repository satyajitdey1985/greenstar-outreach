using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Address
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<City> GetCities()
        {
            return AppContext.city.ToList();
        }
    }
}
