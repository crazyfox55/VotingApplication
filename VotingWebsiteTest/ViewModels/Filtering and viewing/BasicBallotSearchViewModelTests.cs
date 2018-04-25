using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VotingApplication.ViewModels;

namespace VotingWebsiteTest.ViewModels.Filtering
{
	[TestClass]
public class BasicBallotSearchViewModelTests
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
    public void BasicBallotSearchViewModelLoad()
    {
        // Arrange


        // Act
        BasicBallotSearchViewModel viewModel = this.CreateViewModel();


            // Assert
            Assert.IsNotNull(viewModel);

    }

    private BasicBallotSearchViewModel CreateViewModel()
    {
        return new BasicBallotSearchViewModel();
    }
}
}
