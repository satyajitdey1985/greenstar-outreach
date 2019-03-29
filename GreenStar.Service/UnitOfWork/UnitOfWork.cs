using GreenStar.Service.Repositories.Academy;
using GreenStar.Service.Repositories.Address;
using GreenStar.Service.Repositories.Holiday;


namespace GreenStar.Service.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IHolidayCalendarRepository HolidayCalendars { get; private set; }

        public ISchoolRepository Schools { get; private set; }

        public IClassDetailsRepository ClassesDetails { get; private set; }

        public IStudentRepository Students { get; private set; }

        public IParameterAttributeRepository ParameterAttributes { get; set; }

        public IGroupDetailsRepository GroupsDetails { get; set; }

        public IGroupStudentMappingRepository GroupStudentMappings { get; set; }
        public IStateRepository States { get; set; }

        public ICityRepository Cities { get; set; }

        public IPerformCountRepository PerformancesCount { get; set; }
        public IUserRepository Users { get ; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            HolidayCalendars = new HolidayCalendarRepository(_context);
            Schools = new SchoolRepository(_context);
            ClassesDetails = new ClassDetailsRepository(_context);
            Students = new StudentRepository(_context);
            ParameterAttributes = new ParameterAttibuteRepository(_context);
            GroupsDetails = new GroupDetailsRepository(_context);
            GroupStudentMappings = new GroupStudentMappingRepository(_context);
            States = new StateRepository(_context);
            Cities = new CityRepository(_context);
            PerformancesCount = new PerformCountRepository(_context);
            Users = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}