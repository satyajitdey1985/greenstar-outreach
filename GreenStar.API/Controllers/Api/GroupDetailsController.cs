using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.API.Dtos.School;
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
    //[RoutePrefix("GroupDetails")]
    public class GroupDetailsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/GroupDetails
        public IHttpActionResult Get()
        {
            var groupDetails = _unitOfWork.GroupsDetails.GetGroupsDetails();
            Mapper.CreateMap<GroupDetails, GroupDetailsListDTO>();
            var GroupDetailsDtos = groupDetails.Select(Mapper.Map<GroupDetails, GroupDetailsListDTO>);
            return Ok(GroupDetailsDtos);
        }

        // GET: api/GroupDetails/5
        [ResponseType(typeof(GroupDetails))]
        public IHttpActionResult Get(int id)
        {
            var GroupDetailsInDb = _unitOfWork.GroupsDetails.SingleOrDefault(c => c.GroupID == id);

            if (GroupDetailsInDb == null)
                return NotFound();
            Mapper.CreateMap<GroupDetails, GroupDetailsListDTO>();
            return Ok(Mapper.Map<GroupDetails, GroupDetailsListDTO>(GroupDetailsInDb));
        }

        // GET: api/GroupDetails/5
        //[ResponseType(typeof(GroupDetails))]
        //[Route("api/GetGroupsByClass/{classId}")]        
        //public IHttpActionResult GetGroupsByClass(int classId)
        //{
        //    List<GroupDetails> groupDetails = _unitOfWork.GroupsDetails.GetGroupsDetails().Where(g => g.classDetails.ClassID == classId).ToList();
        //    List<GroupDetailsListWithMembersDTO> lstGroupDetailsListWithMembers = new List<GroupDetailsListWithMembersDTO>();
        //    foreach (GroupDetails gd in groupDetails)
        //    {
        //        GroupDetailsListWithMembersDTO objGroupDetailsListWithMembers = new GroupDetailsListWithMembersDTO();
        //        objGroupDetailsListWithMembers.groupID = gd.GroupID;
        //        objGroupDetailsListWithMembers.GroupName = gd.GroupName;
        //        List<GroupStudentMapping> lstGroupStudentMapping = _unitOfWork.GroupStudentMappings.GetGroupStudentMappingsWithGroupID(gd.GroupID).ToList();
        //        foreach (GroupStudentMapping obj in lstGroupStudentMapping)
        //        {
        //            Student objStudent = _unitOfWork.Students.Get(obj.studID);
        //            objGroupDetailsListWithMembers.members += objStudent.studName + ",";
        //        }
        //        if (objGroupDetailsListWithMembers.members != null)
        //        {
        //            objGroupDetailsListWithMembers.members.Trim(',');
        //        }
        //        lstGroupDetailsListWithMembers.Add(objGroupDetailsListWithMembers);
        //    }
        //    return Ok(lstGroupDetailsListWithMembers);

        //}





        //// POST: api/GroupDetails
        //[ResponseType(typeof(GroupDetails))]
        //public IHttpActionResult Post(CreateOrUpdateGroupDetailsInputDTO GroupDetailsDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    Mapper.CreateMap<CreateOrUpdateGroupDetailsInputDTO, GroupDetails>().ForMember(m => m.GroupID, opt => opt.Ignore());

        //    var GroupDetails = Mapper.Map<CreateOrUpdateGroupDetailsInputDTO, GroupDetails>(GroupDetailsDto);
        //    _unitOfWork.GroupsDetails.Add(GroupDetails);
        //    _unitOfWork.Complete();

        //    return Created(new Uri(Request.RequestUri + "/" + GroupDetails.GroupID), GroupDetailsDto);
        //}

        // POST: api/GroupDetails
        
        [ResponseType(typeof(GroupDetails))]
        public IHttpActionResult Post(CreateGroupDetailsWithStudentsDTO groupDetailsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                GroupDetails groupDetails = new GroupDetails();
                groupDetails.GroupName = groupDetailsDto.GroupName;
                groupDetails.ClassID = groupDetailsDto.ClassID;
                groupDetails.CreatioinDate = DateTime.UtcNow;

                _unitOfWork.GroupsDetails.Add(groupDetails);
                _unitOfWork.Complete();

                if (groupDetailsDto.studentIDs != null)
                {
                    foreach (int studentID in groupDetailsDto.studentIDs)
                    {
                        GroupStudentMapping groupStudentMap = new GroupStudentMapping();
                        groupStudentMap.GroupID = groupDetails.GroupID;
                        groupStudentMap.studID = studentID;
                        _unitOfWork.GroupStudentMappings.Add(groupStudentMap);
                        _unitOfWork.Complete();
                    }
                }
                return Created(new Uri(Request.RequestUri + "/" + groupDetails.GroupID), groupDetails);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/GroupDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, CreateOrUpdateGroupDetailsInputDTO groupDetails)
        {
            try
            {
                var groupInDb = _unitOfWork.GroupsDetails.SingleOrDefault(c => c.GroupID == id);

                if (groupInDb == null)
                    return NotFound();

                groupInDb.GroupName = groupDetails.GroupName;
                groupInDb.ClassID = groupDetails.ClassID;
                groupInDb.ModifyDate = DateTime.UtcNow;
                _unitOfWork.GroupsDetails.Update(groupInDb);
                Mapper.CreateMap<GroupDetails, GroupDetailsListDTO>();
                return Ok(Mapper.Map<GroupDetails, GroupDetailsListDTO>(groupInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/GroupDetails/5
        [ResponseType(typeof(GroupDetails))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var groupInDb = _unitOfWork.GroupsDetails.SingleOrDefault(c => c.GroupID == id);
                if (groupInDb == null)
                    return NotFound();
                groupInDb.IsDeleted = true;
                _unitOfWork.GroupsDetails.Update(groupInDb);
                Mapper.CreateMap<GroupDetails, GroupDetailsListDTO>();
                return Ok(Mapper.Map<GroupDetails, GroupDetailsListDTO>(groupInDb));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}

