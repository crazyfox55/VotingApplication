using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;
using Moq;

namespace VotingWebsiteTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void HomeControllerIndexGet()
        {
            var context = new Mock<ApplicationDbContext>();
            HomeController controller = new HomeController(context.Object);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void HomeControllerErrorGet()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Error() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
