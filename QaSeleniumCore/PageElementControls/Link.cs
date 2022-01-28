using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Link : Control
    {
        public Link(ReadOnlyCollection<IWebElement> webElements, int elementNumber = 0) : base(webElements, elementNumber) { }

        /// <summary>
        /// Method returns a string of the href attribute of an element
        /// </summary>
        /// <returns>A string of the href attribute of the link</returns>
        public string GetLinkAddress()
        {
            return GetAttribute("href");
        }
    }
}
