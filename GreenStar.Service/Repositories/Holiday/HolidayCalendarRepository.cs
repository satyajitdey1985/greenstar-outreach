using GreenStar.Entity.Holiday;
using System.Collections.Generic;
using System.Linq;

namespace GreenStar.Service.Repositories.Holiday
{
    public class HolidayCalendarRepository: Repository<HolidayCalendar>, IHolidayCalendarRepository
    {
        public HolidayCalendarRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<HolidayCalendar> GetHolidayCalendar()
        {
            return AppContext.HolidayCalendar.ToList();
        }
    }
}