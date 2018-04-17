using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Registration
{
    [TestClass]
    public class ConfirmEmailViewModelTests
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
        public void CreateConfirmationViewModel()
        {
            // Arrange


            // Act
            ConfirmEmailViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private ConfirmEmailViewModel CreateViewModel()
        {
            return new ConfirmEmailViewModel();
        }
    }
}
