using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls;
using QaSeleniumCore.Toolbox;
using System;
using System.Collections.Generic;
using System.Text;

namespace QaSeleniumCore.UnitTests.SupportClasses
{
    public class TestPages : BasePage
    {
        //Test Account Information 
        //Username: ICSAutomation01 
        //Password: 5@6puH4FiZ68
        //Encrypted Password:
        //Recovery Email: masondpeterson@gmail.com
        //Name: Tester ICSTesting

        public TestPages(IWebDriver webDriver) : base(webDriver) { }

        public Link HeaderLogo => new Link(FindElement(By.Id("headerLogo")));
    }
}
