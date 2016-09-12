using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VirtualPresta
{
    public class PrestaShopClient
    {
        public bool Scilence { get; set; }

        private IWebDriver webDriver = null;

        public PrestaShopClient(string loginAddress, string email, string password, bool scilence = true)
        {
            Scilence = scilence;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            var options = new ChromeOptions();
            if (Scilence)
            {
                service.HideCommandPromptWindow = true;
                options.AddArgument("--window-position=-32000,-32000");
            }
            webDriver = new ChromeDriver(service, options);

            webDriver.Url = loginAddress;
            webDriver.FindElement(By.CssSelector("#email")).SendKeys(email);
            webDriver.FindElement(By.Id("passwd")).SendKeys(password);
            webDriver.FindElement(By.CssSelector("button")).Click();
        }
    }
}
