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

namespace VirtualPresta
{
    public partial class Main : Form
    {
        PrestaShopClient client;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new PrestaShopClient("http://4piano.ir/admin300ix65de/", "develop@4piano.ir", "12345678", false);
            client.GetProductForm();
            //client.Dispose();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Dispose();
        }
    }
}
