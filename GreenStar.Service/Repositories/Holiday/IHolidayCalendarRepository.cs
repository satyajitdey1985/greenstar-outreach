using GreenStar.Entity.Holiday;
using System.Collections.Generic;

namespace GreenStar.Service.Repositories.Holiday
{
    public interface IHolidayCalendarRepository : IRepository<HolidayCalendar>
    {
        IEnumerable<HolidayCalendar> GetHolidayCalendar();
    }
}
