using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Registration
{
    [TestClass]
    public class RegistrationViewModelTests
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
        public void CreateRegistrationViewModel()
        {
            // Arrange


            // Act
            RegistrationViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private RegistrationViewModel CreateViewModel()
        {
            return new RegistrationViewModel();
        }
    }
}
