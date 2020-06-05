using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace YandexMarketTests
{
    public static class WebDriverFactory
    {
        public static IWebDriver InitiateBrowser(string browserName)
        {
            switch (browserName)
            {
                case "chrome":
                    return new ChromeDriver();
                case "firefox":
                default:
                    return new FirefoxDriver();
            }
        }
    }
}
