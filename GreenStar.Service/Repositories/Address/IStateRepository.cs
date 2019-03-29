using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Address
{
    public interface IStateRepository : IRepository<State>
    {
        IEnumerable<State> GetStates();
    }
}
