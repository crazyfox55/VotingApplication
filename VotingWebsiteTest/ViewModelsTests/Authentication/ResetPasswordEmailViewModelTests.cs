using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Authentication
{
    [TestClass]
    public class ResetPasswordEmailViewModelTests
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
        public void CreateResetPasswordEmailViewModel()
        {
            // Arrange


            // Act
            ResetPasswordEmailViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private ResetPasswordEmailViewModel CreateViewModel()
        {
            return new ResetPasswordEmailViewModel();
        }
    }
}
