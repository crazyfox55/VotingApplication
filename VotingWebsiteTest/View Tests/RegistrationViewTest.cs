using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using VotingApplication;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace VotingWebsiteTest
{
    [TestClass]
    public class RegistrationViewTest
    {
        ChromeDriver _chrome = new ChromeDriver(@"C:\Users\colemtg\source\repos");

        

        private TestServer _server;
        private HttpClient _client;

        [TestMethod]
        public void Register()
        {

            var builder = new WebHostBuilder()
            .UseContentRoot(@"C:\Users\colemtg\source\repos\VotingApplication\VotingApplication")
            .UseEnvironment("Development")
            .UseStartup<Startup>()
            .UseApplicationInsights();

            _server = new TestServer(builder);
            _client = _server.CreateClient();
            var response = _client.GetAsync("/");
            Assert.IsNotNull(response);


            // var username = string.Join("", System.Guid.NewGuid().ToString().Take(5));
            //var password = string.Join("", System.Guid.NewGuid().ToString().Take(6));
            //var email = "cole-pierce@uiowa.edu";
            //CreateUser(username, email, password);

        }

        private void CreateUser(string username, string email, string password)
        {
            _chrome.Navigate().GoToUrl("http://localhost:5000/UserRegistration/Register");
            //_chrome.FindElementById("registerLink").Click();
            _chrome.FindElementById("Username").SendKeys(username);
            _chrome.FindElementById("Email").SendKeys(email);
            _chrome.FindElementById("Password").SendKeys(password);
            _chrome.FindElementById("ConfirmPassword").SendKeys(password);
            _chrome.FindElementById("RegistrationButton").Click();

            var userWasCreated =
                _chrome.FindElementById("Email").Text.Contains(email);
            Assert.IsTrue(userWasCreated);

        }

    }
}
