using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Button : Control
    {
        /// <summary>
        /// Currently Identical to Control
        /// </summary>
        /// <remarks>Named different to make page objects easier to read and interact with</remarks>
        /// <param name="element"></param>
        public Button(ReadOnlyCollection<IWebElement> elements, int elementNumber = 0) : base(elements, elementNumber) { }

        //Should add in a method that allows the verification that the button worked and the page changed
    }
}
