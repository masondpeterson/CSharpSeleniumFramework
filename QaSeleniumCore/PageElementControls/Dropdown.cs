using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Dropdown : Control
    {
        public IList<IWebElement> ChildList;

        public Dropdown(ReadOnlyCollection<IWebElement> elements, int elementNumber = 0) : base(elements, elementNumber) { }

        #region List Type Dropdown Methods

        /// <summary>
        /// Clicks on the parent element to open a dropdown list
        /// Not all dropdown types require being open to find a child.
        /// </summary>
        public void Open()
        {
            Element.Click();
        }

        #endregion

        #region List Dropdown Methods

        /// <summary>
        /// Opens and gathers a list of li's and adds them to a elements list "ChildList"
        /// </summary>
        public void CreateChildList()
        {
            Open();
            ChildList = Element.FindElements(By.TagName("li"));
        }

        /// <summary>
        /// Opens dropdown and clicks on child li based on text provided
        /// </summary>
        /// <param name="childText"></param>
        public void ClickChildByText(string childText)
        {
            CreateChildList();
            foreach (var element in ChildList)
            {
                if (element.Text.ToLower() == childText.ToLower())
                {
                    element.Click();
                }
            }
        }

        /// <summary>
        /// Opens dropdwon list and returns string list of all children li's
        /// </summary>
        /// <returns>list of strings</returns>
        public List<string> GetChildNames()
        {
            CreateChildList();
            var childNames = new List<string>();
            foreach (var element in ChildList)
            {
                var text = element.Text;
                childNames.Add(text);
            }

            return childNames;
        }

        #endregion
    }
}
