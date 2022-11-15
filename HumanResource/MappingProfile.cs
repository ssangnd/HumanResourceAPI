using Entities.DTOs;
using Entities.Models;
using AutoMapper;

namespace HumanResource
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress, opt =>
                opt.MapFrom(x => string.Join("", x.Address,x.Country)));

            CreateMap<CompanyCreationDto, Company>();
            CreateMap<CompanyUpdatingDto, Company>().ReverseMap();
        }
    }
}
