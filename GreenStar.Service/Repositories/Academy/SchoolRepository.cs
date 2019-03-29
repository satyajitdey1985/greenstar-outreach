using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<School> GetSchools()
        {
            return AppContext.school.Where(t=>!t.IsDeleted).ToList();
        }
    }
}
