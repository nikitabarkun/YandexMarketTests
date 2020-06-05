using System;
using System.Configuration;
using System.IO;
using System.Text.Json;

namespace YandexMarketTests
{
    public class TestConfiguration
    {
        public readonly string SiteName;
        public readonly string BrowserName;
        public readonly string OutputPath;

        public TestConfiguration()
        {
            ConfigurationSetting settings = JsonSerializer.Deserialize<ConfigurationSetting>(File.ReadAllText("../netcoreapp3.1/config.json"));

            SiteName = settings.SiteName;
            BrowserName = settings.BrowserName;
            OutputPath = settings.OutputPath;
        }

        private class ConfigurationSetting
        {
            public string SiteName { get; set; }
            public string BrowserName { get; set; }
            public string OutputPath { get; set; }
        }

    }
}