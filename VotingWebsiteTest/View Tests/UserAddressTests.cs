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

        //ZLZJTCWVYN, DLVJEJZWCJ, UETJTLCKOS, RPSFWFCDDW
        //MUZDSQNSND, CCPEWXTIPI, PQZJMBWJXM, NRRONNDZTX, GCTZSBARUJ, ANSEGITLRC, ZZZNXOQZGM, XYYGBDSFVO, NPNUKNEDYP, KRHQHKZYRQ, 
        //OWHOWZYSSR, YBZIYLUGXG, RQZRJUYTVE, HCOOWWXDBT, FLMVXEPWRH, TQDLHCAFUK, CYIZRHJABE, BXTRSCERPY, BFNNSYVTTV, UKTXIPEOBN, QVBJZVBZQQ
        [Fact]
        public void NavigateToPage()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);
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
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.FindElementByClassName("btn").Click();

            //Error messages now are there
            Assert.Contains("required", _chrome.FindElementById(id).Text);
            _chrome.Dispose();
        }

        [Fact]
        public void StateFormat()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);

            var invalidState = "iz";
            _chrome.FindElementById("State").SendKeys(invalidState);
            _chrome.FindElementById("City").Click();
            Assert.Contains("Invalid State", _chrome.FindElementById("State-error").Text);
            _chrome.Dispose();
        }

        [Fact]
        public void ZipCodeFormat()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);

            //check that must be correct length
            var invalidZipcode = "1234123";
            _chrome.FindElementById("ZipCode").SendKeys(invalidZipcode);
            _chrome.FindElementById("City").Click();
            Assert.Contains("5 digits", _chrome.FindElementById("ZipCode-error").Text);

            //check that must be a valid zip code
            invalidZipcode = "12345";
            _chrome.FindElementById("ZipCode").SendKeys(Keys.Control + "a");
            _chrome.FindElementById("ZipCode").SendKeys(invalidZipcode);
            _chrome.FindElementByClassName("btn").Click();
            Assert.Contains("does not exist", _chrome.FindElementById("ZipCode-error").Text);
            _chrome.Dispose();
        }
        
        [Fact]
        public void FillOutForm()
        {
            var username = user;
            var password = "hello";
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/Login");
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());
            _chrome.FindElementByLinkText("Register Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);

            var AddressLineOne = "1234 test street";
            var City = "iowa city";
            var State = "IA";
            var ZipCode = "52240"; 

            _chrome.FindElementById("AddressLineOne").SendKeys(AddressLineOne);
            _chrome.FindElementById("City").SendKeys(City);
            _chrome.FindElementById("State").SendKeys(State);
            _chrome.FindElementById("ZipCode").SendKeys(ZipCode);
            _chrome.FindElementByClassName("btn").Click();
           
            //will now say Edit Registration instead of Basic Registration
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Address"));

            //Info should be saved and can click submit without any errors
            _chrome.FindElementByLinkText("Edit Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);
            Assert.Contains(AddressLineOne, _chrome.FindElementById("AddressLineOne").GetAttribute("value"));
            Assert.Contains(City, _chrome.FindElementById("City").GetAttribute("value"));
            Assert.Contains(State, _chrome.FindElementById("State").GetAttribute("value"));
            Assert.Contains(ZipCode, _chrome.FindElementById("ZipCode").GetAttribute("value"));

            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Address"));

            //logout
            _chrome.Navigate().GoToUrl("http://localhost:5000/Authentication/LogoutAsync");

            //log back in
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("Login").Click();
            Assert.Contains(username.ToLower(), _chrome.FindElementById("DashboardMsg").Text.ToLower());

            //should still say edit registration
            _chrome.FindElementByLinkText("Edit Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);


            //should be saved and still be able to click submit without any errors
            Assert.Contains(AddressLineOne, _chrome.FindElementById("AddressLineOne").GetAttribute("value"));
            Assert.Contains(City, _chrome.FindElementById("City").GetAttribute("value"));
            Assert.Contains(State, _chrome.FindElementById("State").GetAttribute("value"));
            Assert.Contains(ZipCode, _chrome.FindElementById("ZipCode").GetAttribute("value"));
            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Address"));

            _chrome.FindElementByLinkText("Edit Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);
            //able to edit fields
            _chrome.FindElementById("AddressLineOne").SendKeys(Keys.Control + "a");
            AddressLineOne = "12 other address lane";
            _chrome.FindElementById("AddressLineOne").SendKeys(AddressLineOne);
            _chrome.FindElementByClassName("btn").Click();
            Assert.NotNull(_chrome.FindElementByLinkText("Edit Address"));

            //FirstName and LastName should now be swapped
            _chrome.FindElementByLinkText("Edit Address").Click();
            Assert.Contains("Voter Address", _chrome.FindElementByClassName("form-signin-heading").Text);
            Assert.Contains(AddressLineOne, _chrome.FindElementById("AddressLineOne").GetAttribute("value"));
            Assert.Contains(City, _chrome.FindElementById("City").GetAttribute("value"));
            Assert.Contains(State, _chrome.FindElementById("State").GetAttribute("value"));
            Assert.Contains(ZipCode, _chrome.FindElementById("ZipCode").GetAttribute("value"));

            _chrome.Dispose();
        }
        
    }
}
