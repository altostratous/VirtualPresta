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
using AltoWebClient;

namespace VirtualPresta
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CredentialWebClient client = new CredentialWebClient();
            client.QueryStrnigVaribalesToPreserve.Add("token");
            client.Login("http://4piano.ir/admin300ix65de/index.php?controller=AdminLogin", "develop@4piano.ir", "12345678");
            MessageBox.Show("Hello");
        }
    }
}
