using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserChangePasswordTests
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
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Profile").Click();
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.Close();
        }
        [Theory]
        [InlineData("OldPassword-error")]
        [InlineData("Password-error")]
        [InlineData("ConfirmPassword-error")]
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

            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Dispose();

        }

        [Fact]
        public void InvalidPasswordCheck()
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

            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);

            //send the wrong password
            _chrome.FindElementById("OldPassword").SendKeys(password + password);
            _chrome.FindElementById("Password").SendKeys("123456");
            _chrome.FindElementById("ConfirmPassword").SendKeys("123456");
            _chrome.FindElementByClassName("btn").Click();
            Assert.Contains("Invalid password.", _chrome.FindElementByClassName("validation-summary-errors").Text);
            _chrome.Dispose();
        }

        [Fact]
        public void MatchingNewPasswordCheck()
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

            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);

            //send non matching passwords
            _chrome.FindElementById("OldPassword").SendKeys(password);
            _chrome.FindElementById("Password").SendKeys("123456");
            _chrome.FindElementById("ConfirmPassword").SendKeys("654321");
            _chrome.FindElementByClassName("btn").Click();
            Assert.Contains("do not match.", _chrome.FindElementById("ConfirmPassword-error").Text);
            _chrome.Dispose();
        }

        
        [Fact]
        public void ChangePassword()
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

            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);

            //change password
            var newPass = "123456";
            _chrome.FindElementById("OldPassword").SendKeys(password);
            _chrome.FindElementById("Password").SendKeys(newPass);
            _chrome.FindElementById("ConfirmPassword").SendKeys(newPass);
            _chrome.FindElementByClassName("btn").Click();

            //back to profile page
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);
            _chrome.FindElementByLinkText("Logout").Click();

            //at login page
            Assert.Contains("User Login", _chrome.FindElementByClassName("form-signin-heading").Text);

            //can't log in with old pass
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains("Invalid username or password.", _chrome.FindElementByClassName("validation-summary-errors").Text);

            //can log in with new pass
            _chrome.FindElementById("Password").SendKeys(newPass);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());

            //change password back to old password so can re run the test
            _chrome.FindElementByLinkText("Profile").Click();
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);
            _chrome.FindElementByLinkText("Change Password").Click();
            Assert.Contains("Change Password", _chrome.FindElementByClassName("form-signin-heading").Text);
            _chrome.FindElementById("OldPassword").SendKeys(newPass);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementByClassName("btn").Click();

            //back to profile page
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.Dispose();

        }
        

    }
 }
