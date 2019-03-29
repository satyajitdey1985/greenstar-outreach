using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.Entity.Academy;
using GreenStar.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Microsoft.Office.Interop.Excel;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PerformanceCountController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;


        public PerformanceCountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult Get()
        {
            var performanceCounts = _unitOfWork.PerformancesCount.GetPerformanceCounts();
            Mapper.CreateMap<PerformanceCount, PerformanceCountListDto>();
            var performanceCountsDtos = performanceCounts.Select(Mapper.Map<PerformanceCount, PerformanceCountListDto>);
            return Ok(performanceCountsDtos);
        }

        // GET: api/PerformanceCount/5
        [ResponseType(typeof(PerformanceCount))]
        public IHttpActionResult Get(int studentID, int paramID)
        {
            var performInDb = _unitOfWork.PerformancesCount.SingleOrDefault(c => c.studID == studentID && c.ParamID == paramID);

            if (performInDb == null)
                return NotFound();

            Mapper.CreateMap<PerformanceCount, PerformanceCountListDto>();

            return Ok(Mapper.Map<PerformanceCount, PerformanceCountListDto>(performInDb));
        }

        [HttpPost]
        [Route("api/UploadFile")]
        public HttpResponseMessage UploadFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }
            return response;
        }

        [HttpGet]
        [Route("api/DownloadTemplate/{classID}")]
        public IHttpActionResult DownloadTemplate(int classID)
        {
            ClassDetails objClassDetails = _unitOfWork.ClassesDetails.SingleOrDefault(c => c.ClassID == classID);
            if (objClassDetails != null)
            {
                School objSchool = _unitOfWork.Schools.GetSchools().SingleOrDefault(s => s.schlID == objClassDetails.schlID);
                if (objSchool != null)
                {
                    List<PerformanceCount> performanceCountList = new List<PerformanceCount>();
                    List<Student> lstStudent = _unitOfWork.Students.GetStudents().Where(s => s.ClassID == classID).ToList();
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                    // Create empty workbook
                    excel.Workbooks.Add();

                    // Create Worksheet from active sheet
                    Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

                    // I created Application and Worksheet objects before try/catch,
                    // so that i can close them in finnaly block.
                    // It's IMPORTANT to release these COM objects!!
                    try
                    {
                        int row = 2; // start row (in row 1 are header cells)
                        workSheet.Cells[row, "A"] = objSchool.schlID;                        
                        workSheet.Cells[row, "B"] = "School Name";
                        workSheet.Cells[row, "C"] = objSchool.schoolName;
                        row = 3;
                        workSheet.Cells[row, "A"] = objClassDetails .ClassID;
                        workSheet.Cells[row, "B"] = "Class Name";
                        workSheet.Cells[row, "C"] = objClassDetails.classStandard.ToString() + "-" + objClassDetails.sectName; 
                        row = 4;
                        // ------------------------------------------------
                        // Creation of header cells
                        // ------------------------------------------------
                        workSheet.Cells[row, "A"] = "StudentID";
                        workSheet.Cells[row, "B"] = "Roll No";
                        workSheet.Cells[row, "C"] = "Name";
                        List<ParameterAttribute> lstParameterAttribute = _unitOfWork.ParameterAttributes.GetParameterAttributes().ToList();
                        string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                        int indexOfAlphabet = 2;
                        foreach (ParameterAttribute obj in lstParameterAttribute)
                        {
                            indexOfAlphabet++;
                            if (indexOfAlphabet < 26)
                            {
                                workSheet.Cells[row, alphabets[indexOfAlphabet]] = obj.ParamName;
                            }
                        }
                        //List<Car> cars = new List<Car>()
                        //{
                        //    new Car {Name = "Toyota", Color = "Red", MaximumSpeed = 195},
                        //    new Car {Name = "Honda", Color = "Blue", MaximumSpeed = 224},
                        //    new Car {Name = "Mazda", Color = "Green", MaximumSpeed = 205}
                        //};
                        // ------------------------------------------------
                        // Populate sheet with some real data from "cars" list
                        // ------------------------------------------------

                        indexOfAlphabet = 2;
                        foreach (Student student in lstStudent)
                        {
                            workSheet.Cells[row, "A"] = student.studID;
                            workSheet.Cells[row, "B"] = student.RollNo;
                            workSheet.Cells[row, "C"] = student.studName;
                            foreach (ParameterAttribute obj in lstParameterAttribute)
                            {
                                indexOfAlphabet++;
                                if (indexOfAlphabet < 26)
                                {
                                    workSheet.Cells[row, alphabets[indexOfAlphabet]] = true;
                                    //workSheet.Range[row.ToString() + alphabets[indexOfAlphabet]].DataValidation.AllowType = CellDataType.Decimal;
                                    //workSheet.Range[row.ToString() + alphabets[indexOfAlphabet]].
                                }
                            }

                            row++;
                        }

                        // Apply some predefined styles for data to look nicely :)
                        workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects1);//xlRangeAutoFormatClassic1
                        string fileName = "StudentPerformanceRecord_" + objSchool.schoolName + "_" + objClassDetails.classStandard.ToString() + "_" + objClassDetails.sectName + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".xlsx";
                        // Define filename
                        string filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + fileName);

                        // Save this data as a file
                        workSheet.SaveAs(fileName);

                        // Display SUCCESS message

                    }
                    catch (Exception exception)
                    {

                    }
                    finally
                    {
                        // Quit Excel application
                        excel.Quit();

                        // Release COM objects (very important!)
                        if (excel != null)
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                        if (workSheet != null)
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                        // Empty variables
                        excel = null;
                        workSheet = null;

                        // Force garbage collector cleaning
                        GC.Collect();
                    }
                }
            }
            return Ok("/UploadFile/" + "ExcelData.xlsx");
        }
        

        [HttpPost]
        [ResponseType(typeof(PerformanceCount))]
        public IHttpActionResult Post(List<CreateOrUpdatePerformanceCountDto> performanceCountDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Mapper.CreateMap<CreateOrUpdatePerformanceCountDto, PerformanceCount>().ForMember(m => m.PerformID, opt => opt.Ignore());
                List<PerformanceCount> performanceCountList = new List<PerformanceCount>();

                foreach (CreateOrUpdatePerformanceCountDto dto in performanceCountDto)
                {
                    var performanceCount = Mapper.Map<CreateOrUpdatePerformanceCountDto, PerformanceCount>(dto);

                    performanceCountList.Add(performanceCount);
                    if (performanceCount.CreatioinDate.Equals(DateTime.MinValue))
                    {
                        performanceCount.CreatioinDate = DateTime.UtcNow;
                    }
                    if (performanceCount.ModifyDate.Equals(DateTime.MinValue))
                    {
                        performanceCount.ModifyDate = DateTime.UtcNow;
                    }
                    if (performanceCount.ParamDate.Equals(DateTime.MinValue))
                    {
                        performanceCount.ParamDate = DateTime.UtcNow;
                    }

                    if (_unitOfWork.PerformancesCount.CheckForExtingPerformanceCounts(performanceCount.studID, performanceCount.ParamID, performanceCount.ParamDate))
                    {
                        var performanceCountObj = _unitOfWork.PerformancesCount.GetPerformanceCount(dto.PerformID);

                        _unitOfWork.PerformancesCount.Remove(performanceCountObj);
                        _unitOfWork.PerformancesCount.Add(performanceCount);
                    }
                    else
                    {
                        _unitOfWork.PerformancesCount.Add(performanceCount);
                    }

                    _unitOfWork.Complete();
                }
                //return Created(new Uri(Request.RequestUri + "/" + performanceCount.PerformID), performanceCount);
                return Ok(performanceCountList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/PerformanceCount/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(CreateOrUpdatePerformanceCountDto performDto)
        {
            try
            {
                var performanceInDb = _unitOfWork.PerformancesCount.SingleOrDefault(c => c.ParamID == performDto.ParamID && c.studID == performDto.studID && c.ParamDate == performDto.ParamDate);

                if (performanceInDb == null)
                    return NotFound();
                Mapper.CreateMap<PerformanceCount, CreateOrUpdatePerformanceCountDto>();
                performanceInDb.Point = performDto.Point;
                performanceInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.PerformancesCount.Update(performanceInDb);
                return Ok(Mapper.Map<PerformanceCount, PerformanceCountListDto>(performanceInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/PerformanceCount/5
        [ResponseType(typeof(PerformanceCount))]
        public IHttpActionResult Delete(CreateOrUpdatePerformanceCountDto performDto)
        {
            try
            {
                var parameterInDb = _unitOfWork.PerformancesCount.SingleOrDefault(c => c.ParamID == performDto.ParamID && c.studID == performDto.studID && c.ParamDate == performDto.ParamDate);
                if (parameterInDb == null)
                    return NotFound();
                Mapper.CreateMap<PerformanceCount, CreateOrUpdatePerformanceCountDto>();
                parameterInDb.IsDeleted = true;
                parameterInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.PerformancesCount.Update(parameterInDb);

                return Ok(Mapper.Map<PerformanceCount, PerformanceCountListDto>(parameterInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    public class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int MaximumSpeed { get; set; }
    }
}
