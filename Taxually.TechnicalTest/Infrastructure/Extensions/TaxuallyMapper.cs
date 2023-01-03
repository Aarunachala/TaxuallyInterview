using AutoMapper;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Infrastructure.Extensions
{
    public class VATRegistrationProfile:Profile
    {
        public VATRegistrationProfile()
        {
            CreateMap<VatRegistrationRequest, VatRegistrationRequestDTO>();
        }
    }

    public interface IAutoMapperConfiguration { MapperConfiguration Configure(); }
    public class AutoMapperConfiguration: IAutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VATRegistrationProfile>();
            });
            return config;
        }
    }
}
