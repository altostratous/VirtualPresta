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
        OpenFileDialog imageOpenFileDialog = new OpenFileDialog() { Filter = "Image Files|*.jpg;*.jpeg;*.png", Multiselect=true};
        private void imagesButton_Click(object sender, EventArgs e)
        {
            string images = "";

            if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in imageOpenFileDialog.FileNames)
                {
                    imagePicturBox.ImageLocation = filename;
                    
                }
            }
        }
    }
}
