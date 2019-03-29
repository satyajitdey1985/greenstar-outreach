using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    public class GroupDetailsRepository: Repository <GroupDetails>, IGroupDetailsRepository
    {
        public GroupDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<GroupDetails> GetGroupsDetails()
        {
            return AppContext.GroupDetails.ToList();
        }
    }
}
