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

        string user = "TQDLHCAFUK"; //set to one of script users

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
            _chrome.FindElementByLinkText("Profile").Click();
            Assert.Contains("User Profile", _chrome.FindElementById("Profile").Text);

            _chrome.FindElementByLinkText("Add Security Questions").Click();
            Assert.Contains("Add Security Questions", _chrome.FindElementByClassName("form-signin-heading").Text);

            _chrome.Close();
        }


    }
 }
