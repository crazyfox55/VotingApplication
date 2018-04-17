using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Authentication
{
    [TestClass]
    public class LoginViewModelTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateLoginViewModel()
        {
            // Arrange


            // Act
            LoginViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private LoginViewModel CreateViewModel()
        {
            return new LoginViewModel();
        }
    }
}
