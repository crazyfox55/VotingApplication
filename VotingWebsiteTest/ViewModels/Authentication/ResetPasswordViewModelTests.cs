using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Authentication
{
    [TestClass]
    public class ResetPasswordViewModelTests
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
        public void ResetPasswordViewModelLoad()
        {
            // Arrange


            // Act
            ResetPasswordViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private ResetPasswordViewModel CreateViewModel()
        {
            return new ResetPasswordViewModel();
        }
    }
}
