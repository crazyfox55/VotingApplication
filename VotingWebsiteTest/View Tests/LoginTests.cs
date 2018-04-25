using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace VotingWebsiteTest.View_Tests
{
    [TestClass]
    public class LoginTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        [TestMethod]
        public void Login()
        {
            //login
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            var loginSuccess = _chrome.FindElementById("DashboardMsg").Text.Contains(username);
            Assert.IsTrue(loginSuccess);
        }

        [TestMethod]
        public void Logout()
        {
            //login
            var username = "blah";
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            var loginSuccess = _chrome.FindElementById("DashboardMsg").Text.Contains(username);
            Assert.IsTrue(loginSuccess);
            //logout
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/LogoutAsync");
            try
            {
                Assert.IsNotNull(_chrome.FindElementById("Login"));
            }
            catch (NoSuchElementException) { }
        }

        [TestMethod]
        public void InvalidLogin()
        {
            //login
            var username = "DDfbsgdnkdsjlF";
            var password = "dsgljkep124";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            var loginFailed = _chrome.FindElementByClassName("validation-summary-errors").Text.Equals("Invalid username or password.");
            Assert.IsTrue(loginFailed);
        }

        [TestMethod]
        public void LoginRequiredFieldsError()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");

            //Error messages should not be there yet
            try
            {
                _chrome.FindElementById("Username-error");
                Assert.Fail();
            }
            catch (NoSuchElementException) { }

            try
            {
                _chrome.FindElementById("Password-error");
                Assert.Fail();
            }
            catch (NoSuchElementException) { }

            _chrome.FindElementById("Login").Click();

            //Error messages should now be there
            Assert.IsTrue(_chrome.FindElementById("Username-error").Text.Contains("required"));
            Assert.IsTrue(_chrome.FindElementById("Password-error").Text.Contains("required"));

        }
        [TestMethod]
        public void LoginNewUserLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementByLinkText("New User?").Click();
             Assert.IsNotNull(_chrome.FindElementById("RegistrationButton"));

        }

        [TestMethod]
        public void LoginForgotPasswordLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementByLinkText("Forgot Password?").Click();
            Assert.IsTrue(_chrome.FindElementByClassName("btn-primary").Text.Contains("Send Email"));

        }

        [TestMethod]
        public void LoginResendEmailConfirmationLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementByLinkText("Resend Email Confirmation?").Click();
            Assert.IsTrue(_chrome.FindElementByClassName("btn-primary").Text.Contains("Send Email"));

        }
    }
 }
