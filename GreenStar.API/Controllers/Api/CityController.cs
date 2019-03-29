using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.Entity.Address;
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
    public class CityController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ClassDetails
        public IHttpActionResult Get()
        {
            var cities = _unitOfWork.Cities.GetCities();

            var cityDtos = cities.Select(Mapper.Map<City, CityDto>);

            Mapper.CreateMap<City, CityDto>();

            return Ok(cityDtos);
        }

        // GET: api/ClassDetails/5
        [ResponseType(typeof(City))]
        public IHttpActionResult Get(int id)
        {
            var cityInDb = _unitOfWork.Cities.SingleOrDefault(c => c.CityID == id);

            Mapper.CreateMap<City, CityDto>();

            if (cityInDb == null)
                return NotFound();

            return Ok(Mapper.Map<City, CityDto>(cityInDb));
        }

        // GET: api/GetCityByState/5
        [Route("api/GetCityByState/{id}")]
        public IHttpActionResult GetCityByState(int id)
        {
            var cities = _unitOfWork.Cities.GetCities().Where(c=> c.statID == id).ToList();

            var cityDtos = cities.Select(Mapper.Map<City, CityDto>);

            Mapper.CreateMap<City, CityDto>();

            return Ok(cityDtos);
        }
    }
}
