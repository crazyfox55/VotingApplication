using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Authentication
{
    [TestClass]
    public class SecurityQuestionViewModelTests
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
        public void CreateSecurityQuestionViewModel()
        {
            // Arrange


            // Act
            SecurityQuestionViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private SecurityQuestionViewModel CreateViewModel()
        {
            return new SecurityQuestionViewModel();
        }
    }
}
