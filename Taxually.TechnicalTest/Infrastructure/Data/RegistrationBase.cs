using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Infrastructure
{
    public abstract class RegistrationBase
    {
        public  abstract Task<bool> Register(VatRegistrationRequest registrationRequest);
    }
}
