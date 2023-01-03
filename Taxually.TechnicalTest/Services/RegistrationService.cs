using NLog;
using Taxually.TechnicalTest.Infrastructure;
using Taxually.TechnicalTest.Infrastructure.Mappers;
using Taxually.TechnicalTest.Models;
using ILogger = NLog.ILogger;


namespace Taxually.TechnicalTest.Service
{
    public class RegistrationService: IRegistrationService
    {
        private readonly IVATRegistrationMapper _registrationmapper;
        private RegistrationBase _VATRegistrator;
        private static readonly ILogger LOG = LogManager.GetCurrentClassLogger();


        public RegistrationService(IVATRegistrationMapper mapper)
        {
            _registrationmapper = mapper;
        }
            

           public async Task<VatRegistrationRequest> APIRegistration(VatRegistrationRequest request)
           {    
                LOG.Info("RegistrationService/: API Registration for Company {0} started.",request.CompanyId);
                VatRegistrationRequest response;
                //For extra security DTO Patter is good to have so model in reallity must be slightly differnt but not too much.
                var requestDTO =  await _registrationmapper.MapToDTO(request);
                var httpClient = new TaxuallyHttpClient();
                httpClient.PostAsync("https://api.uktax.gov.uk", requestDTO).Wait();
                response = request;
                response.IsValid = true;
                return response;
           }

           public async Task<VatRegistrationRequest> CSVRegistration(VatRegistrationRequest request)
           {
                LOG.Info("RegistrationService/: CSV Registration for Company {0} started.", request.CompanyId);
                VatRegistrationRequest response;
                response = request;
                _VATRegistrator = new CSVRequestRegistrator();
                response.IsValid = await _VATRegistrator.Register(request);
                return response;
           }

           public async Task<VatRegistrationRequest> XMLRegistration(VatRegistrationRequest request)
           {
                LOG.Info("RegistrationService/: CSV Registration for Company {0} started.", request.CompanyId);
                VatRegistrationRequest response;
                response = request;
                _VATRegistrator = new XMLRequestRegistrator();
                response.IsValid = await _VATRegistrator.Register(request);
                return response;
        }
        
    }
}
