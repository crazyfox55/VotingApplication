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
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Profile")).Click();
            Assert.Contains("User Profile", _chrome.FindElement(By.Id("Profile")).Text);

            _chrome.FindElement(By.LinkText("Add Security Questions")).Click();
            Assert.Contains("Add Security Questions", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.FindElement(By.ClassName("btn")).Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Dispose();

        }
        [Fact]
        public void MustChooseDifferentCheck()
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

            //choose the same question
            _chrome.FindElement(By.Id("SecurityQuestionOne")).Click();
            _chrome.FindElement(By.Id("SecurityQuestionOne")).SendKeys(Keys.ArrowDown);
            _chrome.FindElement(By.Id("SecurityAnswerOne")).Click();
            _chrome.FindElement(By.Id("SecurityAnswerOne")).SendKeys("yes");

            _chrome.FindElement(By.Id("SecurityQuestionTwo")).Click();
            _chrome.FindElement(By.Id("SecurityQuestionTwo")).SendKeys(Keys.ArrowDown);
            _chrome.FindElement(By.Id("SecurityAnswerTwo")).Click();
            _chrome.FindElement(By.Id("SecurityAnswerTwo")).SendKeys("yes");

            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.Contains("cannot be the same", _chrome.FindElement(By.ClassName("field-validation-error")).Text);

            _chrome.Dispose();
        }

        [Fact]
        public void AddSecurityQuestions()
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

            var answerOne = "answer is one";
            var answerTwo = "answer is two";
            //Fill out with different questions
            _chrome.FindElement(By.Id("SecurityQuestionOne")).Click();
            _chrome.FindElement(By.Id("SecurityQuestionOne")).SendKeys(Keys.ArrowDown);
            _chrome.FindElement(By.Id("SecurityQuestionOne")).SendKeys(Keys.ArrowDown);
            _chrome.FindElement(By.Id("SecurityAnswerOne")).Click();
            _chrome.FindElement(By.Id("SecurityAnswerOne")).SendKeys(answerOne);

            _chrome.FindElement(By.Id("SecurityQuestionTwo")).Click();
            _chrome.FindElement(By.Id("SecurityQuestionTwo")).SendKeys(Keys.ArrowDown);
            _chrome.FindElement(By.Id("SecurityAnswerTwo")).Click();
            _chrome.FindElement(By.Id("SecurityAnswerTwo")).SendKeys(answerTwo);

            _chrome.FindElement(By.ClassName("btn")).Click();

            //back at profile
            Assert.Contains("User Profile", _chrome.FindElement(By.Id("Profile")).Text);

            _chrome.FindElement(By.LinkText("Add Security Questions")).Click();
            Assert.Contains("Add Security Questions", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            //answers are still there
            Assert.Contains(answerOne, _chrome.FindElement(By.Id("SecurityAnswerOne")).GetAttribute("value"));
            Assert.Contains(answerTwo, _chrome.FindElement(By.Id("SecurityAnswerTwo")).GetAttribute("value"));

            _chrome.Dispose();
        }



    }
 }
