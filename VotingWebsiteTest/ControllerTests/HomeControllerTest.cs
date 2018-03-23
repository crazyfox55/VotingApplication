using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using VotingApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace VotingWebsiteTest
{
    [TestClass]
    public class HomeControllerTest
    {
        
        [TestMethod]
        public void HomeControllerIndex()
        {
            var mockRepo = new Mock<ApplicationDbContext>();
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //mockRepo.UseSqlServer("DefaultConnection");
           // var context = new ApplicationDbContext(mockRepo);
            HomeController controller = new HomeController(mockRepo.Object);
            ViewResult result = controller.Index() as ViewResult;
            //Assert    
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeControllerError()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(("DefaultConnection"));
            var context = new ApplicationDbContext(optionsBuilder.Options);

            HomeController controller = new HomeController(context);
            ViewResult result = controller.Error() as ViewResult;
            //Assert    
            Assert.IsNotNull(result);
        }
    }
}
