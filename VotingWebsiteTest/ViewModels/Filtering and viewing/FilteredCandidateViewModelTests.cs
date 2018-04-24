using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class FilteredCandidateViewModelTests
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
    public void FilteredCandidateViewModelLoad()
    {
        // Arrange


        // Act
        FilteredCandidateViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

    }

    private FilteredCandidateViewModel CreateViewModel()
    {
        return new FilteredCandidateViewModel();
    }
}
}
