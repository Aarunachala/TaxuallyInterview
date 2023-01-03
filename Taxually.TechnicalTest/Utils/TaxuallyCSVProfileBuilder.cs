using System.Text;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Utils
{
    //Asuming that the registration request is going to be bigger than this a class for only create the CVS would be cleaner.
    //This can also be a generic class if you want to build different type of object.
    public static class TaxuallyCSVProfileBuilder
    {
        public static async Task<string> BuildRegistration(VatRegistrationRequest model)
        {
            try
            {
                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("CompanyName,CompanyId");
                csvBuilder.AppendLine($"{model.CompanyName}{model.CompanyId}");
                return csvBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
