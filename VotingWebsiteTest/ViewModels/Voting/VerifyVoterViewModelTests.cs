/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voting
{
    [TestClass]
    public class VerifyVoterViewModelTests
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
        public void VerifyVoterViewModelLoad()
        {
            // Arrange


            // Act
            VerifyVoterViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);
        }

        private VerifyVoterViewModel CreateViewModel()
        {
            return new VerifyVoterViewModel();
        }
    }
}
*/