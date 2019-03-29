using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    class PerformCountRepository : Repository<PerformanceCount>, IPerformCountRepository
    {
        public PerformCountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<PerformanceCount> GetPerformanceCountsByStudentAndParamID(int studID, int paramid)
        {
            return AppContext.PerformCount.Where(p => (p.studID == studID && p.ParamID == paramid && p.IsDeleted == false)).ToList();
        }

        public bool CheckForExtingPerformanceCounts(int studID, int paramid, DateTime performanceDate)
        {
            PerformanceCount objPerformanceCount = AppContext.PerformCount.Where(p => p.studID == studID && p.ParamID == paramid && p.IsDeleted == false && p.ParamDate == performanceDate).FirstOrDefault(); //&& (p.ParamDate.ToShortDateString() == performanceDate.ToShortDateString())
            if (objPerformanceCount == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public PerformanceCount GetPerformanceCount(int id)
        {
            return AppContext.PerformCount.Where(p => p.PerformID == id).FirstOrDefault();
        }

        public IEnumerable<PerformanceCount> GetPerformanceCounts()
        {
            return AppContext.PerformCount.Where(p => p.IsDeleted == false).ToList();
        }

        public IEnumerable<PerformanceCount> GetPerformanceCountsByClassAndParameter(int classID, int paramID, DateTime paramDate)
        {

            return AppContext.PerformCount.Where(p =>
            p.IsDeleted == false
            && p.student.ClassID == classID
            && p.ParamID == paramID
            && p.ParamDate.Day == paramDate.Day
            && p.ParamDate.Month == paramDate.Month
            && p.ParamDate.Year == paramDate.Year
            ).ToList();
        }
    }
}
