using AutoMapper;
using SolveX.Business.Assets.API.Dtos;
using SolveX.Business.Assets.Domain.Models;

namespace SolveX.Business.Users.ApplicationServices.Mappings;

public class AssetMap : Profile
{
    public AssetMap()
    {
        CreateMap<AssetDto, Asset>()
            .ReverseMap();
    }
}