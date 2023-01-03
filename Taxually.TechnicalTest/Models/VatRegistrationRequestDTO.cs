namespace Taxually.TechnicalTest.Models
{
    //Ideally we pass a DTO model to the Rest API and can be more complex than this.
    public class VatRegistrationRequestDTO //this can go in another project like Taxually.MessageContracts
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string Country { get; set; }
    }
}
