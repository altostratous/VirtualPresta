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
    public class PrestaShopClient : IDisposable, ILogger
    {
        ILogger parentLogger = null;
        public void log(string tolog)
        {
            if (parentLogger != null)
                parentLogger.log(tolog);
        }
        private IWebDriver webDriver = null;

        public string BaseAddress { get; set; }

        public PrestaShopClient(string baseAddress, string email, string password, bool scilence = true, ILogger logger = null)
        {
            this.parentLogger = logger;
            this.BaseAddress = baseAddress;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            logger.log("created chrome service. ");
            var options = new ChromeOptions();
            if (scilence)
            {
                service.HideCommandPromptWindow = true;
                options.AddArgument("--window-position=-32000,-32000");
            }
            options.AddUserProfilePreference("download.prompt_for_download", "false");

            options.AddUserProfilePreference("download.default_directory", Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
            webDriver = new ChromeDriver(service, options);
            logger.log("created Chrome driver.");
            webDriver.Url = baseAddress;
            logger.log("set url for login. ");
            webDriver.FindElement(By.CssSelector("#email")).SendKeys(email);
            webDriver.FindElement(By.Id("passwd")).SendKeys(password);
            webDriver.FindElement(By.CssSelector("button")).Click();
            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            wait.Until(ExpectedConditions.UrlContains("AdminDashboard"));
        }

        public void Save(Product product)
        {
            log("going to new product page");
            goToProductForm();
            log("in the new product page");
            saveProduct(product);
            log("created the product online");
            bool hasImage = false;
            if (product.ImageFiles != null)
            {
                if (product.ImageFiles.Count > 0)
                    hasImage = true;
            }
            if (product.File != null || hasImage)
            {
                log("starting to save images and files");
                saveFileAndImages(product);
                log("saved images and files");
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
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
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

                System.Threading.Thread.Sleep(2000);
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

        public static void SaveCSVFromProducts(List<Product> products, string v)
        {
            StreamWriter streamWriter = new StreamWriter(v);
            CsvWriter writer = new CsvWriter(streamWriter, new CsvHelper.Configuration.CsvConfiguration() { Delimiter = ";" });
            if (products.Count > 0)
            {
                Product first = products.First();
                foreach (string header in first.Data.Keys)
                {
                    writer.WriteField(header);
                }
                foreach (Product product in products)
                {
                    writer.NextRecord();
                    foreach (string key in product.Data.Keys)
                    {
                        writer.WriteField(product.Data[key]);
                    }
                }
            }
            writer.Dispose();
        }

        internal void DownloadProduct(Product product, string v)
        {
            forceGo(string.Format("index.php?controller=AdminProducts&id_product={0}&updateproduct", product.Id));

            webDriver.FindElement(By.Id("link-VirtualProduct")).Click();
            string fileAddress = "";
            while (fileAddress == "")
            {
                foreach (IWebElement element in webDriver.FindElements(By.CssSelector("a.btn.btn-default")))
                {
                    if (element.GetAttribute("href").Contains("get-file-admin.php?file="))
                    {
                        fileAddress = element.GetAttribute("href");
                        element.Click();
                        break;
                    }
                }
                System.Threading.Thread.Sleep(100);
            }
            string downloadPath = webDriver.FindElement(By.Id("virtual_product_name")).GetAttribute("value");
            while (File.Exists(Path.Combine(v, downloadPath)))
            {
                downloadPath = "new" + downloadPath;
            }
            product.File = downloadPath;
            string directory = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            waitWhileDownlaodComplete(directory, downloadPath);
            string newPath = Path.Combine(directory, downloadPath);
            if (!newPath.Equals(downloadPath))
                File.Move(newPath, Path.Combine( v , downloadPath));
        }

        private void waitWhileDownlaodComplete(string directory, string pattern)
        {
            bool gotFile = false;
            while (!gotFile)
            {
                try
                {


                    StreamWriter writer = new StreamWriter(Directory.GetFiles(directory, pattern).First(), true);
                    gotFile = true;
                    writer.Close();
                }
                catch (Exception) { }
                System.Threading.Thread.Sleep(100);
            }
        }

        private void forceGo(string address)
        {

            while (!webDriver.Url.Contains(address))
            {
                webDriver.Url = BaseAddress + address;
                System.Threading.Thread.Sleep(100);
            }
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.UrlContains(address));
            List<string> invalid_inputs = new List<string>();
            webDriver.FindElement(By.CssSelector("a.btn.btn-continue")).Click();
        }

        public IEnumerable<Product> getProductsSummary(string directory = null)
        {
            forceGo("index.php?controller=AdminProducts&exportproduct");
            string dir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            waitWhileDownlaodComplete(dir, "product*.csv");
            string csvFile = Directory.GetFiles(dir, "product*.csv").First();
            foreach (Product product in GetProductsFromCSV(csvFile))
            {
                yield return product;
            }
            if (directory != null)
            {
                File.Move(csvFile, Path.Combine(directory, Path.GetFileName(csvFile)));
            }
            else
            {
                File.Delete(csvFile);
            }
            yield break;
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
                Product product = new Product(header.Contains("شناسه")) { Data = new CsvCollection() { Data = header + Environment.NewLine + reader.ReadLine() } };
                if (product.File != null)
                {
                    if (!File.Exists(product.File))
                    {
                        string absolute = Path.Combine(Path.GetDirectoryName(csvFile), product.File);
                        if (!File.Exists(absolute))
                        {
                            continue;
                        }
                        else
                        {
                            product.File = absolute;
                        }
                    }
                }
                yield return product;
            }
            reader.Close();
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
