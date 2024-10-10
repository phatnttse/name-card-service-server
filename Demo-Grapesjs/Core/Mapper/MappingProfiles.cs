using AutoMapper;
using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Core.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ImageDto, Image>().ReverseMap();
            CreateMap<NameCardTemplateDto, NameCardTemplate>().ReverseMap();
            CreateMap<InsertUpdateNameCardTemplateDto, NameCardTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<InsertUpdateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
