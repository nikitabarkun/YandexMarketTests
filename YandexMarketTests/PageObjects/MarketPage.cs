using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace YandexMarketTests.PageObjects
{
    public class MarketPage
    {
        private IWebDriver driver;

        private WebDriverWait wait;

        private string menuSelector = "[data-zone-name=\"menu\"]";
        private string categoriesSelector = "[class=\"_381y5orjSo _21NjfY1k45\"] [data-zone-name=\"category-link\"] [class=\"_35SYuInI1T _1vnugfYUli\"] a span";
        private string userPicSelector = "[class=\"header2-nav__user\"]";
        private string logoutSelector = "[class=\"link user user__logout i-bem user_js_inited\"]";

        public MarketPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string ClickRandomPopularCategory()
        {
            List<string> popularCategories = new List<string>();

            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

            wait.Until(driver => driver.FindElement(By.CssSelector(menuSelector)));

            IWebElement menu = driver.FindElement(By.CssSelector(menuSelector));
            IReadOnlyCollection<IWebElement> menuCategories = menu.FindElements(By.CssSelector(categoriesSelector));

            Random randomIndex = new Random();
            List<IWebElement> menuCategoriesRandomized = menuCategories.OrderBy(x => randomIndex.Next()).ToList();

            foreach (var category in menuCategoriesRandomized)
            {
                if ((category.Enabled) && (category.Displayed))
                {
                    string categoryName = category.FindElement(By.XPath("./..")).Text;
                    category.FindElement(By.XPath("./..")).Click();
                    return categoryName;
                }
            }
            return string.Empty;
        }

        public List<string> GetPopularCategories()
        {
            List<string> popularCategories = new List<string>();

            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

            wait.Until(driver => driver.FindElement(By.CssSelector(menuSelector)));

            IWebElement menu = driver.FindElement(By.CssSelector(menuSelector));
            IReadOnlyCollection<IWebElement> menuCategories = menu.FindElements(By.CssSelector(categoriesSelector));

            foreach (var category in menuCategories)
            {
                string text = category.Text;
                popularCategories.Add(text);
            }

            return popularCategories;
        }

        public void ClickAllCategories()
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
           
            wait.Until(driver => driver.FindElement(By.Id("27903767-tab")));

            driver.FindElement(By.Id("27903767-tab")).Click();
        }

        public void WriteAllCategories(TestConfiguration configuration)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IReadOnlyCollection<IWebElement> categories = driver.
                FindElements(By.CssSelector("[data-zone-name=\"category-link\"] [role=tab] a span"));

            using (StreamWriter writer = new StreamWriter(configuration.OutputPath, false, Encoding.UTF8))
            {
                foreach (var category in categories)
                {
                    writer.WriteLine(category.GetAttribute("text"));
                }
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public string GetChoosenCategoryName()
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

            wait.Until(driver => driver.FindElement(By.CssSelector("h1")));

            IWebElement categoryTitle = driver.FindElement(By.CssSelector("h1"));
            string categoryTitleText = categoryTitle.GetAttribute("innerText");

            return categoryTitleText;
        }

        public void Logout()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.CssSelector(userPicSelector)).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.CssSelector(logoutSelector)).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public bool IsLoggedOut()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            if (driver.FindElement(By.CssSelector("[class=\"header2-user user i-bem user_js_inited\"]")).GetAttribute("textContent") == "Войти")
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                return true;
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            return false;
        }
    }
}