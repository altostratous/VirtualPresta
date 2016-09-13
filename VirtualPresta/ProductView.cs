using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualPresta
{
    public partial class ProductView : UserControl
    {
        public Product Product
        {
            get
            {
                return new Product();
            }
            set
            {

            }
        }

        public ProductView()
        {
            InitializeComponent();
            Dock = DockStyle.Top;
        }
    }
}
