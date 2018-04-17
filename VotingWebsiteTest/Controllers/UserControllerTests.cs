using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication;
using VotingApplication.Controllers;

namespace VotingWebsiteTest.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private MockRepository mockRepository;

        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<ApplicationDbContext> mockApplicationDbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockUserManager = this.mockRepository.Create<UserManager<ApplicationUser>>();
            this.mockApplicationDbContext = this.mockRepository.Create<ApplicationDbContext>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange


            // Act
            UserController userController = this.CreateUserController();


            // Assert

        }

        private UserController CreateUserController()
        {
            return new UserController(
                this.mockUserManager.Object,
                this.mockApplicationDbContext.Object);
        }
    }
}
