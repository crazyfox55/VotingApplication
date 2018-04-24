using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace VotingWebsiteTest
{
    [TestClass]
    public class RegistrationViewTest
    {
        ChromeDriver _chrome = new ChromeDriver(@"C:\Users\colemtg\source\repos");

        [TestMethod]
        public void RegisterUser()
        {
            var username = "testUser10";
            var password = "123123";
            var email = "testingUser10@gmail.com";
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            var userWasCreated =
                _chrome.FindElementById("sucMsg").Text.Equals("Email Confirmation Sent");
            Assert.IsTrue(userWasCreated);
        }

        [TestMethod]
        public void DetectTakenUsername()
        {
            var username = "testUser20";
            var password = "123123";
            var email = "testingUser20@gmail.com";

            //create user
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            //check if user was created
            var userWasCreated =
                _chrome.FindElementById("sucMsg").Text.Equals("Email Confirmation Sent");
            Assert.IsTrue(userWasCreated);

            //check that putting the same username causes an error message
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys("");
            var userTaken =
                _chrome.FindElementById("Username-error").Text.Contains(username);
            Assert.IsTrue(userTaken);
        }

        [TestMethod]
        public void DetectTakenEmail()
        {
            var username = "testUser30";
            var password = "123123";
            var email = "testingUser30@gmail.com";

            //create user
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            //check if was created
            var userWasCreated =
                _chrome.FindElementById("sucMsg").Text.Equals("Email Confirmation Sent");
            Assert.IsTrue(userWasCreated);

            //check that putting the same email causes error message
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Username").SendKeys("");
            var emailTaken =
                _chrome.FindElementById("Email-error").Text.Contains(email);
            Assert.IsTrue(emailTaken);
        }
        [TestMethod]
        public void RegistrationRequiredFieldsError()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");

            //Error messages should not be there yet

            try
            {
                _chrome.FindElementById("Username-error");
                Assert.Fail();
            }
            catch (NoSuchElementException) { }

            try
            {
                _chrome.FindElementById("Email-error");
                Assert.Fail();
            }
            catch (NoSuchElementException) { }

            try
            {
                _chrome.FindElementById("Password-error");
                Assert.Fail();
            }
            catch (NoSuchElementException) { }

            try
            {
                _chrome.FindElementById("ConfirmPassword-error");
                Assert.Fail();
            }
            catch (NoSuchElementException) { }
            _chrome.FindElementById("RegistrationButton").Click();

            //Error messages should now be there
            Assert.IsNotNull(_chrome.FindElementById("Username-error"));
            Assert.IsNotNull(_chrome.FindElementById("Email-error"));
            Assert.IsNotNull(_chrome.FindElementById("Password-error"));
            Assert.IsNotNull(_chrome.FindElementById("ConfirmPassword-error"));

        }

        [TestMethod]
        public void PasswordsDifferentCheck()
        {
            var username = "testUser4";
            var password1 = "123123";
            var password2 = "123124";
            var email = "testingUser4@gmail.com";

            //create user
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password1);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password2);
            _chrome.FindElementById("RegistrationButton").Click();

            var passwordMissmatch =
                _chrome.FindElementById("ConfirmPassword-error").
                Text.Equals("The password and confirmation password do not match.");
            Assert.IsTrue(passwordMissmatch);
        }
    }
}
