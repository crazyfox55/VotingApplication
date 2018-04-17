using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Zoning
{
    [TestClass]
    public class AddDistrictViewModelTests
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
        public void CreateAddDistrictViewModel()
        {
            // Arrange


            // Act
            AddDistrictViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private AddDistrictViewModel CreateViewModel()
        {
            return new AddDistrictViewModel();
        }
    }
}
