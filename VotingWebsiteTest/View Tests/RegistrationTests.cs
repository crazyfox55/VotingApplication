//using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace VotingWebsiteTest
{
    public class RegistrationTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [Fact]
        public void RegisterUser()
        {
            var username = RandomString(6);
            var password = "123123";
            var email = username + "@gmail.com";
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            Assert.Equal("Email Confirmation Sent", _chrome.FindElementById("sucMsg").Text);

            _chrome.Close();
        }

        [Fact]
        public void DetectTakenUsername()
        {
            var username = RandomString(6);
            var password = "123123";
            var email = username + "@gmail.com";

            //create user
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            //check if user was created
            Assert.Equal("Email Confirmation Sent", _chrome.FindElementById("sucMsg").Text);

            //check that putting the same username causes an error message
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys("");

            Assert.Contains(username, _chrome.FindElementById("Username-error").Text);

            _chrome.Close();
        }

        [Fact]
        public void DetectTakenEmail()
        {
            var username = RandomString(6);
            var password = "123123";
            var email = username + "@gmail.com";

            //create user
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            //check if was created
            Assert.Equal("Email Confirmation Sent", _chrome.FindElementById("sucMsg").Text);

            //check that putting the same email causes error message
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Username").SendKeys("");

            Assert.Contains(email, _chrome.FindElementById("Email-error").Text);

            _chrome.Close();
        }

        [Theory]
        [InlineData("Username-error")]
        [InlineData("Email-error")]
        [InlineData("Password-error")]
        [InlineData("ConfirmPassword-error")]
        public void RegistrationRequiredFieldsErrorNotShownOnLoad(string id)
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");

            //Error messages should not be there yet
            Assert.Throws<NoSuchElementException>(() => { _chrome.FindElementById(id); });

            _chrome.Close();

        }

        [Theory]
        [InlineData("Username-error")]
        [InlineData("Email-error")]
        [InlineData("Password-error")]
        [InlineData("ConfirmPassword-error")]
        public void RegistrationRequiredFieldsErrorShownOnSubmit(string id)
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementById("RegistrationButton").Click();

            //Error messages should now be there
            Assert.NotNull(_chrome.FindElementById(id));

            _chrome.Close();
        }

        [Fact]
        public void PasswordsDifferentCheck()
        {
            var username = RandomString(6);
            var password1 = "123123";
            var password2 = "123124";
            var email = username + "@gmail.com";

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
            Assert.True(passwordMissmatch);

            _chrome.Close();
        }

        [Fact]
        public void RegistrationAlreadyHaveAccountLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementByLinkText("Already have an account?").Click();
            Assert.Contains("Login", _chrome.FindElementById("Login").Text);

            _chrome.Close();
        }

        [Fact]
        public void RegistrationResendEmailConfirmationLinkWorks()
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            _chrome.FindElementByLinkText("Resend Email Confirmation?").Click();
            Assert.Contains("Send Email", _chrome.FindElementByClassName("btn-primary").Text);

            _chrome.Close();
        }
    }
}
