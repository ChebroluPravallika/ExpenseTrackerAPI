using AutoMapper;
using ExpenseTracker.Application.Models;
using ExpenseTracker.Infrastructure.Models;

namespace ExpenseTracker.Application
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<TransactionDTO, Transaction>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
