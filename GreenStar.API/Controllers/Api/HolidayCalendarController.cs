using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.Service.UnitOfWork;
using GreenStar.Entity.Holiday;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("holidays")]
    public class HolidayCalendarController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public HolidayCalendarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        //[Route("getallholidays")]
        public IHttpActionResult Get()
        {
            var holidayCalendars = _unitOfWork.HolidayCalendars.GetHolidayCalendar();           

            var holidayCalendarDtos = holidayCalendars.Select(Mapper.Map<HolidayCalendar , HolidayCalendarDto>);

            return Ok(holidayCalendarDtos);
        }

        
        //[Route("getholidaybyname/{name}")]
        public IHttpActionResult Get(string name)
        {
            var holidayCalendarInDb = _unitOfWork.HolidayCalendars.SingleOrDefault(c => c.HolidayName == name);

            if (holidayCalendarInDb == null)
                return NotFound();

            return Ok(Mapper.Map<HolidayCalendar, HolidayCalendarDto>(holidayCalendarInDb));
        }

        
        //[HttpPost]        
        public IHttpActionResult Post(HolidayCalendarDto holidayCalendarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var holidayCalendar = Mapper.Map<HolidayCalendarDto, HolidayCalendar>(holidayCalendarDto);

            _unitOfWork.HolidayCalendars.Add(holidayCalendar);
            _unitOfWork.Complete();

            holidayCalendarDto.HolidayID = holidayCalendar.HolidayID;

            return Created(new Uri(Request.RequestUri + "/" + holidayCalendar.HolidayID), holidayCalendarDto);
        }

        public void Put(int id, HolidayCalendarDto holidayCalendarDto)
        {
        }

        
        public void Delete(int id)
        {
        }
    }
}
