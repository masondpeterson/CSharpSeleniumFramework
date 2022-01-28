using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class ElementList : Control
    {
        public ElementList(ReadOnlyCollection<IWebElement> elements, int elementNumber = 0) : base(elements, elementNumber) { }

        //Add methods that help handle lists <li> <ul> options more smoothly
    }
}
