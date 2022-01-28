using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QaSeleniumCore.PageElementControls
{
    public class Table : Control
    {
        public Table(ReadOnlyCollection<IWebElement> webElements, int elementNumber = 0) : base(webElements, elementNumber) { }

        public IList<IWebElement> GetRowList()
        {
            return Element.FindElements(By.TagName("tr"));
        }

        public void LoadTable(string repeater = "none")
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(repeater))
            {
                message = $"The load table method requires a repeater value other than null, empty or whitespace.";
            }

            if (repeater.Equals("none"))
            {
                //do some work to load a table by tr and td
                var table = Driver.FindElements(By.TagName("tbody"));

                //private Table Table => new Table(FindElements(By.repeater("applicant in manageInProcessList.InProcess.Applicants | orderBy:sortType:sortReverse")));
            }
            else
            {
                // do some work to load the table by ng-repeat
            }
        }
        //Add methods that make it easier to work with tables.
    }
}
