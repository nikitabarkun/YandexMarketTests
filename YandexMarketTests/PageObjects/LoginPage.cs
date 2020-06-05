using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace YandexMarketTests.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        private WebDriverWait wait;

        private string loginSelector = "passp-field-login";
        private string passwordSelector = "passp-field-passwd";
        private string phoneSkipSelector = "[data-t=\"phone_skip\"]";

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterLogin(string login)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            
            wait.Until(driver => driver.FindElement(By.Id(loginSelector)));

            IWebElement loginField = driver.FindElement(By.Id(loginSelector));

            loginField.SendKeys(login);
        }

        public void EnterPassword(string password)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            wait.Until(driver => driver.FindElement(By.Id(passwordSelector)).Enabled);

            IWebElement passwordField = driver.FindElement(By.Id(passwordSelector));

            passwordField.SendKeys(password);
        }

        public void PressLoginButton()
        {
            IWebElement buttonContainer = driver.FindElement(By.CssSelector("[class=\"passp-button passp-sign-in-button\"]"));

            buttonContainer.FindElement(By.CssSelector("[type=submit]")).Click();
        }

        public void PressPhoneSkipButton()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            if (driver.WindowHandles.Count != 1)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

                wait.Until(driver => driver.FindElement(By.CssSelector(phoneSkipSelector)));

                IWebElement phoneSkipContainer = driver.FindElement(By.CssSelector(phoneSkipSelector));
                phoneSkipContainer.FindElement(By.CssSelector("[type=button]")).Click();
            }
        }
    }
}