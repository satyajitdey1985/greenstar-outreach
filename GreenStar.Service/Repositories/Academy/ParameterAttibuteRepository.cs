using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    public class ParameterAttibuteRepository : Repository<ParameterAttribute>, IParameterAttributeRepository
    {
        public ParameterAttibuteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<ParameterAttribute> GetParameterAttributes()
        {
            return AppContext.ParameterAttribute.Where(p=>p.IsDeleted == false).ToList();
        }

        public ParameterAttribute GetParameterAttribute(int ID)
        {
            return AppContext.ParameterAttribute.FirstOrDefault(p => p.IsDeleted == false && p.ParamID == ID);
        }
    }
}
