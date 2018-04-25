using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class FilteredUsersViewModelTests
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
    public void FilteredUserViewModelLoad()
    {
        // Arrange


        // Act
        FilteredUsersViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

    }

    private FilteredUsersViewModel CreateViewModel()
    {
        return new FilteredUsersViewModel();
    }
}
}
