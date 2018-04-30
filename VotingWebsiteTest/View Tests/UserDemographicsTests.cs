using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserDemograhicsTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "HCOOWWXDBT"; //set to one of script users
        string pass = "hello"; //set to password

        [Fact]
        public void NavigateToPage()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Demographics").Click();
            Assert.Contains("Voter Demographics", _chrome.FindElementByClassName("form-signin-heading").Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("Party-error")]
        [InlineData("Ethnicity-error")]
        [InlineData("Sex-error")]
        [InlineData("IncomeRange-error")]
        [InlineData("VoterReadiness-error")]
        public void ErrorMessages(string id)
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Demographics").Click();
            Assert.Contains("Voter Demographics", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Dispose();
        }

        //have to change user for each run through
        [Fact]
        public void FillOutForm()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Demographics").Click();
            Assert.Contains("Voter Demographics", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.FindElementById("Party").Click();
            _chrome.FindElementById("Party").SendKeys(Keys.ArrowDown);

            _chrome.FindElementById("Ethnicity").Click();
            _chrome.FindElementById("Ethnicity").SendKeys(Keys.ArrowDown);

            _chrome.FindElementById("Sex").Click();
            _chrome.FindElementById("Sex").SendKeys(Keys.ArrowDown);

            _chrome.FindElementById("IncomeRange").Click();
            _chrome.FindElementById("IncomeRange").SendKeys(Keys.ArrowDown);

            _chrome.FindElementById("VoterReadiness").Click();
            _chrome.FindElementById("VoterReadiness").SendKeys(Keys.ArrowDown);

            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Demographics"));

            _chrome.Dispose();
        }
        
    }
}
