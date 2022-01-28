using NUnit.Framework;
using OpenQA.Selenium;
using QaSeleniumCore.BrowserHelp;
using QaSeleniumCore.PageElementControls;
using QaSeleniumCore.Toolbox;
using QaSeleniumCore.UnitTests.SupportClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace QaSeleniumCore.UnitTests
{
    [TestFixture]
    public class WebManagerTests
    {
        private IWebDriver _driver;
        private WebManager _manager = new WebManager();
        private TestPages _testPages;
        private BasePage _tools;
        private string _startUrl = "https://icsworkfront.my.workfront.com/";
        private string _loginUrl = "https://id.churchofjesuschrist.org/";
        private string _encyptedPass = "8Bcqa6AYpMSOGoYx9kZRqub+LHkYdIQkwWzoUi3eHdw=";

        #region Setup and Teardown

        [SetUp]
        public void Setup()
        {
            _driver = _manager.LaunchBrowser(CreateDriver.Name.Chrome, _startUrl);
            _testPages = new TestPages(_driver);
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
        }

        #endregion

        #region Tests

        [Test]
        public void DriverCreationTest()
        {
            Assert.IsNotNull(_driver);
        }
        
        [Test]
        public void LaunchBrowserNavigationTest()
        {
            _testPages.HeaderLogo.IsVisible();
            Assert.AreEqual(_loginUrl, _manager.GetCurrentUrl());
        }

        [Test]
        public void OktaSignInTest()
        {
            _testPages.HeaderLogo.IsVisible();
            Assert.IsTrue(_manager.OktaSignIn("ICSAutomation01", _encyptedPass));
        }

        [Test]
        public void GoToWebSiteTest()
        {
            _manager.GoToWebsite("https://www.google.com");
            Assert.AreEqual("https://www.google.com/", _manager.GetCurrentUrl());
        }

        #endregion

        
    }
}
