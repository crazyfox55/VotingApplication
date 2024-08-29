using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using Xunit;

namespace VotingWebsiteTest.View_Tests
{
    public class UserAddressTests
    {
        //This should work for relative source, if not change to where the chromedriver is
        ChromeDriver _chrome = new ChromeDriver((Directory.GetParent(Directory.GetCurrentDirectory())).Parent.Parent.FullName);

        string user = "OWHOWZYSSR"; //set to one of script users
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
            _chrome.FindElement(By.LinkText("Register Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            _chrome.Close();
        }

        [Theory]
        [InlineData("AddressLineOne-error")]
        [InlineData("City-error")]
        [InlineData("State-error")]
        [InlineData("ZipCode-error")]
        public void ErrorMessages(string id)
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Register Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            _chrome.FindElement(By.ClassName("btn")).Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElement(By.Id(id)).Text);
            _chrome.Dispose();
        }

        [Fact]
        public void StateFormat()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Register Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            var invalidState = "iz";
            _chrome.FindElement(By.Id("State")).SendKeys(invalidState);
            _chrome.FindElement(By.Id("City")).Click();
            Assert.Contains("Invalid State", _chrome.FindElement(By.Id("State-error")).Text);
            _chrome.Dispose();
        }

        [Fact]
        public void ZipCodeFormat()
        {
            var username = user;
            var password = pass;
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());
            _chrome.FindElement(By.LinkText("Register Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            //check that must be correct length
            var invalidZipcode = "1234123";
            _chrome.FindElement(By.Id("ZipCode")).SendKeys(invalidZipcode);
            _chrome.FindElement(By.Id("City")).Click();
            Assert.Contains("5 digits", _chrome.FindElement(By.Id("ZipCode-error")).Text);

            //check that must be a valid zip code
            invalidZipcode = "12345";
            _chrome.FindElement(By.Id("ZipCode")).SendKeys(Keys.Control + "a");
            _chrome.FindElement(By.Id("ZipCode")).SendKeys(invalidZipcode);
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.Contains("does not exist", _chrome.FindElement(By.Id("ZipCode-error")).Text);
            _chrome.Dispose();
        }
        
        //have to change user every run through
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
            _chrome.FindElement(By.LinkText("Register Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);

            var AddressLineOne = "1234 test street";
            var City = "iowa city";
            var State = "IA";
            var ZipCode = "52240"; 

            _chrome.FindElement(By.Id("AddressLineOne")).SendKeys(AddressLineOne);
            _chrome.FindElement(By.Id("City")).SendKeys(City);
            _chrome.FindElement(By.Id("State")).SendKeys(State);
            _chrome.FindElement(By.Id("ZipCode")).SendKeys(ZipCode);
            _chrome.FindElement(By.ClassName("btn")).Click();
           
            //will now say Edit Registration instead of Basic Registration
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Address")));

            //Info should be saved and can click submit without any errors
            _chrome.FindElement(By.LinkText("Edit Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            Assert.Contains(AddressLineOne, _chrome.FindElement(By.Id("AddressLineOne")).GetAttribute("value"));
            Assert.Contains(City, _chrome.FindElement(By.Id("City")).GetAttribute("value"));
            Assert.Contains(State, _chrome.FindElement(By.Id("State")).GetAttribute("value"));
            Assert.Contains(ZipCode, _chrome.FindElement(By.Id("ZipCode")).GetAttribute("value"));

            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Address")));

            //logout
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Logout");

            //log back in
            _chrome.FindElement(By.Id("Username")).SendKeys(username);
            _chrome.FindElement(By.Id("Password")).SendKeys(password);
            _chrome.FindElement(By.Id("Login")).Click();
            Assert.Contains(username.ToLower(), _chrome.FindElement(By.Id("DashboardMsg")).Text.ToLower());

            //should still say edit registration
            _chrome.FindElement(By.LinkText("Edit Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);


            //should be saved and still be able to click submit without any errors
            Assert.Contains(AddressLineOne, _chrome.FindElement(By.Id("AddressLineOne")).GetAttribute("value"));
            Assert.Contains(City, _chrome.FindElement(By.Id("City")).GetAttribute("value"));
            Assert.Contains(State, _chrome.FindElement(By.Id("State")).GetAttribute("value"));
            Assert.Contains(ZipCode, _chrome.FindElement(By.Id("ZipCode")).GetAttribute("value"));
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Address")));

            _chrome.FindElement(By.LinkText("Edit Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            //able to edit fields
            _chrome.FindElement(By.Id("AddressLineOne")).SendKeys(Keys.Control + "a");
            AddressLineOne = "12 other address lane";
            _chrome.FindElement(By.Id("AddressLineOne")).SendKeys(AddressLineOne);
            _chrome.FindElement(By.ClassName("btn")).Click();
            Assert.NotNull(_chrome.FindElement(By.LinkText("Edit Address")));

            //FirstName and LastName should now be swapped
            _chrome.FindElement(By.LinkText("Edit Address")).Click();
            Assert.Contains("Voter Address", _chrome.FindElement(By.ClassName("form-signin-heading")).Text);
            Assert.Contains(AddressLineOne, _chrome.FindElement(By.Id("AddressLineOne")).GetAttribute("value"));
            Assert.Contains(City, _chrome.FindElement(By.Id("City")).GetAttribute("value"));
            Assert.Contains(State, _chrome.FindElement(By.Id("State")).GetAttribute("value"));
            Assert.Contains(ZipCode, _chrome.FindElement(By.Id("ZipCode")).GetAttribute("value"));

            _chrome.Dispose();
        }
        
    }
}
