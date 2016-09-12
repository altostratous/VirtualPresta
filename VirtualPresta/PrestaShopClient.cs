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
            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
            wait.Until(ExpectedConditions.UrlContains("AdminDashboard"));
        }

        public void Save(Product product)
        {
            goToProductForm();
            saveProduct(product);

            if (product.File != null)
            {

                saveFileAndImages(product);
            }
        }

        private void goToProductForm()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            while (!webDriver.Url.Contains("AdminProducts"))
            {
                webDriver.Url = BaseAddress + "index.php?controller=AdminProducts&addproduct";
                System.Threading.Thread.Sleep(100);
            }
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.UrlContains("AdminProducts"));
            List<string> invalid_inputs = new List<string>();
            webDriver.FindElement(By.CssSelector("a.btn.btn-continue")).Click();
        }

        private void saveProduct(Product product)
        {
            webDriver.FindElement(By.Id("name_1")).SendKeys(product.Name);
            bool success = false;
            while (!success)
            {
                try
                {
                    webDriver.FindElement(By.Name("submitAddproductAndStay")).Click();
                    success = true;
                }
                catch (Exception)
                {
                }
            }
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
            wait.Until(ExpectedConditions.UrlContains("id_product"));
            product.Id = extractProductId(webDriver.Url);
            product.Saved = true;
        }

        private int extractProductId(string url)
        {
            string result = url.Substring(url.IndexOf("id_product=") + 11);
            result = result.Substring(0, result.IndexOf("&"));
            return Convert.ToInt32(result);
        }

        private void saveFileAndImages(Product product)
        {
            webDriver.FindElement(By.Id("virtual_product")).Click();
            webDriver.FindElement(By.Id("link-VirtualProduct")).Click();
            webDriver.FindElement(By.CssSelector("label[for=is_virtual_file_on]")).Click();
            webDriver.FindElement(By.Id("virtual_product_file_uploader")).SendKeys(product.File);

            bool success = false;
            while (!success)
            {
                try
                {
                    webDriver.FindElement(By.Name("submitAddproductAndStay")).Click();
                    success = true;
                }
                catch (Exception)
                {
                }
            }
        }

        public void Dispose()
        {
            webDriver.Quit();
        }
    }
}
