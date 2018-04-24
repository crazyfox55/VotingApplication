using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;

namespace VotingWebsiteTest
{
    [TestClass]
    public class AboutControllerTest
    {
        [TestMethod]
        public void AboutControllerIndex()
        {

            //Arrange
            AboutController controller = new AboutController();
            //Act
            ViewResult result = controller.Index() as ViewResult;
            //Assert    
            Assert.IsNotNull(result);

        }
    }
}