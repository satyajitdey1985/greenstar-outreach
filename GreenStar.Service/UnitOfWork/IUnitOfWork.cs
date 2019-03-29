
using GreenStar.Service.Repositories.Academy;
using GreenStar.Service.Repositories.Address;
using GreenStar.Service.Repositories.Holiday;

namespace GreenStar.Service.UnitOfWork
{
    public interface IUnitOfWork
    {
        IHolidayCalendarRepository HolidayCalendars { get; }
        ISchoolRepository Schools { get; }
        IClassDetailsRepository ClassesDetails { get; }
        IStudentRepository Students { get; }
        IParameterAttributeRepository ParameterAttributes { get; set; }
        IGroupDetailsRepository GroupsDetails { get; set; }
        IGroupStudentMappingRepository GroupStudentMappings { get; set; }
        ICityRepository Cities { get; set; }
        IUserRepository Users { get; set; }
        IStateRepository States { get; set; }
        IPerformCountRepository PerformancesCount { get; set; }
        int Complete();
    }
}
