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
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Basic Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
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
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Basic Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.FindElement(By.ClassName("btn")).Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Dispose();
        }

        [Fact]
        public void SSNFormat()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Basic Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            var invalidSSN = "12-12-123";
            _chrome.FindElement(By.Id("SSNumber")).SendKeys(invalidSSN);
            _chrome.FindElement(By.Id("Identification")).Click();
            Assert.Contains("provide a proper social security number", _chrome.FindElement(By.Id("SSNumber-error)")).Text);
            _chrome.Dispose();
        }

        //have to change user each run through
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
            _chrome.FindElement(By.LinkText("Basic Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            var firstName = "testFirst";
            var lastName = "testLast";
            var DOB = "11/11/2000";
            var DOBcheck = "2000-11-11"; //value reverses
            var ID = "12346224235";
            var SSN = "123-12-1234";
            _chrome.FindElement(By.Id("FirstName")).SendKeys(firstName);
            _chrome.FindElement(By.Id("LastName")).SendKeys(lastName);
            _chrome.FindElement(By.Id("DOB")).SendKeys(DOB);
            _chrome.FindElement(By.Id("Identification")).SendKeys(ID);
            _chrome.FindElement(By.Id("SSNumber")).SendKeys(SSN);
            _chrome.FindElement(By.ClassName("btn")).Click();
           
            //will now say Edit Registration instead of Basic Registration
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Registration")));

            //Info should be saved and can click submit without any errors
            _chrome.FindElement(By.LinkText("Edit Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            Assert.Contains(firstName, _chrome.FindElement(By.Id("FirstName")).GetAttribute("value"));
            Assert.Contains(lastName, _chrome.FindElement(By.Id("LastName")).GetAttribute("value"));
            Assert.Contains(DOBcheck, _chrome.FindElement(By.Id("DOB")).GetAttribute("value"));
            Assert.Contains(ID, _chrome.FindElement(By.Id("Identification")).GetAttribute("value"));
            Assert.Contains(SSN, _chrome.FindElement(By.Id("SSNumber")).GetAttribute("value"));

            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Registration")));

            //logout
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Logout");

            //log back in
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());

            //should still say edit registration
            _chrome.FindElement(By.LinkText("Edit Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);


            //should be saved and still be able to click submit without any errors
            Assert.Contains(firstName, _chrome.FindElement(By.Id("FirstName")).GetAttribute("value"));
            Assert.Contains(lastName, _chrome.FindElement(By.Id("LastName")).GetAttribute("value"));
            Assert.Contains(DOBcheck, _chrome.FindElement(By.Id("DOB")).GetAttribute("value"));
            Assert.Contains(ID, _chrome.FindElement(By.Id("Identification")).GetAttribute("value"));
            Assert.Contains(SSN, _chrome.FindElement(By.Id("SSNumber")).GetAttribute("value"));
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Registration")));

            _chrome.FindElement(By.LinkText("Edit Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            //able to edit fields
            _chrome.FindElement(By.Id("FirstName")).SendKeys(Keys.Control + "a");
            _chrome.FindElement(By.Id("FirstName")).SendKeys(lastName);
            _chrome.FindElement(By.Id("LastName")).SendKeys(Keys.Control + "a");
            _chrome.FindElement(By.Id("LastName")).SendKeys(firstName);
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Registration")));

            //FirstName and LastName should now be swapped
            _chrome.FindElement(By.LinkText("Edit Registration")).Click();
            Assert.Contains("Voter Registration", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            Assert.Contains(lastName, _chrome.FindElement(By.Id("FirstName")).GetAttribute("value"));
            Assert.Contains(firstName, _chrome.FindElement(By.Id("LastName")).GetAttribute("value"));
            Assert.Contains(DOBcheck, _chrome.FindElement(By.Id("DOB")).GetAttribute("value"));
            Assert.Contains(ID, _chrome.FindElement(By.Id("Identification")).GetAttribute("value"));
            Assert.Contains(SSN, _chrome.FindElement(By.Id("SSNumber")).GetAttribute("value"));

            _chrome.Dispose();
        }

    }
}
