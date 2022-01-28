using OpenQA.Selenium;
using QaSeleniumCore.PageElementControls;

namespace QaSeleniumCore.Toolbox
{
    public class SingleSignOn : PageObjectTools
    {
        private Simple3Des _encryption = new Simple3Des();

        public SingleSignOn(IWebDriver webDriver) : base(webDriver) { }

        #region Page Object Methods

        //WAM Login
        public Textbox UsernameInput => new Textbox(FindElement(By.Id("username")));
        public Textbox PasswordInput => new Textbox(FindElement(By.Id("password")));
        public Button SubmitButton => new Button(FindElement(By.Id("sign-in")));
        public Button CreateAccountButton => new Button(FindElement(By.XPath("//button[@class='sc-1o0i5ti-0 iHzWky sc-7932sm-0 gLHLOb']")));
        public Basic FailedLoginMessage => new Basic(FindElement(By.XPath("//div[@xpath=1]")));


        //OKTA Login Pages
        public Link HeaderLogo => new Link(FindElement(By.Id("headerLogo")));
        public Textbox UsernameOkta => new Textbox(FindElement(By.Id("okta-signin-username")));
        public Textbox PasswordOkta => new Textbox(FindElement(By.Id("input9")));
        public Checkbox RememberMe => new Checkbox(FindElement(By.Id("input6")));
        public Button NextButton => new Button(FindElement(By.Id("okta-signin-submit")));
        public Button VerifyButton => new Button(FindElement(By.XPath("//body/div[@id='okta-login-container']/div[@id='okta-sign-in']/div[2]/div[1]/div[1]/form[1]/div[2]/input[1]")));
        public Link SignUpLink => new Link(FindElement(By.XPath("//a[contains(Text(), 'Sign up')]")));
        public Basic PasswordErrorMessage => new Basic(FindElement(By.XPath("//p[contains(text(),'Password is incorrect')]")));
        #endregion

        #region Action Methods

        /// <summary>
        /// Send username and password encrypted with Simple3Des navigate to the Wam Login page first.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="encryptedPassword"></param>
        public bool SignIn(string username, string encryptedPassword)
        {
            //Send username and password and click submit
            UsernameInput.SendText(username);
            PasswordInput.SendText(_encryption.DecryptData(encryptedPassword));
            SubmitButton.Click();

            //Ensure the login was successful or failed
            var getErrorMsg = new Custom(FindElement(By.Id("scriptValidationErrorMsg"), ElementState.Visible, 0, 1)).ListOfElements.Count;
            if (getErrorMsg != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OktaSignIn(string username, string encryptedPassword)
        {
            //Enter Username and click next
            UsernameOkta.SendText(username);
            NextButton.Click();

            //Enter Password and click Verify
            PasswordOkta.SendText(_encryption.DecryptData(encryptedPassword));
            VerifyButton.Click();

            //Ensure Password error message does not appear
            var checkError = PasswordErrorMessage.ListOfElements.Count;
            if (checkError == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string EncryptString(string startText)
        {
            var encryption = new Simple3Des();
            return encryption.EncryptData(startText);
        }

        #endregion
    }
}
