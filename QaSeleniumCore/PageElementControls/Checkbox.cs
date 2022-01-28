using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Checkbox : Control
    {
        public Checkbox(ReadOnlyCollection<IWebElement> elements, int elementNumber = 0) : base(elements, elementNumber) { }

        /// <summary>
        /// Checks CSS attribute on a checkbox to determine if it is checked or not
        /// </summary>
        /// <remarks>Warning: Does not work if the checkbox value is on a different or hidden element</remarks>
        /// <returns>true or false</returns>
        public bool IsChecked()
        {
            var check = Element.GetAttribute("checked");
            if (check == "checked")
            {
                return true;
            }
            else if (check == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the value on an element is true or not
        /// </summary>
        /// <remarks>Checks the value attribute for unconventional checkboxes</remarks>
        /// <returns>true or false</returns>
        public bool IsCheckedValue()
        {
            var check = Element.GetAttribute("value");
            return check == "true";
        }
    }
}
