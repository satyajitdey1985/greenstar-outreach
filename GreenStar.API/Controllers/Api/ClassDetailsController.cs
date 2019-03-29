using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.Entity.Academy;
using GreenStar.Service;
using GreenStar.Service.UnitOfWork;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClassDetailsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ClassDetails
        public IHttpActionResult Get()
        {
            var classDetails = _unitOfWork.ClassesDetails.GetClassDeatils();

            var classDetailsDtos = classDetails.Select(Mapper.Map<ClassDetails, ClassDetailsDto>);

            return Ok(classDetailsDtos);
        }

        // GET: api/ClassDetails/5
        [ResponseType(typeof(ClassDetails))]
        public IHttpActionResult Get(int id)
        {
            var classInDb = _unitOfWork.ClassesDetails.SingleOrDefault(c => c.ClassID == id);

            if (classInDb == null)
                return NotFound();

            return Ok(Mapper.Map<ClassDetails, ClassDetailsDto>(classInDb));
        }

        // GET: api/ClassDetails/5
        [Route("api/GetClassBySchool/{schoolID}")]
        [ResponseType(typeof(ClassDetails))]
        public IHttpActionResult GetClassBySchool(int schoolID)
        {
            var classDetails = _unitOfWork.ClassesDetails.GetClassDeatils().Where(c=>c.schlID == schoolID).ToList();
            if (classDetails == null)
                return NotFound();
            Mapper.CreateMap<ClassDetails, ClassDetailsDto>();
            var classDetailsDtos = classDetails.Select(Mapper.Map<ClassDetails, ClassDetailsDto>);
            return Ok(classDetailsDtos);
        }

        // POST: api/ClassDetails
        [ResponseType(typeof(ClassDetails))]
        public IHttpActionResult Post(ClassDetailsDto classDetailsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var classDetails = Mapper.Map<ClassDetailsDto, ClassDetails>(classDetailsDto);

            _unitOfWork.ClassesDetails.Add(classDetails);
            _unitOfWork.Complete();

            classDetailsDto.schlID = classDetails.schlID;

            return Created(new Uri(Request.RequestUri + "/" + classDetails.ClassID), classDetailsDto);
        }

        // PUT: api/ClassDetails/5
        [ResponseType(typeof(void))]
        public void Put(int id, ClassDetails classDetails)
        {

        }

        // DELETE: api/ClassDetails/5
        [ResponseType(typeof(ClassDetails))]
        public void Delete(int id)
        {            
        }
    }
}