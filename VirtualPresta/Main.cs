using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace VirtualPresta
{
    public partial class Main : Form, ILogger
    {
        PrestaShopClient client;
        public Main()
        {
            log("program started at" + DateTime.Now.ToString("hh:mm:ss"));
            InitializeComponent();
        }
        
        public void log(string tolog)
        {
            StreamWriter writer = new StreamWriter("log.txt", true);
            writer.WriteLine(tolog);
            writer.Close();
        }
        private void uploadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //string csvTempAddress = Path.Combine(Application.ExecutablePath, "temp.csv");
           
            List<Product> products = new List<Product>();
            foreach (ProductView productView in productsPanel.Controls)
            {
                products.Add(productView.Product);
            }
            log("got products");
            int counter = 0;
            int totalWork = 2 + products.Count(product => { return !product.FileAndImagesSaved; });
            
            client = new PrestaShopClient("http://4piano.ir/admin300ix65de/", Properties.Settings.Default.username, Properties.Settings.Default.password, false, this);
            log("initialized client");
            counter++;
            uploadBackgroundWorker.ReportProgress(100 * counter / totalWork);
            if (uploadBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                log("cancelling");
                return;
            }
            foreach (Product product in products.FindAll(item => { return !item.FileAndImagesSaved; })) 
            {
                //try {
                    client.Save(product);
                log("saved one product");
                //} catch (Exception)
                //{
                //    continue;
                //}
                foreach(ProductView view in productsPanel.Controls)
                {
                    if(view.Product == product)
                    {
                        view.BeginInvoke(new Action(delegate { view.UpdateView(); }));
                        
                    }
                }
                counter++;
                uploadBackgroundWorker.ReportProgress(100 * counter / totalWork);
                if (uploadBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    log("cancelling");
                    return;
                }
            }
            //client.ApplyCSV(csvTempAddress, products);
            client.ApplyCSV(openFileDialog.FileName, products);
            foreach (ProductView view in productsPanel.Controls)
            {
                view.Product.CSVPushed = true;
                view.BeginInvoke(new Action(delegate { view.UpdateView(); }));
            }
            log("applied csv");
            uploadBackgroundWorker.ReportProgress(100);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            uploadBackgroundWorker.RunWorkerAsync();
            //productsPanel.Enabled = startButton.Enabled = false;
        }

        private void uploadBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.BeginInvoke(new Action(delegate
            {
                progressBar.Value = e.ProgressPercentage;
            }));
        }

        private void uploadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //productsPanel.Enabled = startButton.Enabled = true;;
            log("background worker completd");
            log("background worker is " + (e.Cancelled ? "" : "not") + " cancelled");
            try {
                client.Dispose();
            } catch (Exception)
            {
                log("could not dispose driver");
                return;
            }
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "CSV Files|*.csv" };

        private void openButton_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog () == DialogResult.OK)
            {
                productsPanel.Controls.Clear();
                IEnumerable<Product> products = PrestaShopClient.GetProductsFromCSV(openFileDialog.FileName);
                foreach (Product product in products)
                {
                    productsPanel.Controls.Add(new ProductView(product) { Dock = DockStyle.Top});
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            uploadBackgroundWorker.CancelAsync();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            try
            {
                client.Dispose();
            }
            catch (Exception)
            {

            }
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            backupBackgroundWorker.RunWorkerAsync();
        }

        private void backupBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Product> products = new List<Product>();
            client = new PrestaShopClient("http://4piano.ir/admin300ix65de/", Properties.Settings.Default.username, Properties.Settings.Default.password, false, this);
            foreach (Product product in client.getProductsSummary())
            {
                products.Add(product);
            }
            foreach (Product product in products)
            {
                client.DownloadProduct(product, Path.GetDirectoryName(Application.ExecutablePath));
            }
            client.Dispose();
        }
        
    }
}
