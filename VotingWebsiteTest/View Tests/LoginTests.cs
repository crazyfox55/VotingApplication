using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class LoginTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "blah"; //set to any user
        string pass = "hello"; //set to password

        [Fact]
        public void Login()
        {
            //login
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            
            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);

            _chrome.Close();
        }

        [Fact]
        public void Logout()
        {
            //login
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();

            Assert.Contains(username, _chrome.FindElementById("DashboardMsg").Text);
            
            //logout
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/LogoutAsync");
            try
            {
                Assert.NotNull(_chrome.FindElementById("Login"));
            }
            catch (NoSuchElementException) { }

            _chrome.Close();
        }

        [Fact]
        public void InvalidLogin()
        {
            //login
            var username = "DDfbsgdnkdsjlF";
            var password = "dsgljkep124";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();

            Assert.Equal("Invalid username or password.", _chrome.FindElementByClassName("validation-summary-errors").Text);

            _chrome.Close();
        }

        [Theory]
        [InlineData("Username-error")]
        [InlineData("Password-error")]
        public void LoginRequiredFieldsErrorNotShownOnLoad(string id)
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");

            //Error messages should not be there yet
            Assert.Throws<NoSuchElementException>(() => { _chrome.FindElementById(id); });

            _chrome.Close();
        }

        [Theory]
        [InlineData("Username-error")]
        [InlineData("Password-error")]
        public void LoginRequiredFieldsErrorShownOnSubmit(string id)
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Login").Click();

            //Error messages should now be there
            Assert.Contains("required", _chrome.FindElementById(id).Text);

            _chrome.Close();
        }

        [Fact]
        public void LoginNewUserLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementByLinkText("New User?").Click();

            Assert.NotNull(_chrome.FindElementById("RegistrationButton"));

            _chrome.Close();
        }

        [Fact]
        public void LoginForgotPasswordLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementByLinkText("Forgot Password?").Click();

            Assert.Contains("Send Email", _chrome.FindElementByClassName("btn-primary").Text);

            _chrome.Close();
        }

        [Fact]
        public void LoginResendEmailConfirmationLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementByLinkText("Resend Email Confirmation?").Click();

            Assert.Contains("Send Email", _chrome.FindElementByClassName("btn-primary").Text);

            _chrome.Close();
        }
    }
 }
