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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

namespace VotingWebsiteTest
{
    [TestClass]
    public class UserControllerTest
    {

        [TestMethod]
        public void UserControllerDashboard()
        {
            var mockRepo = new Mock<UserManager<ApplicationUser>>();
           // mockedDB.Setup(x => x.ExecuteScalar(It.IsAny<DbCommand>())).Returns(returnValue);
            var controller = new UserController(mockRepo.Object);
            ViewResult result = controller.Dashboard() as ViewResult;
            //Assert    
            Assert.IsNotNull(result);
        }

    }
}
