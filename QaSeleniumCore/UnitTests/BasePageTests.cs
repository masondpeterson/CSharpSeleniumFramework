using NUnit.Framework;
using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls;
using QaSeleniumCore.Toolbox;
using System;
using System.Collections.Generic;
using System.Text;

namespace QaSeleniumCore.UnitTests
{
    [TestFixture]
    public class BasePageTests
    {
        private IWebDriver _driver;
        private WebManager _manager = new WebManager();
        private BasePage _tools;
        private string _startUrl = "https://www.churchofjesuschrist.org/?lang=eng";

        #region Setup and Teardown

        [SetUp]
        public void Setup()
        {
            _driver = _manager.LaunchBrowser(CreateDriver.Name.Chrome, _startUrl);
            _tools = new BasePage(_driver);
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
        }

        #endregion

        #region Tests

        [Test]
        public void FindElementTest()
        {
            var accountClass = AccountMenu.GetAttribute("Class");
            Assert.AreEqual("topItem accountLink", accountClass);
        }

        [Test]
        public void ElementClickableTest()
        {
            _manager.GoToWebsite("https://www.google.com/");
            var element = _tools.FindElement(By.ClassName("lnXdpd"),BasePage.ElementState.Clickable);
            Assert.IsTrue(element != null);
        }

        #endregion

        #region PageControls

        public Button AccountMenu => new Button(_tools.FindElement(By.XPath("//*[@class='topItem accountLink']")));

        #endregion
    }
}
