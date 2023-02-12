using AutoMapper;
using SolveX.Business.Users.API.Dtos;
using SolveX.Business.Users.Domain.Models;

namespace SolveX.Business.Users.ApplicationServices.Mappings;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<UserDto, UserModel>()
            .ForMember(domain => domain.UserName, opt => opt.MapFrom(dto => dto.Name))
            .ReverseMap();
    }
}
