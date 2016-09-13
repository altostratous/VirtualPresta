namespace VirtualPresta
{
    partial class ProductView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.savedCheckbox = new System.Windows.Forms.CheckBox();
            this.uploadedCheckbox = new System.Windows.Forms.CheckBox();
            this.fileButton = new System.Windows.Forms.Button();
            this.imagesButton = new System.Windows.Forms.Button();
            this.imagePicturBox = new System.Windows.Forms.PictureBox();
            this.csvView = new VirtualPresta.CSVView();
            ((System.ComponentModel.ISupportInitialize)(this.imagePicturBox)).BeginInit();
            this.SuspendLayout();
            // 
            // savedCheckbox
            // 
            this.savedCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savedCheckbox.AutoSize = true;
            this.savedCheckbox.Location = new System.Drawing.Point(932, 5);
            this.savedCheckbox.Name = "savedCheckbox";
            this.savedCheckbox.Size = new System.Drawing.Size(57, 17);
            this.savedCheckbox.TabIndex = 3;
            this.savedCheckbox.Text = "Saved";
            this.savedCheckbox.UseVisualStyleBackColor = true;
            // 
            // uploadedCheckbox
            // 
            this.uploadedCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadedCheckbox.AutoSize = true;
            this.uploadedCheckbox.Location = new System.Drawing.Point(932, 28);
            this.uploadedCheckbox.Name = "uploadedCheckbox";
            this.uploadedCheckbox.Size = new System.Drawing.Size(72, 17);
            this.uploadedCheckbox.TabIndex = 4;
            this.uploadedCheckbox.Text = "Uploaded";
            this.uploadedCheckbox.UseVisualStyleBackColor = true;
            // 
            // fileButton
            // 
            this.fileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fileButton.Location = new System.Drawing.Point(69, 3);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(40, 44);
            this.fileButton.TabIndex = 5;
            this.fileButton.Text = "File";
            this.fileButton.UseVisualStyleBackColor = true;
            // 
            // imagesButton
            // 
            this.imagesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.imagesButton.Location = new System.Drawing.Point(115, 3);
            this.imagesButton.Name = "imagesButton";
            this.imagesButton.Size = new System.Drawing.Size(52, 44);
            this.imagesButton.TabIndex = 6;
            this.imagesButton.Text = "Images";
            this.imagesButton.UseVisualStyleBackColor = true;
            // 
            // imagePicturBox
            // 
            this.imagePicturBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.imagePicturBox.Location = new System.Drawing.Point(3, 3);
            this.imagePicturBox.Name = "imagePicturBox";
            this.imagePicturBox.Size = new System.Drawing.Size(60, 44);
            this.imagePicturBox.TabIndex = 7;
            this.imagePicturBox.TabStop = false;
            // 
            // csvView
            // 
            this.csvView.CSV = "\r\n";
            this.csvView.Location = new System.Drawing.Point(173, 3);
            this.csvView.Name = "csvView";
            this.csvView.Size = new System.Drawing.Size(753, 44);
            this.csvView.TabIndex = 8;
            // 
            // ProductView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.csvView);
            this.Controls.Add(this.imagePicturBox);
            this.Controls.Add(this.imagesButton);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.uploadedCheckbox);
            this.Controls.Add(this.savedCheckbox);
            this.Name = "ProductView";
            this.Size = new System.Drawing.Size(1007, 50);
            ((System.ComponentModel.ISupportInitialize)(this.imagePicturBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox savedCheckbox;
        private System.Windows.Forms.CheckBox uploadedCheckbox;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Button imagesButton;
        private System.Windows.Forms.PictureBox imagePicturBox;
        private CSVView csvView;
    }
}
