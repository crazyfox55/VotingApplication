using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;

namespace VotingWebsiteTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void UserControllerDashboardGet()
        {
            UserController controller = new UserController();
            ViewResult result = controller.Dashboard() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UserControllerProfileGet()
        {
            UserController controller = new UserController();
            ViewResult result = controller.Profile() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UserControllerChangePasswordGet()
        {
            UserController controller = new UserController();
            ViewResult result = controller.ChangePassword() as ViewResult;
            Assert.IsNotNull(result);
        }

    }
}
