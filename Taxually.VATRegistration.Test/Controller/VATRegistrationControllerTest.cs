using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Logging;
using Moq;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Taxually.TechnicalTest.Models;

namespace Taxually.VATRegistration.Test.Controller
{
    public class VATRegistrationControllerTest
    {
        private Fixture fixture;
        private VatRegistrationController _vatRegistrationController;
        private Mock<IRegistrationService> _registrationServiceMock;
        private VatRegistrationRequest _vatRegistrationRequestModel;
        private Mock<ILogger> logger;

        [SetUp]
        public void Setup()
        {
            _registrationServiceMock = new Mock<IRegistrationService>();
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
              this.logger = this.fixture.Freeze<Mock<ILogger>>();
            _vatRegistrationRequestModel = fixture.Create<VatRegistrationRequest>();
            _vatRegistrationController = new VatRegistrationController(_registrationServiceMock.Object);
        }

        [Test]
        public void SendRequest()
        {
            bool resultReturned;
            _vatRegistrationController.ControllerContext = new ControllerContext();
            _vatRegistrationController.ControllerContext.HttpContext = new DefaultHttpContext();

            _registrationServiceMock.Setup(x =>  x.XMLRegistration(_vatRegistrationRequestModel)).Returns(Task.FromResult(_vatRegistrationRequestModel));
            var result = _vatRegistrationController.PostAsync(_vatRegistrationRequestModel);
            resultReturned = (bool)((result.Result) as ObjectResult).Value;
            Assert.False(resultReturned);//failed by precondition
            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
