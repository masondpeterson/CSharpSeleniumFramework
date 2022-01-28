using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace QaSeleniumCore.Toolbox
{
    public class PageObjectTools
    {
        protected IWebDriver Driver;
        private const int _maxWaitSec = 15;
        private int _fluentWaitInterval;

        public PageObjectTools(IWebDriver webDriver, int fluentWaitInterval = 250)
        {
            Driver = webDriver;
            _fluentWaitInterval = fluentWaitInterval;
        }

        public string Title => Driver.Title;

        public enum ElementState { Invisible, Visible, Clickable }

        #region Find Methods

        /// <summary>
        /// Find 0,1,or more elements which can used used by element or elementList from the control/Page element classes.
        /// Allows to select desired state to find element, 
        /// a stall in milliseconds before searching, 
        /// and a timeout adjustment as optional params
        /// </summary>
        /// <param name="by">Required</param>
        /// <param name="elementState">Optional</param>
        /// <param name="stall">Optional</param>
        /// <param name="timeoutInSeconds">Optional</param>
        /// <returns>Collection of IWebElements</returns>
        public ReadOnlyCollection<IWebElement> FindElement(By by, ElementState elementState = ElementState.Visible, int stall = 0, int timeoutInSeconds = _maxWaitSec)
        {
            ReadOnlyCollection<IWebElement> result;

            Stall(stall);

            try
            {
                switch (elementState)
                {
                    case ElementState.Clickable:
                        WaitUntilClickable(by, timeoutInSeconds);
                        break;
                    case ElementState.Invisible:
                        WaitUntilNotVisible(by, timeoutInSeconds);
                        break;
                    case ElementState.Visible:
                        WaitUntilVisible(by, timeoutInSeconds);
                        break;
                    default:
                        WaitUntilVisible(by, timeoutInSeconds);
                        break;
                }
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine("{0} Exception caught.  Since this is looking for a collection and a collection may have a count of 0, execution will be allowed to continue.", e);
            }
            finally
            {
                result = Driver.FindElements(by);
            }

            return result;
        }

        #endregion

        #region Switch Methods

        /// <summary>
        /// Switches to the specified iFrame using its Name
        /// </summary>
        /// <param name="frameName"></param>
        public void SwitchFrame(string frameName)
        {
            Driver.SwitchTo().Frame(frameName);
        }

        /// <summary>
        /// Switches to the specified iFrame using its Number
        /// </summary>
        /// <param name="frameNumber"></param>
        public void SwitchFrame(int frameNumber)
        {
            try
            {
                Driver.SwitchTo().Frame(frameNumber);
            }
            catch (NoSuchFrameException)
            {
                frameNumber++;
                SwitchFrame(frameNumber);
            }
        }

        /// <summary>
        /// Switches to the default iFrame for the page
        /// </summary>
        public void SwitchFrameDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Switches to the last window in the broser then to the default iFrame in that window
        /// </summary>
        public void SwitchToLastWindow()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Driver.SwitchTo().DefaultContent();
        }

        #endregion

        #region Wait Methods

        /// <summary>
        /// Takes a URL to ensure the browser navigated to the right destination. 
        /// Takes an element to help determine if the page has loaded.
        /// </summary>
        /// <param name="url">The full URL for the page you wish to display</param>
        /// <param name="by">Must be an identifier that is unique to the page</param>
        /// <returns>True if you are on the correct page otherwise false.</returns>
        protected bool IsAt(string url, IWebElement element)
        {
            WaitUntilUrlContains(url);
            return element.Displayed;
        }

        private bool WaitUntilUrlContains(string url)
        {
            return Wait(_maxWaitSec).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(url));
        }

        /// <summary>
        /// Explicit wait until the specified IWebElement is clickable before intaracting
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns>true if element is visible otherwise false</returns>
        public bool WaitUntilClickable(IWebElement element, int timeout = _maxWaitSec)
        {
            var newElement = Wait(timeout).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            return newElement.Enabled;
        }

        /// <summary>
        /// Explicit wait until the specified by element is clickable before interacting. 
        /// Also checks if the element exists first and adjusts the timeout so the full timeout is met no matter what
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns>true if element is clickable otherwise false</returns>
        protected bool WaitUntilClickable(By by, int timeout = _maxWaitSec)
        {
            var timeout2 = CheckElementExists(by, timeout);

            var newElement = Wait(timeout2).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            return newElement.Enabled;
        }

        /// <summary>
        /// Explicit wait until the specified by element is visible before interacting
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns>true if element is displayed otherwise false</returns>
        protected bool WaitUntilVisible(By by, int timeout = _maxWaitSec)
        {
            var element = Wait(timeout).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            return element.Displayed;
        }

        /// <summary>
        /// Explicit wait until the specified element is not displayed on the page but exists
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns>true if element not displayed otherwise false</returns>
        protected bool WaitUntilNotVisible(By by, int timeout = _maxWaitSec)
        {
            var element = Wait(timeout).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
            return element;
        }

        /// <summary>
        /// Creates a WebDriverWaitObject with a specified timeout
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>WebDriverWait</returns>
        protected WebDriverWait Wait(int seconds = _maxWaitSec)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Implicit Wait on the browser for a specified number of milliseconds
        /// </summary>
        /// <remarks>Should only be used if absolutely necessary</remarks>
        /// <param name="milliseconds"></param>
        protected void Stall(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public bool AngularLoaded()
        {
            bool angularLoaded = false;
            string angularReadyScript = "return angular.element(document).injector().get('$http').pendingRequests.length === 0";
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string angularReadyString = (string)js.ExecuteScript(angularReadyScript);
            Debug.WriteLine("angularReadyString:");
            Debug.WriteLine(angularReadyString);

            return angularLoaded; //Need to still add a path for when this method is successful.

            //Wait for Angular to load
            //ExpectedConditions<Boolean> angularLoad = driver => Boolean.valueO
        }

        private int CheckElementExists(By by, int timeout)
        {
            var check = false;
            int counter = 0;
            double timeout2 = timeout;
            do
            {
                try
                {
                    Driver.FindElement(by);
                    check = true;
                }
                catch (NoSuchElementException)
                {
                    Stall(_fluentWaitInterval);
                    timeout2 = timeout2 - (_fluentWaitInterval*.001);
                    counter++;
                }
            } while (check && counter < (timeout * 4));

            return Convert.ToInt32(timeout2);
        }

        #endregion

    }
}
