using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace QaSeleniumCore.PageElementControls
{
    public class DropdownSelect : Dropdown
    {
        public DropdownSelect(ReadOnlyCollection<IWebElement> elements, int elementNumber = 0) : base(elements, elementNumber) { }

        /// <summary>
        /// Selects child from Select type Dropdown based on value
        /// </summary>
        /// <param name="childValue"></param>
        public void SelectChildByValue(string childValue)
        {
            var mySelect = new SelectElement(Element);
            mySelect.SelectByValue(childValue);
        }

        /// <summary>
        /// Selects child from Select type Dropdown based on name
        /// </summary>
        /// <param name="childName"></param>
        public void SelectChildByName(string childName)
        {
            var mySelect = new SelectElement(Element);
            mySelect.SelectByText(childName);
        }

        /// <summary>
        /// Finds the currently selected value in a Select Dropdown
        /// </summary>
        /// <param name="dropdownSelect"></param>
        /// <returns>string, text of selected option</returns>
        public string FindDropdownSelection()
        {
            return Element.FindElements(By.TagName("option")).Where(e => e.GetAttribute("selected") != null)
                .First().Text;
        }
    }
}
