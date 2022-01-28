using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls.BaseAbstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace QaSeleniumCore.PageElementControls
{
    public class Textbox : Control
    {
        public Textbox(ReadOnlyCollection<IWebElement> elements, int elementNumber = 0) : base(elements, elementNumber) { }

        /// <summary>
        /// Click on, clears, and enters desired text into a textbox element
        /// Optional param to press enter after sending text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enter"></param>
        public void SendText(string text, bool pressEnterAfter = false)
        {
            Click();
            ClearText();
            Element.SendKeys(text);
            if (pressEnterAfter)
            {
                Element.SendKeys(Keys.Enter);
            }
        }

        /// <summary>
        /// Clicks on, clears, and enters the text one at a time slowing the process to ensure all characters are entered
        /// </summary>
        /// <param name="textToType"></param>
        /// <param name="pressEnterAfter"></param>
        public void SetTextOneByOne(string textToType, bool pressEnterAfter = false)
        {
            Click();
            ClearText();
            string currentInputExpected = string.Empty;
            foreach (var currentChar in textToType.ToCharArray())
            {
                currentInputExpected += currentChar;
                Thread.Sleep(30);
                Element.SendKeys(currentChar.ToString());
            }

            if (pressEnterAfter)
            {
                Thread.Sleep(30);
                Element.SendKeys(Keys.Enter);
            }
        }

        /// <summary>
        /// Enters text and then validates that all text was entered correctly
        /// </summary>
        /// <param name="text"></param>
        /// <returns>true or false</returns>
        public bool ValidateEntry(string text)
        {
            SendText(text);
            var response = GetText();
            return response == text;
        }

        /// <summary>
        /// Pulls the text out of an input field using the Value attribute
        /// </summary>
        /// <returns>value as a string</returns>
        public string GetText()
        {
            var response = Element.GetAttribute("value");
            return response;
        }

        /// <summary>
        /// Clears the text out of an input field leaving it blank
        /// </summary>
        /// <remarks>If the reference is stale and gets an exception it will try again.</remarks>
        public void ClearText()
        {
            try
            {
                Element.Clear();
            }
            catch (StaleElementReferenceException)
            {
                ClearText(); //Element stale try again
            }
        }
    }
}
