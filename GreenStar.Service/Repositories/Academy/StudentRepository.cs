using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Service.Repositories.Academy
{
    class StudentRepository : Repository<Student>,  IStudentRepository 
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<Student> GetStudents()
        {
            return AppContext.Student.Where(t => !t.IsDeleted).ToList();
        }

        public IEnumerable<Student> GetStudentByClassID(int classID)
        {
            return AppContext.Student.Where(t => !t.IsDeleted && t.ClassID == classID).OrderBy(t=>t.RollNo).ToList();
        }
    }
}
