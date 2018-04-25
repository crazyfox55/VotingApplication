using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using VotingApplication.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using VotingApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;
public class TestUserManager : UserManager<ApplicationUser>
{
    public TestUserManager() : base(new Mock<IUserStore<ApplicationUser>>().Object,
    new Mock<IOptions<IdentityOptions>>().Object,
    new Mock<IPasswordHasher<ApplicationUser>>().Object,
    new Mock<IEnumerable<IUserValidator<ApplicationUser>>>().Object,
    new Mock<IEnumerable<IPasswordValidator<ApplicationUser>>>().Object,
    new Mock<ILookupNormalizer>().Object,
    new Mock<IdentityErrorDescriber>().Object,
    new Mock<IServiceProvider>().Object,
    new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
    { }
}
/*
namespace VotingWebsiteTest
{
    
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void UserControllerDashboardGet()
        {
            var mockSet = new Mock<DbSet<SettingsDataModel>>();
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(options);
            mockContext.Setup(m => m.Settings).Returns(mockSet.Object);

            var mockApp = new Mock<ApplicationUser>();
            
            var store = new Mock<IUserStore<ApplicationUser>>();


            CancellationToken token = new CancellationToken();
            store.Setup(s => s.FindByIdAsync("testId",token)).ReturnsAsync(new ApplicationUser
            {
                Id = "testId",
                Email = "test@email.com"
            });
            var mockId = new Mock<IdentityOptions>();
            var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
            optionsAccessor.Setup(m => m.Value).Returns(mockId.Object);

            var passwordHasher = new Mock<IPasswordHasher<ApplicationUser>>();
            passwordHasher.Setup(s => s.HashPassword(mockApp.Object, "")).Returns("");

            var userValidators = new Mock<IEnumerable<IUserValidator<ApplicationUser>>>();
            var mockIen = new Mock<IEnumerator<IUserValidator<ApplicationUser>>>();
            userValidators.Setup(a => a.GetEnumerator()).Returns(mockIen.Object);

            var passwordValidators = new Mock<IEnumerable<IPasswordValidator<ApplicationUser>>>();
            var keyNormalizer = new Mock<ILookupNormalizer>();
            var errors = new Mock<IdentityErrorDescriber>();
            var services = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<ApplicationUser>>>();

           var manager =
                new UserManager<ApplicationUser>(store.Object,optionsAccessor.Object,
                passwordHasher.Object,userValidators.Object,passwordValidators.Object,
                keyNormalizer.Object,errors.Object,services.Object,logger.Object);

            //TestUserManager manager = new TestUserManager();
            var service = new UserController(manager,mockContext.Object);
            ViewResult result = service.Dashboard() as ViewResult;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void UserControllerProfileGet()
        {
           
        }
        [TestMethod]
        public void UserControllerChangePasswordGet()
        {
          
        }

    }
}
*/