using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace VirtualPresta
{
    public class PrestaShopClient : IDisposable
    {

        private IWebDriver webDriver = null;

        private string token;

        public string BaseAddress { get; set; }

        public PrestaShopClient(string baseAddress, string email, string password, bool scilence = true)
        {
            this.BaseAddress = baseAddress;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            var options = new ChromeOptions();
            if (scilence)
            {
                service.HideCommandPromptWindow = true;
                options.AddArgument("--window-position=-32000,-32000");
            }
            webDriver = new ChromeDriver(service, options);

            webDriver.Url = baseAddress;
            webDriver.FindElement(By.CssSelector("#email")).SendKeys(email);
            webDriver.FindElement(By.Id("passwd")).SendKeys(password);
            webDriver.FindElement(By.CssSelector("button")).Click();
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            //WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
            //wait.Until(ExpectedConditions.UrlContains("token"));
            //token = webDriver.Url.Substring(webDriver.Url.IndexOf("token") + 5);
        }

        public Dictionary<string, string> GetProductForm()
        {

            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
            wait.Until(ExpectedConditions.UrlContains("AdminDashboard"));
            string addProductLink = "";
            Dictionary<string, string> result = new Dictionary<string, string>();
            var links = webDriver.FindElements(By.TagName("a"));
            foreach(IWebElement element in links)
            {
                Console.WriteLine(element.GetAttribute("href"));
                try
                {
                    if (element.GetAttribute("href").StartsWith("index.php?controller=AdminProducts&token="))
                    {
                        addProductLink = element.GetAttribute("href") + "&addproduct";
                        break;
                    }
                }
                catch (NullReferenceException) { }
            }
            webDriver.Url = addProductLink;

            return result;
        }

        public void Dispose()
        {
            webDriver.Quit();
        }
    }
}
