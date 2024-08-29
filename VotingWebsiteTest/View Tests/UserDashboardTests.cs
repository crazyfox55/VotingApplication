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

        //has to be a new/clean user(nothing filled out yet)
        string user = "UETJTLCKOS"; //set to one of script users
        string pass = "hello"; //set to password

        [Fact]
        public void Login()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.Close();
        }
        [Fact]
        public void NothingSaysEdit()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            //all still say register
            Assert.NotNull(_chrome.FindElement(By.LinkText("Basic Registration")));
            Assert.NotNull(_chrome.FindElement(By.LinkText("Register Address")));
            Assert.NotNull(_chrome.FindElement(By.LinkText("Register Demographics")));
            //check that the finalize button does not do anything yet
            _chrome.FindElement(By.ClassName("btn-primary")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.Close();
        }

        [Fact]
        public void LinksWork()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());

            //Basic Registration
            _chrome.FindElement(By.LinkText("Basic Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            _chrome.Navigate().GoToUrl("http://localhost:5000/VoterRegistration/Dashboard");
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());

            //Address
            _chrome.FindElement(By.LinkText("Register Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            _chrome.Navigate().GoToUrl("http://localhost:5000/VoterRegistration/Dashboard");
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());

            //Demographics
            _chrome.FindElement(By.LinkText("Register Demographics")).Click();
            Assert.Contains("Voter Demographics", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            _chrome.Navigate().GoToUrl("http://localhost:5000/VoterRegistration/Dashboard");
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            
            _chrome.Close();
        }
    }
}
