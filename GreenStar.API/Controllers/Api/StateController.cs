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

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StateController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Student
        public IHttpActionResult Get()
        {
            var state = _unitOfWork.States.GetStates();
            Mapper.CreateMap<State, StateListDto>();
            var stateDtos = state.Select(Mapper.Map<State, StateListDto>);
            return Ok(stateDtos);
        }
    }
}
