using System;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace YandexMarketTests.PageObjects
{
    public class MainPage
    {
        private IWebDriver driver;

        private string loginButtonSelector =
            "[href=\"https://passport.yandex.by/auth?origin=market_desktop_header&retpath=https://market.yandex.by/?lr=0&rtr=157\"]";

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickLoginButton()
        {
            IWebElement header = driver.FindElement(By.ClassName("header2-nav__user"));

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            wait.Until(header => header.
                FindElement(By.CssSelector(loginButtonSelector)));

            header.FindElement(By.CssSelector(loginButtonSelector)).Click();
        }
    }
}