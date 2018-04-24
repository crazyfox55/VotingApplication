using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;

namespace VotingWebsiteTest
{
    [TestClass]
    public class AboutControllerTest
    {
        [TestMethod]
        public void AboutControllerIndexGet()
        {
            AboutController controller = new AboutController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
