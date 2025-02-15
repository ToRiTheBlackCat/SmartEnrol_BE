using AutoMapper;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.Services.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Account, StudentAccountProfileModel>();
            CreateMap<StudentAccountProfileModel, Account>()
                .ForMember(StudentAccountProfileModel => StudentAccountProfileModel.AccountId, opt => opt.MapFrom(Account => Account.AccountId))
                .ForMember(StudentAccountProfileModel => StudentAccountProfileModel.AccountName, opt => opt.MapFrom(Account => Account.AccountName))
                .ForMember(StudentAccountProfileModel => StudentAccountProfileModel.Email, opt => opt.MapFrom(Account => Account.Email));
        }
    }
}
