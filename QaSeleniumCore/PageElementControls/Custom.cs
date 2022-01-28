using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Custom : Control
    {
        public Custom(ReadOnlyCollection<IWebElement> webElements, int elementNumber = 0) : base(webElements, elementNumber) { }
    }
}
