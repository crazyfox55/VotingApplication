using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voter.Registration
{
    [TestClass]
    public class VoterAddressViewModelTests
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
        public void VoterAddressViewModelLoad()
        {
            // Arrange


            // Act
            VoterAddressViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

        }

        private VoterAddressViewModel CreateViewModel()
        {
            return new VoterAddressViewModel();
        }
    }
}
