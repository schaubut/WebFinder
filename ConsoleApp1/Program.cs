using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp1
{

    public static class SeleniumExtensions

    {

        public static void WaitForNavigation(this IWebDriver driver)
        {

            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(60));

            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {

                Console.WriteLine("ERROR a keyword must be passed in as arg0");

                return;

            }
            else
            {
                string keyWord = args[0];
            }


            //partial string for searching for a Zehpr test
            const string ZehprStr = "ZTM-";

            //partial string for searching for a JIRA Task or BUG
            const string TaskStr = "CP-";  

            //used to store string URLS
            List<string> taskURLs = new List<string>();

            //used to store string URLS
            List<string> ztmURLS = new List<string>();

            //Create the reference for our browser
            IWebDriver driver = new ChromeDriver();

            LogIt log = new LogIt("C:\\Users\\jons\\documents\\Webcrawler.txt");  

            Tasks tasks = new Tasks( TaskStr);

            Tests ZTMs = new Tests( ZehprStr);

            string startPage = "https://bugs.casenetllc.com:9093/browse/CP-149391/";

            //Navigate to google page
            driver.Navigate().GoToUrl(startPage);

            Thread.Sleep(10000);

            log.WriteNewLog("Navigate to start page");

            var loginBox = driver.FindElement(By.Id("login-form-username"));
            loginBox.SendKeys("jschaubhut");

            var pwBox = driver.FindElement(By.Id("login-form-password"));
            pwBox.SendKeys("U$$RangerCV61");

            var logInBtn = driver.FindElement(By.Id("login-form-submit"));
            logInBtn.Click();

            log.AppendLog("Login: successful");


            log.AppendLog("Before loop URL = " + driver.Url);

            taskURLs = tasks.getURL(driver);

   
            //Go to each URL by looping the found URLS and navigate to the page and look for the word argument supplied by the user.
            foreach (string pageURL in taskURLs)
            {

                try
                {

                    driver.Navigate().GoToUrl(pageURL);

                    driver.WaitForNavigation();

                }

                catch (Exception)
                {
                    log.AppendLog(("StaleElementReferenceException occurred "));
                   
                }

                log.AppendLog("Current URL = " + driver.Url);

                if (driver.PageSource.Contains("Correspondence"))
                {

                    log.AppendLog("Correspondence found on this page");

                    ztmURLS = ZTMs.getURL(driver);
                    
                    foreach (string ZTMpageURL in ztmURLS)
                    {

                        log.AppendLog(ZTMpageURL);

                    }

                }

                try
                {

                    driver.Navigate().Back();

                    driver.WaitForNavigation();

                }
                catch (Exception )
                {

                    log.AppendLog(("StaleElementReferenceException occurred "));
                    
                }
      
                Thread.Sleep(5000);

            }

            log.AppendLog(("End:  foreach (string pageURL in webPagesURLS) "));
        }
    }
}
