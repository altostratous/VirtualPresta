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
        
        
        private void uploadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            client = new PrestaShopClient("http://4piano.ir/admin300ix65de/", "develop@4piano.ir", "12345678", true);

            client.Dispose();
        }
        
    }
}
