using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Address
{
    class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<State> GetStates()
        {
            return AppContext.state.ToList();
        }
    }
}
