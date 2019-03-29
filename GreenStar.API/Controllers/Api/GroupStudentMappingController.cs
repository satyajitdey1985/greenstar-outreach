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
    //[RoutePrefix("GroupStudentMapping")]
    public class GroupStudentMappingController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupStudentMappingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/GroupStudentMapping
        public IHttpActionResult Get()
        {
            var groupStudentMapping = _unitOfWork.GroupStudentMappings.GetGroupStudentMappings();
            Mapper.CreateMap<GroupStudentMapping, GroupStudentMappingListDto>();
            var groupStudentMappingDtos = groupStudentMapping.Select(Mapper.Map<GroupStudentMapping, GroupStudentMappingListDto>);

            return Ok(groupStudentMapping);
        }

        // GET: api/GroupStudentMapping/5
        [ResponseType(typeof(GroupStudentMapping))]
        public IHttpActionResult Get(int GroupID)
        {
            var groupStudentMappingInDb = _unitOfWork.GroupStudentMappings.GetGroupStudentMappingsWithGroupID(GroupID);

            var groupStudentMappingDtos = groupStudentMappingInDb.Select(Mapper.Map<GroupStudentMapping, GroupStudentMappingListDto>);
            Mapper.CreateMap<GroupStudentMapping, GroupStudentMappingListDto>();
            return Ok(groupStudentMappingDtos);
        }       


        [Route("api/GetGroupStudentsByClass/{classId}")]
        public IHttpActionResult GetGroupStudentsByClass(int classID)
        {
            List<GroupDetails> groupDetails = _unitOfWork.GroupsDetails.GetGroupsDetails().Where(g => g.classDetails.ClassID == classID).ToList();
            List<GroupDetailsListWithMembersDTO> lstGroupDetailsListWithMembers = new List<GroupDetailsListWithMembersDTO>();
            foreach (GroupDetails gd in groupDetails)
            {
                GroupDetailsListWithMembersDTO objGroupDetailsListWithMembers = new GroupDetailsListWithMembersDTO();
                objGroupDetailsListWithMembers.group = gd;                
                List<GroupStudentMapping> lstGroupStudentMapping = _unitOfWork.GroupStudentMappings.GetGroupStudentMappingsWithGroupID(gd.GroupID).ToList();
                if (lstGroupStudentMapping.Count > 0)
                {
                    objGroupDetailsListWithMembers.members = new List<Student>();
                    foreach (GroupStudentMapping obj in lstGroupStudentMapping)
                    {
                        Student objStudent = _unitOfWork.Students.Get(obj.studID);
                        objGroupDetailsListWithMembers.members.Add (objStudent);
                    }
                }
                lstGroupDetailsListWithMembers.Add(objGroupDetailsListWithMembers);
            }
            return Ok(lstGroupDetailsListWithMembers);
        }

        // POST: api/GroupStudentMapping
        [ResponseType(typeof(GroupStudentMapping))]
        public IHttpActionResult Post(GroupDetailsListWithMembersDTO groupStudentMappingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //Mapper.CreateMap<GroupDetailsListWithMembersDTO, GroupStudentMapping>();
            //GroupStudentMapping groupStudentMapping = new GroupStudentMapping();

            //groupStudentMapping = (GroupStudentMapping)Mapper.Map(groupStudentMappingDto, groupStudentMapping, typeof(GroupDetailsListWithMembersDTO), typeof(GroupStudentMapping));

            foreach (Student stud in groupStudentMappingDto.members)
            {
                GroupStudentMapping groupStudentMapping = _unitOfWork.GroupStudentMappings.GetGroupStudentMappings().SingleOrDefault(gs => gs.GroupID == groupStudentMappingDto.group.GroupID && gs.studID == stud.studID);

                if (groupStudentMapping != null)
                {
                    _unitOfWork.GroupStudentMappings.Remove(groupStudentMapping);
                    _unitOfWork.Complete();
                    Student studForModification = _unitOfWork.Students.GetStudents().SingleOrDefault(s => s.studID == stud.studID);
                    if (studForModification != null)
                    {
                        studForModification.isAssignToGroup = true;
                        studForModification.ModifyDate = DateTime.UtcNow;
                        _unitOfWork.Students.Update(studForModification);
                        _unitOfWork.Complete();
                    }
                    _unitOfWork.GroupStudentMappings.Add(groupStudentMapping);
                    _unitOfWork.Complete();
                    //studForModification = _unitOfWork.Students.GetStudents().SingleOrDefault(s => s.studID == stud.studID);
                    if (studForModification != null)
                    {
                        studForModification.isAssignToGroup = true;
                        studForModification.ModifyDate = DateTime.UtcNow;
                        _unitOfWork.Students.Update(studForModification);
                        _unitOfWork.Complete();
                    }
                }
            }
            //return Created(new Uri(Request.RequestUri + "/" + groupStudentMapping.GroupID), groupStudentMappingDto);
            return Ok("Data inserted sucessfully");
        }

        // PUT: api/GroupStudentMapping/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, GroupStudentMapping groupStudentMapping)
        {
            try
            {
                var groupStudentMapInDb = _unitOfWork.GroupStudentMappings.SingleOrDefault(c => c.GroupID == id);

                if (groupStudentMapInDb == null)
                    return NotFound();

                groupStudentMapInDb.GroupID = groupStudentMapping.GroupID;
                groupStudentMapInDb.studID = groupStudentMapping.studID;
                groupStudentMapInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.GroupStudentMappings.Update(groupStudentMapInDb);
                Mapper.CreateMap<GroupStudentMapping, GroupStudentMappingListDto>();
                return Ok(Mapper.Map<GroupStudentMapping, GroupStudentMappingListDto>(groupStudentMapInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/GroupStudentMapping/5
        [ResponseType(typeof(GroupStudentMapping))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var groupStudentMapInDb = _unitOfWork.GroupStudentMappings.SingleOrDefault(c => c.GroupID == id);
                if (groupStudentMapInDb == null)
                    return NotFound();
                groupStudentMapInDb.IsDeleted = true;
                _unitOfWork.GroupStudentMappings.Update(groupStudentMapInDb);
                Mapper.CreateMap<GroupStudentMapping, GroupStudentMappingListDto>();
                return Ok(Mapper.Map<GroupStudentMapping, GroupStudentMappingListDto>(groupStudentMapInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}
