using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserDashBoardTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "UETJTLCKOS"; //set to one of script users

        //ZLZJTCWVYN, DLVJEJZWCJ, UETJTLCKOS, RPSFWFCDDW
        [Fact]
        public void Login()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.Close();
        }
        [Fact]
        public void NothingSaysEdit()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            //all still say register
            Assert.NotNull(_chrome.FindElementByLinkText("Basic Registration"));
            Assert.NotNull(_chrome.FindElementByLinkText("Register Address"));
            Assert.NotNull(_chrome.FindElementByLinkText("Register Demographics"));
            //check that the finalize button does not do anything yet
            _chrome.FindElementByClassName("btn-primary").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.Close();
        }

        [Fact]
        public void LinksWork()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());

            //Basic Registration
            _chrome.FindElementByLinkText("Basic Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);
            _chrome.Navigate().GoToUrl("http://localhost:5000/VoterRegistration/Dashboard");
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());

            //Address
            _chrome.FindElementByLinkText("Register Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);
            _chrome.Navigate().GoToUrl("http://localhost:5000/VoterRegistration/Dashboard");
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());

            //Demographics
            _chrome.FindElementByLinkText("Register Demographics").Click();
            Assert.Contains("Voter Demographics", _chrome.FindElementByClassName("form-signin-heading").Text);
            _chrome.Navigate().GoToUrl("http://localhost:5000/VoterRegistration/Dashboard");
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            
            _chrome.Close();
        }
    }
}
