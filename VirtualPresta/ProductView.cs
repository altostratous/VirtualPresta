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
        private Product product;
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                UpdateView();
            }
        }

        public void UpdateView()
        {
            savedCheckbox.Checked = product.Saved;
            uploadedCheckbox.Checked = product.FileAndImagesSaved;
            csvSavedCheckbox.Checked = product.CSVPushed;
            csvView.CSV = product.StandardCSV.Data;
            try
            {
                imagePicturBox.ImageLocation = product.ImageFiles.First();
            }
            catch (Exception) { }
        }

        public ProductView()
        {
            InitializeComponent();
            Dock = DockStyle.Top;
        }

        public ProductView(Product product)
        {
            InitializeComponent();
            Product = product;
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

                UpdateView();
            }
        }

        private OpenFileDialog fileOpenFileDialog = new OpenFileDialog() { };
        private void fileButton_Click(object sender, EventArgs e)
        {
            if(fileOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                product.File = fileOpenFileDialog.FileName;
                UpdateView();
            }
        }
    }
}
