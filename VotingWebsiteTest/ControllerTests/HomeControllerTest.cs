/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace VotingWebsiteTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void HomeControllerIndexGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);
            
            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);
            var service = new HomeController(mockContext.Object);
            ViewResult result = service.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void HomeControllerErrorGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);

            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);
            var service = new HomeController(mockContext.Object);
            ViewResult result = service.Error() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}*/
