/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.User
{
    [TestClass]
    public class ChangePasswordViewModelTests
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
        public void ChangePasswordViewModelLoad()
        {
            // Arrange


            // Act
            ChangePasswordViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private ChangePasswordViewModel CreateViewModel()
        {
            return new ChangePasswordViewModel();
        }
    }
}
*/