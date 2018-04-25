/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Microsoft.AspNetCore.Mvc.ViewFeatures;


                                    /*
namespace VotingWebsiteTest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void AdminControllerDashboardGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);

            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);
            var service = new AdminController(mockContext.Object);

            var meta = new Mock<IModelMetadataProvider>();
            var model = new Mock<ModelStateDictionary>();

            service.ViewData = new ViewDataDictionary(meta.Object,model.Object);
            service.Dashboard();
            //ViewDataDictionary viewData = result.ViewData;
            Assert.IsTrue(service.ViewData != null);
            //Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AdminControllerUserSearchGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);

            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);
            var service = new AdminController(mockContext.Object);
            ViewResult result = service.UserSearch() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AdminControllerVerifyVoterGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);

            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);
            var service = new AdminController(mockContext.Object);
            ViewResult result = service.VerifyVoter() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AdminControllerAddOfficeGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);

            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);
            var service = new AdminController(mockContext.Object);
            ViewResult result = service.AddOffice() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AdminControllerAddBallotGet()
        {

        }
        [TestMethod]
        public void AdminControllerAddCandidateGet()
        {
  
        }
        [TestMethod]
        public void AdminControllerUserManagementGet()
        {
  
        }
        [TestMethod]
        public void AdminControllerZipCodeMapGet()
        {
  
        }
    }
}
                  */