# DataDrivenTest-using-CSV-CSharp-Dotnet-Core-VSCode
DataDrivenTest using CSV CSharp Dotnet Core VSCode
reference
https://joshclose.github.io/CsvHelper/examples/reading/reading-by-hand 

using CsvHelper; 

public class Foo
        {
            public string User { get; set; }
            public string Password { get; set; }
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
