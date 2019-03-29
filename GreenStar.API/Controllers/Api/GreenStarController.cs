using GreenStar.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GreenStar.Entity.Academy;
using GreenStar.API.Dtos;
using System.Web.Http.Description;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GreenStarController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public GreenStarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ResponseType(typeof(StarDto))]
        [Route("api/GetStarByClass/classID/{classID}/attributeID/{attributeID}")]
        public IHttpActionResult GetStarByClass(int classID, int attributeID)
            {
            int currentMonth = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            DateTime firstDate = Convert.ToDateTime(currentYear.ToString() + "/" + currentMonth.ToString() + "/01");
            DateTime lastDate = Convert.ToDateTime(currentYear.ToString() + "/" + currentMonth.ToString() + "/" + daysInMonth.ToString());
            List<StarDto> lstStarData = new List<StarDto>();
            for (int iday = 1; iday <= daysInMonth; iday++)
            {
                StarDto objStarDto = new StarDto();
                objStarDto.day = iday;
                var performanceCounts = _unitOfWork.PerformancesCount.GetPerformanceCountsByClassAndParameter(classID, attributeID, Convert.ToDateTime(currentYear.ToString() + "/" + currentMonth.ToString() + "/" + iday.ToString()));
                int totalCount = performanceCounts.Count();
                if (totalCount > 0)
                {
                    int positiveCount = performanceCounts.Where(p => p.Point == true).Count();
                    decimal percent = (positiveCount * 100) / totalCount;
                    ParameterAttribute attribute = _unitOfWork.ParameterAttributes.GetParameterAttribute(attributeID);                   

                    if (percent >= attribute.GreenVal)
                    {
                        objStarDto.colorHexCode = "#7CFC00";
                    }
                    else if (percent >=attribute.YellowVal)
                    {
                        objStarDto.colorHexCode = "#DAA520";
                    }
                    else
                    {
                        objStarDto.colorHexCode = "#8B0000";
                    }
                }
                else
                {
                    objStarDto.colorHexCode = "#87ceeb";
                }
                lstStarData.Add(objStarDto);
            }
            if (daysInMonth != 31)
            {
                for (int iDay = 1; iDay <= (31 - daysInMonth); iDay++)
                {
                    StarDto objStarDto = new StarDto();
                    objStarDto.day = daysInMonth + iDay;
                    objStarDto.colorHexCode = "#87ceeb";
                    lstStarData.Add(objStarDto);
                }
            }
            return Ok(lstStarData);
        }
    }
}
