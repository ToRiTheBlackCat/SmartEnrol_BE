using AutoMapper;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.ViewModels.Student;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, StudentAccountProfileModel>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId)) // Ensure type compatibility
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.AccountName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.AreaId, opt => opt.MapFrom(src => src.AreaId))
            .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : null)); // Handle potential null Area

        CreateMap<StudentAccountProfileModel, Account>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId)) // Convert back to int
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.AccountName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.AreaId, opt => opt.MapFrom(src => src.AreaId))
            .ForMember(dest => dest.Area, opt => opt.Ignore()); // Prevent accidental overwriting, as Area is a navigation property
    }
}
