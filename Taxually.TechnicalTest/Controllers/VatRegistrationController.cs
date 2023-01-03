using Microsoft.AspNetCore.Mvc;
using NLog;
using Taxually.TechnicalTest.Infrastructure.Validation;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Service;
using Taxually.TechnicalTest.Utils;
using ILogger = NLog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] authorization here or in the methods
    public class VatRegistrationController : ControllerBase
    {

        private readonly IRegistrationService _registrationService;
        private static readonly ILogger LOG = LogManager.GetCurrentClassLogger();

        public VatRegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost("Post")]
        //[Route("api/vatregistration/post")] if you expect to have several post better to put a route.I also changed to PostAsync to indicate the developer is async.
        [ProducesResponseType(typeof(VatRegistrationRequest), 200)] //Can return a more complex object. In a extended version a list of error must be included.
        public async Task<ActionResult> PostAsync([FromBody] VatRegistrationRequest request)
        {
            VatRegistrationRequest response;
            try
            {

                if (request == null)
                {
                    return BadRequest();
                }

                var isValid = await VATRegistrationValidator.Validate(request);

                if (isValid)
                {
                    switch (request.Country)
                    {
                        case Countries.UK:
                            // UK has an API to register for a VAT number
                            response = await _registrationService.APIRegistration(request);
                            break;
                        case Countries.France:
                            // France requires an excel spreadsheet to be uploaded to register for a VAT number
                            response = await _registrationService.CSVRegistration(request);
                            break;
                        case Countries.Germany:
                            // Germany requires an XML document to be uploaded to register for a VAT number
                            response = await _registrationService.XMLRegistration(request);
                            break;
                        default:
                            throw new Exception("Country not supported");

                    }
                    if (response.IsValid)
                    {
                        LOG.Info("VAT registration created successfully.");
                        return Ok();
                    }
                    else
                        return BadRequest("Something goes wrong during the operation. Please check the logs for more details.");

                    //Other valid returns
                    //if (result.IsValid)
                    //{
                    //    return StatusCode(StatusCodes.Status201Created, result);
                    //}
                    //else
                    //{
                    //    return StatusCode(StatusCodes.Status412PreconditionFailed, result);
                    //}
                    //This can be more complex like returning an object with the result with errorcodes:
                }
                else
                {
                    return StatusCode(StatusCodes.Status412PreconditionFailed, false);
                }
            }
            catch (Exception ex)
            {
                LOG.Error(ex, "Exception creating VAT company : companyId=" + request.CompanyId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Exception while registering VAT company.");
            }
        }
        }
    }

    

