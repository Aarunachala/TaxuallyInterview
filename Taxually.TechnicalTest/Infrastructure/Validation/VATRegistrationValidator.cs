using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Utils;

namespace Taxually.TechnicalTest.Infrastructure.Validation
{
    public static class VATRegistrationValidator
    {

       

        public async static Task<bool> Validate(VatRegistrationRequest model)
        {
            if (string.IsNullOrEmpty(model.CompanyName) || string.IsNullOrEmpty(model.Country) || model.CompanyId<=0)
                return false;

            if(model.Country.Equals(Countries.UK) || model.Country.Equals(Countries.France) || model.Country.Equals(Countries.Germany))
                return true;
            else
                return false;

        }
    }
}
