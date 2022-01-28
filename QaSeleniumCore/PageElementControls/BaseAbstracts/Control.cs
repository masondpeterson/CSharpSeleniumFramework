using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace QaSeleniumCore.PageElementControls.BaseAbstracts
{
    public abstract class Control : PageElement
    {
        protected Control(ReadOnlyCollection<IWebElement> webElements, int elementNumber) : base(webElements, elementNumber) { }

        /// <summary>
        /// Checks if element is enabled before clicking the element
        /// </summary>
        /// <returns>true if enabled otherwise false</returns>
        public bool Click()
        {
            var enabled = PageHelper.WaitUntilClickable(Element);
            try
            {
                if (enabled)
                {
                    Element.Click();
                }
                else
                {
                    Debug.WriteLine("Element is not clickable.");
                }
                
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(500);
                Element.Click();
            }
            
            return enabled;
        }
        /// <summary>
        /// Checks if element is clickable before double clicking it
        /// </summary>
        /// <returns>True if enabled otherwise false</returns>
        public bool DoubleClick()
        {
            var enabled = PageHelper.WaitUntilClickable(Element);
            if (enabled)
            {
                Actions action = new Actions(Driver);
                action.MoveToElement(Element).DoubleClick().Perform();
            }
            else
            {
                Debug.WriteLine("Element is not clickable");
            }
            return enabled;
        }

        /// <summary>
        /// Imitates the hitting of the desired key on the keyboard
        /// </summary>
        /// <remarks>Capitalize the desired key, default is Enter</remarks>
        /// <param name="keys"></param>
        public void Keyboard(string keys)
        {
            switch (keys)
            {
                case "Enter":
                    Element.SendKeys(Keys.Enter);
                    break;
                case "Down":
                    Element.SendKeys(Keys.Down);
                    break;
                case "Up":
                    Element.SendKeys(Keys.Up);
                    break;
                case "Space":
                    Element.SendKeys(Keys.Space);
                    break;
                default:
                    Element.SendKeys(Keys.Enter);
                    break;
            }
        }

        /// <summary>
        /// Validates that an element is enabled on the page. Waits for condition with each
        /// try lasting 250 milliseconds
        /// </summary>
        /// <returns>returns true if enabled otherwise false</returns>
        public bool ElementEnabled(int maxTries = 20)
        {
            var check = false;
            if (ListOfElements.Count > 0)
            {
                //Added in a custom wait since waituntilenabled is not an option in selenium
                int loopingCounter = 0;
                do
                {
                    check = Element.Enabled;
                    if (check)
                    {
                        return check;
                    }
                    else
                    {
                        Thread.Sleep(250);
                        loopingCounter++;
                    }
                } while (check == false && loopingCounter < maxTries);
            }

            return check;
        }
    }
}
