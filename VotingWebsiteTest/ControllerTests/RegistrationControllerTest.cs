using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;

namespace VotingWebsiteTest
{
    [TestClass]
    public class RegistrationControllerTest
    {
        [TestMethod]
        public void RegistrationControllerRegisterGet()
        {
            RegistrationController controller = new RegistrationController();
            ViewResult result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

    }
}
