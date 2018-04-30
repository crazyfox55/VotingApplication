/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class ManageUserViewModelTests
{
    private MockRepository mockRepository;

    private Mock<ApplicationUser> mockApplicationUser;

    [TestInitialize]
    public void TestInitialize()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockApplicationUser = this.mockRepository.Create<ApplicationUser>();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        this.mockRepository.VerifyAll();
    }

    [TestMethod]
    public void ManageUserViewModelLoad()
    {
        // Arrange


        // Act
        ManageUserViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

    }

    private ManageUserViewModel CreateViewModel()
    {
        return new ManageUserViewModel(
            this.mockApplicationUser.Object);
    }
}
}*/
