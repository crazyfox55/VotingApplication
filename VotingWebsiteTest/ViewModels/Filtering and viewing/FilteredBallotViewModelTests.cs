using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class FilteredBallotViewModelTests
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
    public void FilteredBallotViewModelLoad()
    {
        // Arrange


        // Act
        FilteredBallotViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

    }

    private FilteredBallotViewModel CreateViewModel()
    {
        return new FilteredBallotViewModel();
    }
}
}
