using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Utils;

namespace Taxually.TechnicalTest.Infrastructure
{
    public class XMLRequestRegistrator : RegistrationBase
    {
        public XMLRequestRegistrator()  {}

        public async override Task<bool> Register(VatRegistrationRequest xmlContent)
        {
            try
            {
                var xml = TaxuallyXMLSerializer.SerializeXML<VatRegistrationRequest>(xmlContent);
                var xmlQueueClient = new TaxuallyQueueClient();
                xmlQueueClient.EnqueueAsync("vat-registration-xml", xml).Wait(); // Register the xml
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
