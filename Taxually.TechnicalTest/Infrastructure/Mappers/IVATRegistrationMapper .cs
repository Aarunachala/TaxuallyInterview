using AutoMapper;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Infrastructure.Mappers
{
    public interface IVATRegistrationMapper // interface? if methods gets extended yes
    {
        Task<VatRegistrationRequestDTO> MapToDTO(VatRegistrationRequest sourceModel);
    }
}
