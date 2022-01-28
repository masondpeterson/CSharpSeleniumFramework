using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace QaSeleniumCore.Toolbox
{
    public class CreateDriver
    {
        public IWebDriver Driver { get; set; }
        public bool CleanBrowser = false;
        private bool _headless;
        private ChromeOptions _chromeOptions = new ChromeOptions();

        /// <summary>
        /// Enum contains the available Driver Types that work in the framework
        /// </summary>
        public enum Name { Chrome, Firefox, Edge }

        /// <summary>
        /// Used by the Browser class to create a driver and launch browser
        /// </summary>
        /// <param name="browserName"></param>
        public CreateDriver(Name browserName, bool headless)
        {
            _headless = headless;
            switch (browserName)
            {
                case Name.Chrome:
                    Driver = ChromeDriver();
                    break;
                case Name.Firefox:
                    Driver = FirefoxDriver();
                    break;
                case Name.Edge:
                    Driver = EdgeDriver();
                    break;
                default:
                    Driver = ChromeDriver();
                    break;
            }
        }

        /// <summary>
        /// Used to create an instance of Chrome using default chrome options
        /// Headless is a variable set in the class constructor
        /// </summary>
        /// <returns></returns>
        protected ChromeDriver ChromeDriver()
        {
            if (_headless)
            {
                _chromeOptions.AddArgument("headless");
            }

            _chromeOptions.AddArguments("start-maximized","incognito","disable-infobars","version","disable-popup-blocking");
            var driver = new ChromeDriver(_chromeOptions);
            return driver;
        }

        /// <summary>
        /// Used to create a instance of Firefox
        /// </summary>
        /// <returns></returns>
        protected FirefoxDriver FirefoxDriver()
        {
            var driver = new FirefoxDriver();
            return driver;
        }
        /// <summary>
        /// Used to create an instance of edge
        /// </summary>
        /// <returns>Edge Driver</returns>
        protected EdgeDriver EdgeDriver()
        {
            var driver = new EdgeDriver();
            return driver;
        }
    }
}
