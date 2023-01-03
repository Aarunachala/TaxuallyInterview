using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Service
{
    public interface IRegistrationService
    {

        Task<VatRegistrationRequest> APIRegistration(VatRegistrationRequest request);
        Task<VatRegistrationRequest> CSVRegistration(VatRegistrationRequest request);
        Task<VatRegistrationRequest> XMLRegistration(VatRegistrationRequest request);
    }
}
