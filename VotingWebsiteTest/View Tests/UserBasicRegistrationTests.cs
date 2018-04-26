using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserBasicRegistrationTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "CCPEWXTIPI"; //set to one of script users
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
            _chrome.FindElementByLinkText("Basic Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("FirstName-error")]
        [InlineData("LastName-error")]
        [InlineData("Identification-error")]
        [InlineData("SSNumber-error")]
        public void ErrorMessages(string id)
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Basic Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Dispose();
        }

        [Fact]
        public void SSNFormat()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Basic Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);

            var invalidSSN = "12-12-123";
            _chrome.FindElementById("SSNumber").SendKeys(invalidSSN);
            _chrome.FindElementById("Identification").Click();
            Assert.Contains("provide a proper social security number", _chrome.FindElementById("SSNumber-error").Text);
            _chrome.Dispose();
        }

        //have to change user each run through
        [Fact]
        public void FillOutForm()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Basic Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);

            var firstName = "testFirst";
            var lastName = "testLast";
            var DOB = "11/11/2000";
            var DOBcheck = "2000-11-11"; //value reverses
            var ID = "12346224235";
            var SSN = "123-12-1234";
            _chrome.FindElementById("FirstName").SendKeys(firstName);
            _chrome.FindElementById("LastName").SendKeys(lastName);
            _chrome.FindElementById("DOB").SendKeys(DOB);
            _chrome.FindElementById("Identification").SendKeys(ID);
            _chrome.FindElementById("SSNumber").SendKeys(SSN);
            _chrome.FindElementByClassName("btn").Click();
           
            //will now say Edit Registration instead of Basic Registration
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Registration"));

            //Info should be saved and can click submit without any errors
            _chrome.FindElementByLinkText("Edit Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);
            Assert.Contains(firstName, _chrome.FindElementById("FirstName").GetAttribute("value"));
            Assert.Contains(lastName, _chrome.FindElementById("LastName").GetAttribute("value"));
            Assert.Contains(DOBcheck, _chrome.FindElementById("DOB").GetAttribute("value"));
            Assert.Contains(ID, _chrome.FindElementById("Identification").GetAttribute("value"));
            Assert.Contains(SSN, _chrome.FindElementById("SSNumber").GetAttribute("value"));

            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Registration"));

            //logout
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/LogoutAsync");

            //log back in
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());

            //should still say edit registration
            _chrome.FindElementByLinkText("Edit Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);


            //should be saved and still be able to click submit without any errors
            Assert.Contains(firstName, _chrome.FindElementById("FirstName").GetAttribute("value"));
            Assert.Contains(lastName, _chrome.FindElementById("LastName").GetAttribute("value"));
            Assert.Contains(DOBcheck, _chrome.FindElementById("DOB").GetAttribute("value"));
            Assert.Contains(ID, _chrome.FindElementById("Identification").GetAttribute("value"));
            Assert.Contains(SSN, _chrome.FindElementById("SSNumber").GetAttribute("value"));
            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Registration"));

            _chrome.FindElementByLinkText("Edit Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);
            //able to edit fields
            _chrome.FindElementById("FirstName").SendKeys(Keys.Control + "a");
            _chrome.FindElementById("FirstName").SendKeys(lastName);
            _chrome.FindElementById("LastName").SendKeys(Keys.Control + "a");
            _chrome.FindElementById("LastName").SendKeys(firstName);
            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Registration"));

            //FirstName and LastName should now be swapped
            _chrome.FindElementByLinkText("Edit Registration").Click();
            Assert.Contains("Voter Registration", _chrome.FindElementByClassName("form-signin-heading").Text);
            Assert.Contains(lastName, _chrome.FindElementById("FirstName").GetAttribute("value"));
            Assert.Contains(firstName, _chrome.FindElementById("LastName").GetAttribute("value"));
            Assert.Contains(DOBcheck, _chrome.FindElementById("DOB").GetAttribute("value"));
            Assert.Contains(ID, _chrome.FindElementById("Identification").GetAttribute("value"));
            Assert.Contains(SSN, _chrome.FindElementById("SSNumber").GetAttribute("value"));

            _chrome.Dispose();
        }

    }
}
