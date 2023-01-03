using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Taxually.TechnicalTest.Service;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Infrastructure.Mappers;

namespace Taxually.VATRegistration.Test.Controller
{
    public class RegistrationServiceTest
    {
        private Fixture fixture;
        private VatRegistrationRequest _vatRegistrationRequestModel;
        private Mock<IVATRegistrationMapper> _mockmapper;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            _vatRegistrationRequestModel = fixture.Create<VatRegistrationRequest>();
            _mockmapper = new Mock<IVATRegistrationMapper>();
        }

        [Test]
        public void RegisterinAPI()
        {

            #region Arrange
            var VATService = new RegistrationService(_mockmapper.Object);
            #endregion

            #region Act
            var returnedObject = VATService.APIRegistration(_vatRegistrationRequestModel);
            #endregion

            #region Assert
            Assert.IsInstanceOf(typeof(VatRegistrationRequest), returnedObject.Result);
            Assert.True((returnedObject.Result as VatRegistrationRequest).IsValid);
            #endregion

        }

        [Test]
        public void RegisterXML()
        {

            #region Arrange
            var VATService = new RegistrationService(_mockmapper.Object);
            #endregion

            #region Act
            var returnedObject = VATService.XMLRegistration(_vatRegistrationRequestModel);
            #endregion

            #region Assert
            Assert.IsInstanceOf(typeof(VatRegistrationRequest), returnedObject.Result);
            Assert.True((returnedObject.Result as VatRegistrationRequest).IsValid);
            #endregion

        }

        [Test]
        public void RegisterCVS()
        {

            #region Arrange
            var VATService = new RegistrationService(_mockmapper.Object);
            #endregion

            #region Act
            var returnedObject = VATService.CSVRegistration(_vatRegistrationRequestModel);
            #endregion

            #region Assert
            Assert.IsInstanceOf(typeof(VatRegistrationRequest), returnedObject.Result);
            Assert.True((returnedObject.Result as VatRegistrationRequest).IsValid);
            #endregion

        }
    }
}
