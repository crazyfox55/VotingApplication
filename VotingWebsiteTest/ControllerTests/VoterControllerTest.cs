using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VotingApplication.Services;
using Moq;

namespace VotingWebsiteTest
{
    [TestClass]
    public class VoterControllerTest
    {
        [TestMethod]
        public void VoterControllerRegistrationGet()
        {
            VoterController controller = new VoterController();
            ViewResult result = controller.Registration() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VoterControllerDemographicsGet()
        {
            VoterController controller = new VoterController();
            ViewResult result = controller.Demographics() as ViewResult;
            Assert.IsNotNull(result);
        }

    }
}
