using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voter.Registration
{
    [TestClass]
    public class VoterDashboardViewModelTests
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
        public void VoterDashboardViewModelLoad()
        {
            // Arrange


            // Act
            VoterDashboardViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private VoterDashboardViewModel CreateViewModel()
        {
            return new VoterDashboardViewModel();
        }
    }
}
