using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;


namespace ConsoleApp1
{

    class Tasks : JIRA
    {
        public  Tasks( string docType)
            : base( docType)
        {

            DocType = docType;

        }
       
        public string DocType { get; set; }

        public List<string> getTasks(IWebDriver driver)
        {

            //used to store string URLS
            List<string> webPagesURLS = new List<string>();

            return getURL(driver);

        }

    }

    //------------------------

    class Tests : JIRA
    {
        public Tests(string docType)
            :base( docType)
        {

            DocType = docType;

        }

        public string DocType { get; set; }

        public List<string> getTests(IWebDriver driver)
        {
         
            //Get all references to HTTP:// or Https://

            return getURL(driver);

        }

    }

    public class JIRA 
    {

        public JIRA( string docType)
        {

            DocType = docType;
            
        }

        public string DocType { get; set; }

        public string tester = "";

       

        // collection to temp hold all hrefs
        public ICollection<IWebElement> webPagesReferences = new List<IWebElement>();

        
        //used to store string URLS
        List<string> webPagesURLS = new List<string>();
            
        public List<string> getURL(IWebDriver driver)
        {

        //Get all references to HTTP:// or Https://
        webPagesReferences = driver.FindElements(By.TagName("a"));

            // Using the references get the URL attributes
            foreach (IWebElement pageReference in webPagesReferences)
        {
            try
            {
                tester = pageReference.GetAttribute("href").ToString();

                if (tester.ToUpper().Contains(DocType))
                {

                    if (!(tester.Contains("?")))
                    {

                        webPagesURLS.Add(pageReference.GetAttribute("href").ToString());

                    }

                }
            }
            catch (Exception)
            {

                Console.WriteLine("GetAttribute returned null");

                continue;

            }

            }
            return webPagesURLS;
        }
    }
}
