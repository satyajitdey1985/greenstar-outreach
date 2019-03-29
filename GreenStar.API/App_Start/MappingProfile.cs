using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.API.Dtos.School;
using GreenStar.Entity.Academy;
using GreenStar.Entity.Holiday;

namespace GreenStar.API.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to Dto            
            Mapper.CreateMap<HolidayCalendar, HolidayCalendarDto>();

            //Dto to Domain
            Mapper.CreateMap<HolidayCalendarDto, HolidayCalendar>()
                .ForMember(m => m.HolidayID, opt => opt.Ignore());


            Mapper.CreateMap<School, SchoolDto>();
            //Mapper.CreateMap<CreateOrUpdateSchoolInputDTO, School>().ForMember(m => m.schlID, opt => opt.Ignore());

            Mapper.CreateMap<ClassDetails, ClassDetailsDto>();
            Mapper.CreateMap<ClassDetailsDto, ClassDetails>().ForMember(m => m.ClassID, opt => opt.Ignore());


            Mapper.CreateMap<ParameterAttribute, ParameterAttributeListDto>();
            Mapper.CreateMap<ParameterAttributeListDto, ParameterAttribute>().ForMember(m => m.ParamID, opt => opt.Ignore());


            //Mapper.CreateMap<GroupDetails, CreateOrUpdateGroupDetailsInputDTO>();
            //Mapper.CreateMap<CreateOrUpdateGroupDetailsInputDTO, GroupDetails>().ForMember(m => m.GroupID, opt => opt.Ignore());
            //Mapper.CreateMap<GroupStudentMappingListDto, GroupStudentMapping>().ForMember(gd=>gd.groupDetails,opt=>opt.Ignore()).ForMember(stud=>stud.student,opt=>opt.Ignore());
        }
    }
}