/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class BasicUserSearchViewModelTests
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
    public void BasicUserSearchViewModelLoad()
    {
        // Arrange


        // Act
        BasicUserSearchViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

    }

    private BasicUserSearchViewModel CreateViewModel()
    {
        return new BasicUserSearchViewModel();
    }
}
}
*/