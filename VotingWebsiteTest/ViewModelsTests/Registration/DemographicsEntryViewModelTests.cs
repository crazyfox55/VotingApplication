using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Registration
{
    [TestClass]
    public class DemographicsEntryViewModelTests
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
        public void CreateDemographicsEntryViewModel()
        {
            // Arrange


            // Act
            DemographicsEntryViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private DemographicsEntryViewModel CreateViewModel()
        {
            return new DemographicsEntryViewModel();
        }
    }
}
