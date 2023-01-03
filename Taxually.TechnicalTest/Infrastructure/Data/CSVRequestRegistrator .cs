using System.Text;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Service;
using Taxually.TechnicalTest.Utils;

namespace Taxually.TechnicalTest.Infrastructure
{
    public class CSVRequestRegistrator : RegistrationBase
    {

        public async override Task<bool> Register(VatRegistrationRequest csvContent)
        {
            try
            {
                var txtContent = await TaxuallyCSVProfileBuilder.BuildRegistration(csvContent);
                var csv = Encoding.UTF8.GetBytes(txtContent);
                var excelQueueClient = new TaxuallyQueueClient();
                // Queue file to be processed
                excelQueueClient.EnqueueAsync("vat-registration-csv", csv).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
