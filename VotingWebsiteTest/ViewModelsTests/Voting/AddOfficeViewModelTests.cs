using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Voting
{
    [TestClass]
    public class AddOfficeViewModelTests
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
        public void CreateAddOfficeViewModel()
        {
            // Arrange


            // Act
            AddOfficeViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private AddOfficeViewModel CreateViewModel()
        {
            return new AddOfficeViewModel();
        }
    }
}