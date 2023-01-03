using AutoMapper;
using Taxually.TechnicalTest.Infrastructure.Extensions;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Infrastructure.Mappers
{
    public class VATRegistrationMapper : IVATRegistrationMapper// interface? if methods gets extended yes
    {
        private readonly IMapper _mapper; 
        public VATRegistrationMapper(IAutoMapperConfiguration config)
        {
            var mapperConfig = config.Configure();
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<VatRegistrationRequestDTO> MapToDTO(VatRegistrationRequest sourceModel)
        {
            return _mapper.Map<VatRegistrationRequestDTO>(sourceModel);
        }
    }
}
