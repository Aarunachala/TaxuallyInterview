using AutoMapper;

namespace Taxually.TechnicalTest.Infrastructure.Extensions
{
    public interface IMapperFactory
    {
        IMapper GetMapper(string mapperName = "");
    }

    
}
