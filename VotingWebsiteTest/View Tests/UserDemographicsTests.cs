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
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Register Demographics")).Click();
            Assert.Contains("Voter Demographics", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
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
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Register Demographics")).Click();
            Assert.Contains("Voter Demographics", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.FindElement(By.ClassName("btn")).Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Dispose();
        }

        //have to change user for each run through
        [Fact]
        public void FillOutForm()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Register Demographics")).Click();
            Assert.Contains("Voter Demographics", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.FindElement(By.Id("Party")).Click();
            _chrome.FindElement(By.Id("Party")).SendKeys(Keys.ArrowDown);

            _chrome.FindElement(By.Id("Ethnicity")).Click();
            _chrome.FindElement(By.Id("Ethnicity")).SendKeys(Keys.ArrowDown);

            _chrome.FindElement(By.Id("Sex")).Click();
            _chrome.FindElement(By.Id("Sex")).SendKeys(Keys.ArrowDown);

            _chrome.FindElement(By.Id("IncomeRange")).Click();
            _chrome.FindElement(By.Id("IncomeRange")).SendKeys(Keys.ArrowDown);

            _chrome.FindElement(By.Id("VoterReadiness")).Click();
            _chrome.FindElement(By.Id("VoterReadiness")).SendKeys(Keys.ArrowDown);

            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Demographics")));

            _chrome.Dispose();
        }
        
    }
}
