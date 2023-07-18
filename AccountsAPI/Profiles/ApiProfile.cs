using AccountsAPI.Dtos;
using AccountsAPI.Models;
using AutoMapper;

namespace AccountsAPI.Profiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<CreateAccountDto, CurrentAccount>();
            CreateMap<CreateAccountDto, SavingsAccount>();
            CreateMap<UpdateAccountDto, Account>();

            CreateMap<Account, ReturnAccountDto>();
        }
    }
}
