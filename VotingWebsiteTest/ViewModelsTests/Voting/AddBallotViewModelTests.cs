using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voting
{
    [TestClass]
    public class AddBallotViewModelTests
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
        public void CreateAddBallotViewModel()
        {
            // Arrange


            // Act
            AddBallotViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private AddBallotViewModel CreateViewModel()
        {
            return new AddBallotViewModel();
        }
    }
}
