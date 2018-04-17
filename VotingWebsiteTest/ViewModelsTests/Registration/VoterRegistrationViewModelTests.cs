using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Registration
{
    [TestClass]
    public class VoterRegistrationViewModelTests
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
        public void CreateVoterRegistrationViewModel()
        {
            // Arrange


            // Act
            VoterRegistrationViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private VoterRegistrationViewModel CreateViewModel()
        {
            return new VoterRegistrationViewModel();
        }
    }
}
