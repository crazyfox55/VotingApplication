using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.User
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
        public void SecurityQuestionViewModelLoad()
        {
            // Arrange


            // Act
            SecurityQuestionViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private SecurityQuestionViewModel CreateViewModel()
        {
            return new SecurityQuestionViewModel();
        }
    }
}
