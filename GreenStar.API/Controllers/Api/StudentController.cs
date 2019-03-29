using AutoMapper;
using GreenStar.API.Dtos.StudentDto;
using GreenStar.Entity.Academy;
using GreenStar.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Student
        public IHttpActionResult Get()
        {
            var student = _unitOfWork.Students.GetStudents();
            Mapper.CreateMap<Student, StudentListDTO>();
            var studentDtos = student.Select(Mapper.Map<Student, StudentListDTO>);
            return Ok(studentDtos);
        }

        [Route("api/GetStudentsForGrouping/{classId}")]
        public IHttpActionResult GetStudentsForGrouping(int classId)
        {
            var student = _unitOfWork.Students.GetStudents().Where(s => s.ClassID == classId && s.isAssignToGroup == false).OrderBy(s => s.studName).OrderBy(s=>s.RollNo);
            Mapper.CreateMap<Student, StudentListDTO>();
            var studentDtos = student.Select(Mapper.Map<Student, StudentListDTO>);
            return Ok(studentDtos);
        }

        // GET: api/Student/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult Get(int id)
        {
            var studentInDb = _unitOfWork.Students.SingleOrDefault(c => c.studID == id);
            if (studentInDb == null)
                return NotFound();
            Mapper.CreateMap<Student, StudentListDTO>();
            return Ok(Mapper.Map<Student, StudentListDTO>(studentInDb));
        }

        [Route("api/GetStudentByClassID/{classID}")]
        public IHttpActionResult GetStudentByClassID(int classID)
        {
            var student = _unitOfWork.Students.GetStudents().Where(s=>s.ClassID == classID).OrderBy(s=>s.RollNo).ToList();
            //var student = _unitOfWork.Students.GetStudentByClassID(classID).ToList();
            if (student == null)
                return NotFound();

            Mapper.CreateMap<Student, StudentListDTO>();
            var studentDtos = student.Select(Mapper.Map<Student, StudentListDTO>);
           
            return Ok(studentDtos);
        }

        // POST: api/ClassDetails
        [ResponseType(typeof(ClassDetails))]
        public IHttpActionResult Post(CreateOrUpdateStudentInputDTO studentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                Mapper.CreateMap<CreateOrUpdateStudentInputDTO, Student>().ForMember(m => m.studID, opt => opt.Ignore());
                var student = Mapper.Map<CreateOrUpdateStudentInputDTO, Student>(studentDto);
                _unitOfWork.Students.Add(student);
                _unitOfWork.Complete();
                return Created(new Uri(Request.RequestUri + "/" + student.studID), student);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, CreateOrUpdateStudentInputDTO student)
        {
            try
            {
                var studentInDb = _unitOfWork.Students.SingleOrDefault(c => c.studID == id);

                if (studentInDb == null)
                    return NotFound();

                studentInDb.studName = student.studName;
                studentInDb.ClassID = student.ClassID;
                studentInDb.RollNo = student.RollNo;
                studentInDb.studAddress1 = student.studAddress1;
                studentInDb.studAddress2 = student.studAddress2;
                studentInDb.studAddress3 = student.studAddress3;
                studentInDb.studlZip = student.studlZip;
                studentInDb.schlMailID = student.schlMailID;
                studentInDb.studMobileNo = student.studMobileNo;
                studentInDb.schlMailID = student.schlMailID;
                studentInDb.ModifyBy = student.ModifyBy;               
                studentInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.Students.Update(studentInDb);
                Mapper.CreateMap<Student, StudentListDTO>();
                return Ok(Mapper.Map<Student, StudentListDTO>(studentInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Student))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var studentInDb = _unitOfWork.Students.SingleOrDefault(c => c.studID == id);
                if (studentInDb == null)
                    return NotFound();
                studentInDb.IsDeleted = true;
                studentInDb.IsActive = false;
                _unitOfWork.Students.Update(studentInDb);
                Mapper.CreateMap<Student, StudentListDTO>();
                return Ok(Mapper.Map<Student, StudentListDTO>(studentInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}
