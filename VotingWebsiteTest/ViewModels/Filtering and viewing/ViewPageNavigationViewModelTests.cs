using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class ViewPageNavigationViewModelTests
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
    public void ViewPageNavigationViewLoad()
    {
        // Arrange


        // Act
        ViewPageNavigationViewModel viewModel = this.CreateViewModel();


        // Assert
        Assert.IsNotNull(viewModel);

    }

    private ViewPageNavigationViewModel CreateViewModel()
    {
        return new ViewPageNavigationViewModel();
    }
}
}
