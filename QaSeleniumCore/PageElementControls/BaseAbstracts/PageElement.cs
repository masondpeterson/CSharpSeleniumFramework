using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using QaSeleniumCore.Toolbox;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls.BaseAbstracts
{
    public abstract class PageElement
    {
        public IWebElement Element;
        public ReadOnlyCollection<IWebElement> ListOfElements;
        protected IWebDriver Driver => ((IWrapsDriver)Element).WrappedDriver;
        protected readonly PageObjectTools PageHelper;

        protected PageElement(ReadOnlyCollection<IWebElement> webElements, int elementNumber)
        {
            if (webElements.Count > 0)
            {
                Element = webElements[elementNumber];
                ListOfElements = webElements;
                PageHelper = new PageObjectTools(Driver);
            }
            else
            {
                ListOfElements = webElements;
            }
        }

        /// <summary>
        /// Validates if an element is displayed on the page.
        /// </summary>
        /// <remarks>Visible to a computer may be different than visible to a user</remarks>
        /// <returns>true if visible otherwise false</returns>
        public bool IsVisible()
        {
            bool visible = false;
            if (ListOfElements.Count > 0)
            {
                visible = Element.Displayed;
                return visible;
            }
            else
            {
                return visible;
            }
        }

        /// <summary>
        /// Returns the value of a given CSS attribute for specified element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns>Value as a string</returns>
        public string GetAttribute(string attributeName)
        {
            var response = Element.GetAttribute(attributeName);
            return response;
        }

        /// <summary>
        /// Gets the text of an element (not user input text)
        /// </summary>
        /// <returns>Text value as a string</returns>
        public string Text()
        {
            return Element.Text;
        }
  
    }
}
