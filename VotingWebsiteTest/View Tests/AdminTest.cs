using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class AdminTest
    {
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        [Fact]
        public void Login()
        {
            //login
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();

            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);

            _chrome.Close();
        }



        [Fact]
        public void ManageUserClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Manage Users").Click();
            Assert.Contains("Manage Users", _chrome.FindElementById("HeaderMsg").Text);
            _chrome.Close();
        }



        [Fact]
        public void SearchUserClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Search Users").Click();
            Assert.Contains("Search Users", _chrome.FindElementById("PageTag").Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("DOB-error")]
        public void SearchUserErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Search Users").Click();
            _chrome.FindElementById("submitbutton").Click();
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Close();
        }

        [Fact]
        public void VerifyVotersClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Verify Voters").Click();
            Assert.Contains("Verify Voter", _chrome.FindElementById("PageTag").Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("FirstName-error")]
        public void VerifyVotersErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Verify Voters").Click();
            _chrome.FindElementByClassName("btn").Click();
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddOfficeClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Add Office").Click();
            Assert.Contains("Add Office", _chrome.FindElementById("PageTag").Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("OfficeName-error")]
        public void AddOfficeErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Add Office").Click();

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddBallotClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Add Ballot").Click();
            Assert.Contains("Add Ballot", _chrome.FindElementById("PageTag").Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("BallotName-error")]
        public void AddBallotErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Add Ballot").Click();

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddCandidateClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("Add Candidate").Click();
            Assert.Contains("Add Candidate", _chrome.FindElementById("PageTag").Text);
            _chrome.Close();
        }

 

        [Fact]
        public void ViewZipCodeMapClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            _chrome.FindElementByLinkText("View Zip Code Map").Click();
            Assert.Contains("Create", _chrome.FindElementById("CreateButton").Text);
            _chrome.Close();
        }

    }
}