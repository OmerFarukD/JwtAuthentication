using AutoMapper;
using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Model;

namespace JwtAuthentication.Service.DtoMappers;

public class DtoMapper : Profile
{
    public DtoMapper()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<UserApp, UserDto>().ReverseMap();
    }
}