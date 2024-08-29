using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class AdminTest
    {
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Fact]
        public void Login()
        {
            //login
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();

            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);

            _chrome.Close();
        }



        [Fact]
        public void ManageUserClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Manage Users")).Click();
            Assert.Contains("Manage Users", _chrome.FindElement(By.Id("HeaderMsg")).Text);
            _chrome.Close();
        }



        [Fact]
        public void SearchUserClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Search Users")).Click();
            Assert.Contains("Search Users", _chrome.FindElement(By.Id("PageTag")).Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("DOB-error")]
        public void SearchUserErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Search Users")).Click();
            _chrome.FindElement(By.Id("submitbutton")).Click();
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Close();
        }

        [Fact]
        public void VerifyVotersClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Verify Voters")).Click();
            Assert.Contains("Verify Voter", _chrome.FindElement(By.Id("PageTag")).Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("FirstName-error")]
        public void VerifyVotersErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Verify Voters")).Click();
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddOfficeClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Add Office")).Click();
            Assert.Contains("Add Office", _chrome.FindElement(By.Id("PageTag")).Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("OfficeName-error")]
        public void AddOfficeErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Add Office")).Click();

            _chrome.FindElement(By.ClassName("btn")).Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddOfficeFunctionalityTest()
        {
            var username = "blah";
            var password = "hello";
            var officeTitle = RandomString(10);
            var officeDescription = "This office deals problems of ece students";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            _chrome.FindElement(By.LinkText("Add Office")).Click();
            _chrome.FindElement(By.Id("OfficeName")).SendKeys(officeTitle);
            _chrome.FindElement(By.Id("OfficeDescription")).SendKeys(officeDescription);
            _chrome.FindElement(By.Id("OfficeLevel")).SendKeys(Keys.ArrowDown);
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddBallotClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Add Ballot")).Click();
            Assert.Contains("Add Ballot", _chrome.FindElement(By.Id("PageTag")).Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("BallotName-error")]
        public void AddBallotErrorTest(string id)
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Add Ballot")).Click();

            _chrome.FindElement(By.ClassName("btn")).Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Close();
        }

        [Fact]
        public void AddCandidateClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("Add Candidate")).Click();
            Assert.Contains("Add Candidate", _chrome.FindElement(By.Id("PageTag")).Text);
            _chrome.Close();
        }

 

        [Fact]
        public void ViewZipCodeMapClickTest()
        {
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username, _chrome.FindElement(By.Id("DashboardMsg")).Text);
            _chrome.FindElement(By.LinkText("View Zip Code Map")).Click();
            Assert.Contains("Create", _chrome.FindElement(By.Id("CreateButton")).Text);
            _chrome.Close();
        }

    }
}