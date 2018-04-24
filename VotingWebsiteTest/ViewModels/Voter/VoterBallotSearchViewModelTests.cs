using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voter
{
    [TestClass]
    public class VoterBallotSearchViewModelTests
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
        public void VoterBallotSearchViewModelLoad()
        {
            // Arrange


            // Act
            VoterBallotSearchViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private VoterBallotSearchViewModel CreateViewModel()
        {
            return new VoterBallotSearchViewModel();
        }
    }
}
