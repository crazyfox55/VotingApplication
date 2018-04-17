using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.FilteringAndViewing
{
    [TestClass]
    public class UserSearchViewModelTests
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
        public void CreateUserSearchViewModel()
        {
            // Arrange


            // Act
            UserSearchViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private UserSearchViewModel CreateViewModel()
        {
            return new UserSearchViewModel();
        }
    }
}
