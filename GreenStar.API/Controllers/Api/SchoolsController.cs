using AutoMapper;
using GreenStar.API.Dtos.School;
using GreenStar.Entity.Academy;
using GreenStar.Service.UnitOfWork;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]    
    public class SchoolsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Schools
        public IHttpActionResult Get()
        {
            try
            {
                var schools = _unitOfWork.Schools.GetSchools();
                Mapper.CreateMap<School, SchoolListDTO>();
                var schoolDtos = schools.Select(Mapper.Map<School, SchoolListDTO>);
                return Ok(schoolDtos);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
          
        }

        // GET: api/Schools/5
        [ResponseType(typeof(School))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var schoolInDb = _unitOfWork.Schools.SingleOrDefault(c => c.schlID == id);
                if (schoolInDb == null)
                    return NotFound();
                Mapper.CreateMap<School, SchoolListDTO>();
                return Ok(Mapper.Map<School, SchoolListDTO>(schoolInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/GetSchoolByCity/{cityID}")]
        public IHttpActionResult GetSchoolByCity(int cityID)
        {
            try
            {
                var schools = _unitOfWork.Schools.GetSchools().Where(s=>s.cityId == cityID);
                Mapper.CreateMap<School, SchoolListDTO>();
                var schoolDtos = schools.Select(Mapper.Map<School, SchoolListDTO>);
                return Ok(schoolDtos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST: api/Schools
        [ResponseType(typeof(School))]
        public IHttpActionResult Post(CreateOrUpdateSchoolInputDTO input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                Mapper.CreateMap<CreateOrUpdateSchoolInputDTO, School>().ForMember(m => m.schlID, opt => opt.Ignore());
                var school = Mapper.Map<CreateOrUpdateSchoolInputDTO, School>(input);
                _unitOfWork.Schools.Add(school);
                _unitOfWork.Complete();
                return Created(new Uri(Request.RequestUri + "/" + school.schlID), school);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/Schools/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, CreateOrUpdateSchoolInputDTO school)
        {
            try
            {
                var schoolInDb = _unitOfWork.Schools.SingleOrDefault(c => c.schlID == id);

                if (schoolInDb == null)
                    return NotFound();

                schoolInDb.schoolName = school.schoolName;
                schoolInDb.cityId = school.CityID;
                schoolInDb.schlAddress1 = school.schlAddress1;
                schoolInDb.schlAddress2 = school.schlAddress2;
                schoolInDb.schlAddress3 = school.schlAddress3;
                schoolInDb.schlLatitude = school.schlLatitude;
                schoolInDb.schlLongitude = school.schlLongitude;
                schoolInDb.schlMailID = school.schlMailID;
                schoolInDb.schlMobileNo = school.schlMobileNo;
                schoolInDb.schlPOCName = school.schlPOCName;
                schoolInDb.schlTeleNo = school.schlTeleNo;
                schoolInDb.schlZip = school.schlZip;
                schoolInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.Schools.Update(schoolInDb);
                Mapper.CreateMap<School, SchoolListDTO>();
                return Ok(Mapper.Map<School, SchoolListDTO>(schoolInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Schools/5
        [ResponseType(typeof(School))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var schoolInDb = _unitOfWork.Schools.SingleOrDefault(c => c.schlID == id);
                if (schoolInDb == null)
                    return NotFound();
                schoolInDb.IsDeleted = true;
                _unitOfWork.Schools.Update(schoolInDb);
                Mapper.CreateMap<School, SchoolListDTO>();
                return Ok(Mapper.Map<School, SchoolListDTO>(schoolInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

        }
    }
}