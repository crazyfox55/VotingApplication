using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace VotingWebsiteTest
{
    [TestClass]
    public class RegistrationTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        [TestMethod]
        public void RegisterUser()
        {
            var username = "testUser1";
            var password = "123123";
            var email = "testingUser1@gmail.com";
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
            var username = "testUser2";
            var password = "123123";
            var email = "testingUser2@gmail.com";

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
            var username = "testUser3";
            var password = "123123";
            var email = "testingUser3@gmail.com";

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

        [TestMethod]
        public void RegistrationAlreadyHaveAccountLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementByLinkText("Already have an account?").Click();
            Assert.IsTrue(_chrome.FindElementById("Login").Text.Contains("Login"));
        }

        [TestMethod]
        public void RegistrationResendEmailConfirmationLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementByLinkText("Resend Email Confirmation?").Click();
            Assert.IsTrue(_chrome.FindElementByClassName("btn-primary").Text.Contains("Send Email"));
        }
    }
}
