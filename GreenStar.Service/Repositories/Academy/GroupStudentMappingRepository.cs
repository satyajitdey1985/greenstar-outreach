using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    class GroupStudentMappingRepository: Repository<GroupStudentMapping>, IGroupStudentMappingRepository 
    {
        public GroupStudentMappingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<GroupStudentMapping> GetGroupStudentMappings()
        {
            return AppContext.GroupStudentMapping.ToList();
        }

        public IEnumerable<GroupStudentMapping> GetGroupStudentMappingsWithGroupID(int GroupID)
        {
            return AppContext.GroupStudentMapping.Where(s => s.groupDetails.GroupID == GroupID).ToList();
        }
    }
}
