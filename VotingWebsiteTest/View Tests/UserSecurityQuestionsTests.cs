using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserSecurityQuestionTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "NRRONNDZTX"; //set to one of script users
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

            _chrome.FindElementByLinkText("Add Security Questions").Click();
            Assert.Contains("Add Security Questions", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.Close();
        }

        [Theory]
        [InlineData("SecurityQuestionOne-error")]
        [InlineData("SecurityQuestionTwo-error")]
        [InlineData("SecurityAnswerOne-error")]
        [InlineData("SecurityAnswerTwo-error")]
        public void ErrorMessages(string id)
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

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Dispose();

        }
        [Fact]
        public void MustChooseDifferentCheck()
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

            //choose the same question
            _chrome.FindElementById("SecurityQuestionOne").Click();
            _chrome.FindElementById("SecurityQuestionOne").SendKeys(Keys.ArrowDown);
            _chrome.FindElementById("SecurityAnswerOne").Click();
            _chrome.FindElementById("SecurityAnswerOne").SendKeys("yes");

            _chrome.FindElementById("SecurityQuestionTwo").Click();
            _chrome.FindElementById("SecurityQuestionTwo").SendKeys(Keys.ArrowDown);
            _chrome.FindElementById("SecurityAnswerTwo").Click();
            _chrome.FindElementById("SecurityAnswerTwo").SendKeys("yes");

            _chrome.FindElementByClassName("btn").Click();
            Assert.Contains("cannot be the same", _chrome.FindElementByClassName("field-validation-error").Text);

            _chrome.Dispose();
        }

        [Fact]
        public void AddSecurityQuestions()
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

            var answerOne = "answer is one";
            var answerTwo = "answer is two";
            //Fill out with different questions
            _chrome.FindElementById("SecurityQuestionOne").Click();
            _chrome.FindElementById("SecurityQuestionOne").SendKeys(Keys.ArrowDown);
            _chrome.FindElementById("SecurityQuestionOne").SendKeys(Keys.ArrowDown);
            _chrome.FindElementById("SecurityAnswerOne").Click();
            _chrome.FindElementById("SecurityAnswerOne").SendKeys(answerOne);

            _chrome.FindElementById("SecurityQuestionTwo").Click();
            _chrome.FindElementById("SecurityQuestionTwo").SendKeys(Keys.ArrowDown);
            _chrome.FindElementById("SecurityAnswerTwo").Click();
            _chrome.FindElementById("SecurityAnswerTwo").SendKeys(answerTwo);

            _chrome.FindElementByClassName("btn").Click();

            //back at profile
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.FindElementByLinkText("Add Security Questions").Click();
            Assert.Contains("Add Security Questions", _chrome.FindElementByClassName("form-signin-heading").Text);

            //answers are still there
            Assert.Contains(answerOne, _chrome.FindElementById("SecurityAnswerOne").GetAttribute("value"));
            Assert.Contains(answerTwo, _chrome.FindElementById("SecurityAnswerTwo").GetAttribute("value"));

            _chrome.Dispose();
        }



    }
 }
