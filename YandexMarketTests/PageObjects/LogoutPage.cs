using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace YandexMarketTests.PageObjects
{
    public class LogoutPage
    {
        private IWebDriver driver;
        public LogoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Logout()
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            wait.Until(driver => driver.FindElement(By.CssSelector("[class=\"passp-account-list-item_block\"]")));

            IWebElement logoutContainer = driver.FindElement(By.CssSelector("[class=\"passp-account-list-item_block\"]"));

            Actions action = new Actions(driver);
            action.MoveToElement(logoutContainer).Perform();
            
            driver.FindElement(By.CssSelector("div [class=\"passp-account-list-item_block\"] [class=\"control link link_theme_normal passp-account-list-item__remove-button\"]")).Click();
        }

        public string GetLogoutVerification()
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            wait.Until(driver => driver.FindElement(By.CssSelector("h3")));

            return driver.FindElement(By.CssSelector("h3")).Text;
        }
    }
}
