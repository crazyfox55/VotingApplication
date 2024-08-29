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
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Profile")).Click();
            Assert.Contains("User Profile", _chrome.FindElement(By.Id("Profile")).Text);

            _chrome.Close();
        }

        [Fact]
        public void ChangePasswordLinkWorks()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Profile")).Click();
            Assert.Contains("User Profile", _chrome.FindElement(By.Id("Profile")).Text);

            _chrome.FindElement(By.LinkText("Change Password")).Click();
            Assert.Contains("Change Password", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.Close();
        }

        [Fact]
        public void SecurityQuestionsLinkWorks()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Profile")).Click();
            Assert.Contains("User Profile", _chrome.FindElement(By.Id("Profile")).Text);

            _chrome.FindElement(By.LinkText("Add Security Questions")).Click();
            Assert.Contains("Add Security Questions", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.Close();
        }

    }
 }
