using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Image : Control
    {
        public Image(ReadOnlyCollection<IWebElement> webElements, int elementNumber = 0) : base(webElements, elementNumber) { }

        //place holder for potential methods that are specific to dealing with images on a page
    }
}
