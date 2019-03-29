using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    class ClassDetailsRepository : Repository<ClassDetails>, IClassDetailsRepository
    {

        public ClassDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<ClassDetails> GetClassDeatils()
        {
            return AppContext.ClassDetails.ToList();
        }
    }
}
