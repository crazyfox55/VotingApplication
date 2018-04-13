using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;

namespace VotingWebsiteTest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void AdminControllerDashboardGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.Dashboard() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerUserSearchGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.UserSearch() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerVerifyVoterGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.VerifyVoter() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerAddOfficeGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.AddOffice() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerAddBallotGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.AddBallot() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerAddCandidateGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.AddCandidate() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerUserManagementGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.UserManagement() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AdminControllerZipCodeMapGet()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.ZipCodeMap() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
