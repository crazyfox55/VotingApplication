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
        public void ManageUser()
        {
            //login
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            _chrome.FindElementById("UserManagement").Click();

            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);

            _chrome.Close();
        }
    }
}