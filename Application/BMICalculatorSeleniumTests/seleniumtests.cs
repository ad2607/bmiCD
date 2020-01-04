using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace BMICalculatorSeleniumTests
{
    [TestClass]
    public class seleniumtests
    {
        private static TestContext testContext;
        private RemoteWebDriver driver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            seleniumtests.testContext = testContext;
        }

        [TestInitialize]
        public void TestInit()
        {
            driver = GetChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
        }

        [TestCleanup]
        public void TestClean()
        {
            driver.Quit();
        }

        [TestMethod]
        public void SampleFunctionalTest1()
        {
            var webAppUrl = "";
            try
            {
                webAppUrl = testContext.Properties["webAppUrl"].ToString();
            }
            catch (Exception)
            {
                webAppUrl = "http://localhost:50433/";
            }
            //var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimestamp = startTimestamp + 60 * 10;

            //Arragne
            string inputValueStones = "13";
            string inputValuePounds = "4";
            string inputValueFeet = "6";
            string inputValueInches = "3";
            string expectedResult = "Your BMI is 23.25";
            //Acts
            driver.Navigate().GoToUrl("webAppUrl");
            driver.Manage().Window.Size = new System.Drawing.Size(974, 1040);
            driver.FindElement(By.Id("BMI_WeightStones")).SendKeys("13");
            driver.FindElement(By.Id("BMI_WeightPounds")).SendKeys("4");
            driver.FindElement(By.Id("BMI_HeightFeet")).SendKeys("6");
            driver.FindElement(By.Id("BMI_HeightInches")).SendKeys("3");
            driver.FindElement(By.CssSelector(".btn")).Click();
            string actualValue = driver.FindElement(By.Id("BMIValue")).Text;

            //Assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        private RemoteWebDriver GetChromeDriver()
        {
            var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }
    }
}