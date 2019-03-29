using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    public interface IPerformCountRepository : IRepository<PerformanceCount>
    {
        IEnumerable<PerformanceCount> GetPerformanceCounts();
        IEnumerable<PerformanceCount> GetPerformanceCountsByStudentAndParamID(int studID, int paramid);
        bool CheckForExtingPerformanceCounts(int studID, int paramid, DateTime performanceDate);
        PerformanceCount GetPerformanceCount(int id);
        IEnumerable<PerformanceCount> GetPerformanceCountsByClassAndParameter(int classID, int paramID, DateTime paramDate);
    }
}
