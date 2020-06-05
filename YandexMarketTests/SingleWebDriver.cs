using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace YandexMarketTests
{
    public class SingleWebDriver
    {
        private static IWebDriver instance;

        public static IWebDriver GetInstance(TestConfiguration configuration)
        {
            if (instance == null)
            {
                instance = WebDriverFactory.InitiateBrowser(configuration.BrowserName);
            }
            return instance;
        }
    }
}