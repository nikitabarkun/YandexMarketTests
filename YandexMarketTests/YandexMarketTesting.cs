using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using YandexMarketTests.PageObjects;

namespace YandexMarketTests
{
    [TestFixture]
    public class YandexMarketTests
    {
        [Test]
        public void YandexMarketTesting()
        {
            TestConfiguration configuration = new TestConfiguration();

            IWebDriver driver = SingleWebDriver.GetInstance(configuration);

            driver.Navigate().GoToUrl(configuration.SiteName);

            driver.Manage().Window.Maximize();

            MainPage mainPage = new MainPage(driver);
            Assert.AreEqual("Яндекс.Маркет — выбор и покупка товаров из проверенных интернет-магазинов", driver.Title, "Ошибка открытия главной страницы.");
            mainPage.ClickLoginButton();

            if (driver.WindowHandles.Count != 1)
            {
                driver.SwitchTo().Window(driver.WindowHandles[1]);
            }

            LoginPage loginPage = new LoginPage(driver);
            loginPage.EnterLogin("test.user.a1qa");
            loginPage.PressLoginButton();
            loginPage.EnterPassword("testPWD");
            loginPage.PressLoginButton();

            driver.SwitchTo().Window(driver.WindowHandles[0]);

            MarketPage marketPage = new MarketPage(driver);
            List<string> popularCategories = marketPage.GetPopularCategories();
            string randomCategoryName = marketPage.ClickRandomPopularCategory();

            string randomCategoryNameLong;
            GetShortAndLongCategoriesNames().TryGetValue(randomCategoryName, out randomCategoryNameLong);

            Assert.AreEqual(marketPage.GetChoosenCategoryName(), randomCategoryNameLong, "Несовпадение названий категорий.");

            marketPage.ClickAllCategories();
            marketPage.WriteAllCategories(configuration);

            List<string> categoriesFromCsvFile = ReadCategoriesFile(configuration.OutputPath);
            Assert.IsTrue(categoriesFromCsvFile.Intersect(popularCategories).Any(),
                "Список всех категорий из файла не содержит в себе полный список популярных категорий");

            marketPage.Logout();

            if (driver.WindowHandles.Count != 1)
            {
                driver.SwitchTo().Window(driver.WindowHandles[1]);
            }

            driver.SwitchTo().Window(driver.WindowHandles[0]);

            Assert.IsTrue(marketPage.IsLoggedOut(), "Ошибка выхода из аккаунта.");

            driver.Quit();
        }

        public List<string> ReadCategoriesFile(string path)
        {
            List<string> categories = new List<string>();
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                categories.Add(reader.ReadLine());
            }
            return categories;
        }

        public Dictionary<string, string> GetShortAndLongCategoriesNames()
        {
            Dictionary<string, string> shortAndLongCategoriesNames = new Dictionary<string, string>();

            shortAndLongCategoriesNames.Add("Электроника", "Электроника");
            shortAndLongCategoriesNames.Add("Бытовая техника", "Бытовая техника");
            shortAndLongCategoriesNames.Add("Ремонт", "Строительство и ремонт");
            shortAndLongCategoriesNames.Add("Компьютеры", "Компьютерная техника");
            shortAndLongCategoriesNames.Add("Детям", "Детские товары");
            shortAndLongCategoriesNames.Add("Авто", "Товары для авто- и мототехники");
            shortAndLongCategoriesNames.Add("Дом", "Товары для дома");
            shortAndLongCategoriesNames.Add("Спорт", "Спорт и отдых");
            shortAndLongCategoriesNames.Add("Красота", "Товары для красоты");
            shortAndLongCategoriesNames.Add("Здоровье", "Здоровье");
            shortAndLongCategoriesNames.Add("Зоотовары", "Товары для животных");
            shortAndLongCategoriesNames.Add("Продукты", "Продукты");
            shortAndLongCategoriesNames.Add("Гардероб", "Одежда и обувь");
            shortAndLongCategoriesNames.Add("Дача", "Дача, сад и огород");
            shortAndLongCategoriesNames.Add("Оборудование", "Оборудование");
            shortAndLongCategoriesNames.Add("Досуг", "Досуг и развлечения");
            shortAndLongCategoriesNames.Add("Уцененные товары", "Уцененные товары");
            shortAndLongCategoriesNames.Add("Скидки и акции", "Скидки и акции");

            return shortAndLongCategoriesNames;
        }
    }
}