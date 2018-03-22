using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;

namespace VotingWebsiteTest
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public void Index()
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
