using AutoMapper;
using GreenStar.API.Dtos;
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
    [RoutePrefix("ParameterAttribute")]
    public class ParameterAttributeController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParameterAttributeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ParameterAttribute
        public IHttpActionResult Get()
        {
            var parameterAttribute = _unitOfWork.ParameterAttributes.GetParameterAttributes();

            Mapper.CreateMap<ParameterAttribute, ParameterAttributeListDto>();

            var parameterAttributeDtos = parameterAttribute.Select(Mapper.Map<ParameterAttribute, ParameterAttributeListDto>);

            return Ok(parameterAttributeDtos);
        }

        // GET: api/ParameterAttribute/5
        [ResponseType(typeof(ParameterAttribute))]
        public IHttpActionResult Get(int id)
        {
            var parameterAttributeInDb = _unitOfWork.ParameterAttributes.SingleOrDefault(c => c.ParamID == id);

            if (parameterAttributeInDb == null)
                return NotFound();

            Mapper.CreateMap<ParameterAttribute, ParameterAttributeListDto>();

            return Ok(Mapper.Map<ParameterAttribute, ParameterAttributeListDto>(parameterAttributeInDb));
        }

        // POST: api/ParameterAttribute
        [ResponseType(typeof(ClassDetails))]
        public IHttpActionResult Post(CreateOrUpdateParameterAttributeDto parameterAttributeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Mapper.CreateMap<CreateOrUpdateParameterAttributeDto, ParameterAttribute>().ForMember(m => m.ParamID, opt => opt.Ignore());

                var parameterAttribute = Mapper.Map<CreateOrUpdateParameterAttributeDto, ParameterAttribute>(parameterAttributeDto);

                //parameterAttributeDto.ParamID = parameterAttribute.ParamID;

                _unitOfWork.ParameterAttributes.Add(parameterAttribute);
                _unitOfWork.Complete();

                return Created(new Uri(Request.RequestUri + "/" + parameterAttribute.ParamID), parameterAttribute);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/ParameterAttribute/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, CreateOrUpdateParameterAttributeDto parameterAttribute)
        {
            try
            {
                var parameterInDb = _unitOfWork.ParameterAttributes.SingleOrDefault(c => c.ParamID == id);

                if (parameterInDb == null)
                    return NotFound();
                Mapper.CreateMap<ParameterAttribute, CreateOrUpdateParameterAttributeDto>();
                parameterInDb.ParamName = parameterAttribute.ParamName;
                parameterInDb.RedVal = parameterAttribute.RedVal;
                parameterInDb.GreenVal = parameterAttribute.GreenVal;
                parameterInDb.YellowVal = parameterAttribute.YellowVal;
                parameterInDb.Comments = parameterAttribute.Comments;
                parameterInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.ParameterAttributes.Update(parameterInDb);
                return Ok(Mapper.Map<ParameterAttribute, ParameterAttributeListDto>(parameterInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/ParameterAttribute/5
        [ResponseType(typeof(ParameterAttribute))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var parameterInDb = _unitOfWork.ParameterAttributes.SingleOrDefault(c => c.ParamID == id);
                if (parameterInDb == null)                    
                return NotFound();

                Mapper.CreateMap<ParameterAttribute, CreateOrUpdateParameterAttributeDto>();

                parameterInDb.IsDeleted = true;
                _unitOfWork.ParameterAttributes.Update(parameterInDb);

                return Ok(Mapper.Map<ParameterAttribute, ParameterAttributeListDto>(parameterInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}
