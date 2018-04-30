using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserProfileTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "GCTZSBARUJ"; //set to one of script users
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
            _chrome.FindElementByLinkText("Profile").Click();
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.Close();
        }

        [Fact]
        public void ChangePasswordLinkWorks()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Profile").Click();
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.Close();
        }

        [Fact]
        public void SecurityQuestionsLinkWorks()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Profile").Click();
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.FindElementByLinkText("Add Security Questions").Click();
            Assert.Contains("Add Security Questions", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.Close();
        }

    }
 }
