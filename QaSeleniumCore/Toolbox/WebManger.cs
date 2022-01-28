using OpenQA.Selenium;
using System.Collections.Generic;

namespace QaSeleniumCore.Toolbox
{
    public class WebManager
    {
        public IWebDriver Driver;
        public List<IWebDriver> DriversList = new List<IWebDriver>();

        public string GetTitle => Driver.Title;

        #region Launch Browser Methods

        /// <summary>
        /// Creates webDriver, launches browser, navigates to URL
        /// </summary>
        /// <remarks>Uses the driver create class to create the driver which is where the Enum for driver name lives</remarks>
        /// <param name="driverName"></param>
        /// <param name="url"></param>
        /// <returns>IWebDriver</returns>
        public IWebDriver LaunchBrowser(CreateDriver.Name driverName, string url, bool headless = false)
        {
            //Create Driver and open browser (Set Driver for tests)
            Driver = new CreateDriver(driverName, headless).Driver;
            DriversList.Add(Driver);

            //Go to specified url
            GoToWebsite(url);

            //return the driver that was created
            return Driver;
        }

        /// <summary>
        /// Creates a Driver and also logs in to the wam Login page. Must pass in an encrypted password
        /// </summary>
        /// <param name="driverName"></param>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns>The Driver for your tests to use</returns>
        public IWebDriver LaunchBrowserAndWamLogin(CreateDriver.Name driverName, string url, string username,
            string encryptedPassword, bool headless = false)
        {
            //Create Driver and open browser (Set Driver for tests)
            Driver = new CreateDriver(driverName, headless).Driver;
            DriversList.Add(Driver);

            //Go to specified url
            GoToWebsite(url);

            //Login to WAM (without 2 step authentication)
            var wamLogin = new SingleSignOn(Driver);
            wamLogin.SignIn(username, encryptedPassword);

            //return the driver that was created
            return Driver;
        }

        /// <summary>
        /// Creates a Driver and launches the specified browser but does not navigate to a URL.
        /// </summary>
        /// <param name="driverName"></param>
        /// <returns>IWebDriver</returns>
        public IWebDriver CreateDriver(CreateDriver.Name driverName, bool headless = false)
        {
            //Create a one off driver
            Driver = new CreateDriver(driverName, headless).Driver;
            DriversList.Add(Driver);
            return Driver;
        }

        #endregion

        #region Methods for use after Driver is created

        /// <summary>
        /// Uses a previously created Driver to navigate to a specified URL
        /// </summary>
        /// <param name="url"></param>
        public void GoToWebsite(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Using the active webDriver it gets the current URL
        /// </summary>
        /// <returns>URL as a string</returns>
        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        /// <summary>
        /// Simply refreshes the current active page in the browser.
        /// </summary>
        public void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        /// <summary>
        /// Accepts a basic alert popup (not a multiple choice alert)
        /// </summary>
        public void AcceptAlert()
        {
            Driver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Takes a username and encrypted password and Logs in to the Wam login page.
        /// Does not yet handle 2 step authentication.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns>True if Login was successful and false if it fails</returns>
        public bool SingleSignIn(string username, string encryptedPassword)
        {
            var signIn = new SingleSignOn(Driver);
            return signIn.SignIn(username, encryptedPassword);
        }

        /// <summary>
        /// Takes a username and encrypted password then navigates the double login page.
        /// Does not yet handle 2 step authentication.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns>True if the password error message does not appear</returns>
        public bool OktaSignIn(string username, string encryptedPassword)
        {
            var signIn = new SingleSignOn(Driver);
            return signIn.OktaSignIn(username, encryptedPassword);
        }

        #endregion

        #region Scroll Methods

        /// <summary>
        /// Scrolls the browser to the bottom of the page
        /// </summary>
        public void ScrollToBottomOfPage()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        /// <summary>
        /// Scrolls the browser to the top of the page
        /// </summary>
        public void ScrollToTopOfPage()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");
        }

        public void ScrollToElement(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(" + element.Location.X + "," + element.Location.Y + ")");
            }
            catch
            {
                throw new ElementNotVisibleException();
            }
        }

        /// <summary>
        /// Scrolls so a specific element is at the top of the page if possible. 
        /// </summary>
        /// <remarks>Warning: May cause element to be hidden still if there is a floating header</remarks>
        /// <param name="by"></param>
        public void ScrollToElementBy(By by)
        {
            try
            {
                var element = Driver.FindElement(by);
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(" + element.Location.X + "," + element.Location.Y + ")");
            }
            catch
            {
                throw new ElementNotVisibleException();
            }
        }

        #endregion

        #region Encryption Method

        public string EncryptPassword(string password)
        {
            var encrypt = new Simple3Des();
            return encrypt.EncryptData(password);
        }

        #endregion
    }
}
