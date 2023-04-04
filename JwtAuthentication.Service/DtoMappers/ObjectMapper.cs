using AutoMapper;

namespace JwtAuthentication.Service.DtoMappers;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DtoMapper>();
        });
        return config.CreateMapper();
    });

    public static IMapper Mapper => Lazy.Value;
}