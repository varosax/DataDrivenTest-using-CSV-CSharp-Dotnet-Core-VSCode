using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using WDSE.Decorators;
using WDSE;
using WDSE.ScreenshotMaker;
using System.Text;
using CsvHelper;
using System.Data;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace dotnetcoreDataDriven
{
    public class Tests
    {
        /*public Screenshot TakeScreenshot()
        {
            try
            {
                var screenshotDriver = (ITakesScreenshot)this.driver;
                var screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile("C:/Users/alvar/Documents/dotnetcoreDataDriven/Test.png", ScreenshotImageFormat.Png);
                Console.WriteLine(screenshot);
                return screenshot;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Test failed but was unable to get webdriver screenshot.");
            }
            catch (UnhandledAlertException)
            {
                Console.WriteLine("Test failed but was unable to get webdriver screenshot.");
            }

            return null;
        }*/
        public class Foo
        {
            public string User { get; set; }
            public string Password { get; set; }
        }
        public void TakeScreenshot()
        {
            try
            {
                Random random = new Random();
                var testnum = random.Next(1, 1000);
                //Take the screenshot
                var screenshotDriver = (ITakesScreenshot)this.driver;
                var screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile("C:/Users/alvar/Documents/dotnetcoreDataDriven/Test" + testnum + ".png", ScreenshotImageFormat.Png);
                Console.WriteLine(screenshot);
                Console.WriteLine("passed");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail("Failed with Exception: " + e);
            }
        }
        public IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        }

        [Test]

        public void Test1()
        {
            //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C:\\Users\\alvar\\Documents\\dotnetcoreDataDriven\\Test.png",ScreenshotImageFormat.Png);
            using (var reader = new StreamReader("C:/Users/alvar/Documents/dotnetcoreDataDriven/data.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Foo>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    String user = csv.GetField("User");
                    String pass = csv.GetField("Password");
                    driver.FindElement(By.Id("user-name")).SendKeys(user);
                    driver.FindElement(By.Id("password")).SendKeys(pass);
                    driver.FindElement(By.XPath("//input[@value='LOGIN']")).Click();
                    TakeScreenshot();
                    if(driver.Url.Contains("https://www.saucedemo.com/inventory.html")){
                        TakeScreenshot();
                    }else{
                        Console.WriteLine(user+" "+pass);
                    }
                    driver.Navigate().GoToUrl("https://www.saucedemo.com/");
                }

            }
        }

        [TearDown]
        public void Dispose()
        {
            driver.Quit();
        }
    }
}