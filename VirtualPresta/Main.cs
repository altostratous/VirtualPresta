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
    public partial class Main : Form
    {
        PrestaShopClient client;
        public Main()
        {
            InitializeComponent();
        }
        
        
        private void uploadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string csvTempAddress = Path.Combine(Application.ExecutablePath, "temp.csv");
            List<Product> products = new List<Product>();
            foreach (ProductView productView in productsPanel.Controls)
            {
                products.Add(productView.Product);
            }
            int counter = 0;
            int totalWork = 2 + products.Count(product => { return !product.FileAndImagesSaved; });
            client = new PrestaShopClient("http://4piano.ir/admin300ix65de/", "develop@4piano.ir", "12345678", true);
            counter++;
            uploadBackgroundWorker.ReportProgress(100 * counter / totalWork);
            if (uploadBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            foreach (Product product in products)
            {
                client.Save(product);
                foreach(ProductView view in productsPanel.Controls)
                {
                    if(view.Product == product)
                    {
                        view.Product = product;
                    }
                }
                counter++;
                uploadBackgroundWorker.ReportProgress(100 * counter / totalWork);
                if (uploadBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            client.ApplyCSV(csvTempAddress, products);
            uploadBackgroundWorker.ReportProgress(100);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            uploadBackgroundWorker.RunWorkerAsync();
            productsPanel.Enabled = startButton.Enabled = false;
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
            productsPanel.Enabled = startButton.Enabled = true;
            try {
                client.Dispose();
            } catch (Exception)
            {
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
    }
}
