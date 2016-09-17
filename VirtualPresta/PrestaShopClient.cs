using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CsvHelper;
using System.IO;

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
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            wait.Until(ExpectedConditions.UrlContains("AdminDashboard"));
        }

        public void Save(Product product)
        {
            goToProductForm();
            saveProduct(product);

            bool hasImage = false;
            if (product.ImageFiles != null)
            {
                if (product.ImageFiles.Count > 0)
                    hasImage = true;
            }
            if (product.File != null || hasImage)
            {

                saveFileAndImages(product);
            }
        }

        private void goToCSVImportForm()
        {
            while (!webDriver.Url.Contains("AdminImport"))
            {
                webDriver.Url = BaseAddress + "index.php?controller=AdminImport";
                System.Threading.Thread.Sleep(100);
            }
        }

        private void goToProductForm()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            while (!webDriver.Url.Contains("addproduct"))
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
            if (product.File != null && System.IO.File.Exists(product.File))
            {
                webDriver.FindElement(By.Id("virtual_product")).Click();
                webDriver.FindElement(By.Id("link-VirtualProduct")).Click();
                webDriver.FindElement(By.CssSelector("label[for=is_virtual_file_on]")).Click();
                webDriver.FindElement(By.Id("virtual_product_file_uploader")).SendKeys(product.File);
            }

            if(product.ImageFiles != null)
            {
                webDriver.FindElement(By.Id("link-Images")).Click();
                string files = "";
                foreach (string imageFile in product.ImageFiles)
                {
                    if (System.IO.File.Exists(imageFile))
                    {
                        if (files != "")
                        {
                            files += ";";
                        }
                        files += imageFile;
                    }
                }
                webDriver.FindElement(By.Id("file")).SendKeys(files);
                webDriver.FindElement(By.Id("file-upload-button")).Click();
                while (!webDriver.FindElement(By.Id("file-success")).Displayed)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                System.Threading.Thread.Sleep(1000);
            }

            webDriver.FindElement(By.Id("link-VirtualProduct")).Click();

            bool success = false;
            while (!success)
            {

                var sumbitbuttons = webDriver.FindElements(By.Name("submitAddproductAndStay"));
                foreach (IWebElement element in sumbitbuttons)
                {
                    try
                    {
                        element.Click();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            product.FileAndImagesSaved = true;
        }

        public static IEnumerable<Product> GetProductsFromCSV(string csvFile)
        {
            //CsvReader reader = new CsvReader(new System.IO.StreamReader(csvFile), new CsvHelper.Configuration.CsvConfiguration() { Delimiter = ";" });
            //while (reader.Read())
            //{
            //    Product product = new Product() { Id = Convert.ToInt32(reader.GetField("ID")), Name = reader.GetField("Name *") };
            //    if (reader.FieldHeaders.Contains("File"))
            //    {
            //        product.File = reader.GetField("File");
            //    }
            //    if (reader.FieldHeaders.Contains("ImageFiles"))
            //    {
            //        if (!string.IsNullOrEmpty(reader.GetField("ImageFiles")))
            //        {
            //            product.ImageFiles = new List<string>(reader.GetField("ImageFiles").Split(','));
            //        }
            //    }
            //    yield return product;

            //}
            //yield break;
            StreamReader reader = new StreamReader(csvFile);
            string header = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                yield return new Product() { Data = new CsvCollection() { Data = header + Environment.NewLine + reader.ReadLine() } };
            }
            yield break;
        }

        private string getAppliedName(string filename)
        {
             return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filename), System.IO.Path.GetFileNameWithoutExtension(filename) + "applied.csv");
        }

        public void ApplyCSV(string csvFile, IEnumerable<Product> products = null)
        {
            if (products != null)
            {
                CsvReader reader = new CsvReader(new System.IO.StreamReader(csvFile), new CsvHelper.Configuration.CsvConfiguration() { Delimiter = ";" });
                CsvWriter writer = new CsvWriter(new System.IO.StreamWriter(getAppliedName(csvFile)), new CsvHelper.Configuration.CsvConfiguration() { Delimiter = ";" });
                reader.ReadHeader();
                foreach (string header in reader.FieldHeaders)
                {
                    writer.WriteField(header);
                }
                writer.NextRecord();
                foreach (Product product in products)
                {
                    writer.WriteField(product.Id);
                    if (!reader.Read())
                    {
                        throw new Exception("No enough records in csv file.");
                    }
                    for (int i = 1; i < reader.FieldHeaders.Count(); i++)
                    {
                        writer.WriteField(reader.GetField(i));
                    }
                    writer.NextRecord();
                }
                if (reader.Read())
                    throw new Exception("No enough products.");
                writer.Dispose();
                reader.Dispose();
            }

            goToCSVImportForm();

            webDriver.FindElement(By.Id("entity")).SendKeys("محصول{ENTER}");
            webDriver.FindElement(By.Id("file")).SendKeys(products != null ? getAppliedName(csvFile) : csvFile);
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("csv_selected_filename")));
            bool success = false;
            while (!success)
            {
                try
                {
                    webDriver.FindElement(By.Id("submitImportFile")).Click();
                    success = true;
                }
                catch (Exception)
                {
                }
            }
            success = false;
            while (!success)
            {
                try
                {
                    webDriver.FindElement(By.Id("import")).Click();
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
