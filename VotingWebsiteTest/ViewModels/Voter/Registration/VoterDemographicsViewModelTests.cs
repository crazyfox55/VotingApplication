/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voter.Registration
{
    [TestClass]
    public class VoterDemographicsViewModelTests
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
        public void VoterDemographicsViewModelLoad()
        {
            // Arrange


            // Act
            VoterDemographicsViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private VoterDemographicsViewModel CreateViewModel()
        {
            return new VoterDemographicsViewModel();
        }
    }
}
*/