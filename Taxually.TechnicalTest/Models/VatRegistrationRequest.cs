namespace Taxually.TechnicalTest.Models
{
    public class VatRegistrationRequest
    {
        public string CompanyName { get; set; }
        public int CompanyId { get; set; } //converted to int. It is less likely to be a string. Usually is either a Guid or a int.
        public string Country { get; set; }

        public bool IsValid { get; set; }
    }
}
