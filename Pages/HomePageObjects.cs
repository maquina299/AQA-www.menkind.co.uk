﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace www.menkind.co.uk.Pages
{
    public class HomePageObject : BasePage
    {
        // CHANGED: Use the overloaded BasePage constructor that accepts an existing driver.
        public HomePageObject(IWebDriver driver) : base() { }

        private static By LogoSelector => By.CssSelector("a.header__logo");
        private static By SignInLink => By.CssSelector("a.header__sign-in");
        private static By LoginEmailField => By.Id("login_email");
        private static By LoginPassField => By.Id("login_pass");
        private static By SignInButton => By.CssSelector("input[type='submit'][value='Sign In']");
        private static By AccountLink => By.CssSelector("a.header__sign-in[href='/account.php']");

        public void EnterLoginEmail(string loginEmail)
            => WaitForElementToBeVisible(LoginEmailField).SendKeys(loginEmail);

        public void EnterLoginPass(string pass)
            => WaitForElementToBeVisible(LoginPassField).SendKeys(pass);

        public bool IsLogoDisplayed()
        {
            // CHANGED: Use the public Driver property instead of the private _driver.
            return Driver.FindElement(LogoSelector).Displayed;
        }

        public bool IsLogoLoaded()
        {
            if (Driver == null)
            {
                Logger.Error("WebDriver is not initialized.");
                return false;
            }

            try
            {
                IWebElement logo = WaitForElementToBeVisible(LogoSelector);

                string script = @"
                    var img = arguments[0].querySelector('img');
                    return img && img.complete && 
                           typeof img.naturalWidth != 'undefined' && 
                           img.naturalWidth > 0;
                ";
                bool isLoaded = (bool)((IJavaScriptExecutor)Driver).ExecuteScript(script, logo);

                if (!isLoaded)
                {
                    Logger.Warn("Logo image is not fully loaded");
                    TakeScreenshot("logo_is_not_loaded");
                }

                return isLoaded;
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Error("Timeout occurred while waiting for logo element");
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error occurred while checking if logo is loaded");
                return false;
            }
        }

        public string GetTitle()
        {
            // CHANGED: Use Driver property.
            return Driver.Title;
        }

        public bool IsUserLoggedIn()
        {
            try
            {
                return WaitForElementToBeVisible(AccountLink).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void SignIn()
        {
            WaitForElementToBeClickable(SignInLink).Click();
        }

        public void Submit()
        {
            WaitForElementToBeVisible(SignInButton).Click();
        }
    }
}
