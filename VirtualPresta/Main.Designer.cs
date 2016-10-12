namespace VirtualPresta
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.openButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.productsPanel = new System.Windows.Forms.Panel();
            this.uploadBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.usertextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordtextbox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageUpload = new System.Windows.Forms.TabPage();
            this.tabPageManage = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.backupBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl.SuspendLayout();
            this.tabPageUpload.SuspendLayout();
            this.tabPageManage.SuspendLayout();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openButton.Location = new System.Drawing.Point(6, 318);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "OpenFile";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startButton.Location = new System.Drawing.Point(87, 318);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start Upload";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.Location = new System.Drawing.Point(168, 318);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 289);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(905, 23);
            this.progressBar.TabIndex = 0;
            // 
            // productsPanel
            // 
            this.productsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productsPanel.AutoScroll = true;
            this.productsPanel.Location = new System.Drawing.Point(6, 6);
            this.productsPanel.Name = "productsPanel";
            this.productsPanel.Size = new System.Drawing.Size(905, 277);
            this.productsPanel.TabIndex = 4;
            // 
            // uploadBackgroundWorker
            // 
            this.uploadBackgroundWorker.WorkerReportsProgress = true;
            this.uploadBackgroundWorker.WorkerSupportsCancellation = true;
            this.uploadBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploadBackgroundWorker_DoWork);
            this.uploadBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.uploadBackgroundWorker_ProgressChanged);
            this.uploadBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uploadBackgroundWorker_RunWorkerCompleted);
            // 
            // usertextbox
            // 
            this.usertextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::VirtualPresta.Properties.Settings.Default, "username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.usertextbox.Location = new System.Drawing.Point(53, 6);
            this.usertextbox.Name = "usertextbox";
            this.usertextbox.Size = new System.Drawing.Size(100, 20);
            this.usertextbox.TabIndex = 5;
            this.usertextbox.Text = global::VirtualPresta.Properties.Settings.Default.username;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password:";
            // 
            // passwordtextbox
            // 
            this.passwordtextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::VirtualPresta.Properties.Settings.Default, "password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.passwordtextbox.Location = new System.Drawing.Point(221, 6);
            this.passwordtextbox.Name = "passwordtextbox";
            this.passwordtextbox.PasswordChar = '*';
            this.passwordtextbox.Size = new System.Drawing.Size(100, 20);
            this.passwordtextbox.TabIndex = 8;
            this.passwordtextbox.Text = global::VirtualPresta.Properties.Settings.Default.password;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageUpload);
            this.tabControl.Controls.Add(this.tabPageManage);
            this.tabControl.Location = new System.Drawing.Point(12, 32);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(934, 372);
            this.tabControl.TabIndex = 9;
            // 
            // tabPageUpload
            // 
            this.tabPageUpload.Controls.Add(this.openButton);
            this.tabPageUpload.Controls.Add(this.startButton);
            this.tabPageUpload.Controls.Add(this.cancelButton);
            this.tabPageUpload.Controls.Add(this.progressBar);
            this.tabPageUpload.Controls.Add(this.productsPanel);
            this.tabPageUpload.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpload.Name = "tabPageUpload";
            this.tabPageUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpload.Size = new System.Drawing.Size(926, 346);
            this.tabPageUpload.TabIndex = 0;
            this.tabPageUpload.Text = "Adding Products";
            this.tabPageUpload.UseVisualStyleBackColor = true;
            // 
            // tabPageManage
            // 
            this.tabPageManage.Controls.Add(this.progressBar1);
            this.tabPageManage.Controls.Add(this.button1);
            this.tabPageManage.Controls.Add(this.backupButton);
            this.tabPageManage.Location = new System.Drawing.Point(4, 22);
            this.tabPageManage.Name = "tabPageManage";
            this.tabPageManage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManage.Size = new System.Drawing.Size(926, 346);
            this.tabPageManage.TabIndex = 1;
            this.tabPageManage.Text = "Manage Products";
            this.tabPageManage.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(917, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(110, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backupButton
            // 
            this.backupButton.Location = new System.Drawing.Point(3, 35);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(101, 23);
            this.backupButton.TabIndex = 6;
            this.backupButton.Text = "Create Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.backupButton_Click);
            // 
            // backupBackgroundWorker
            // 
            this.backupBackgroundWorker.WorkerReportsProgress = true;
            this.backupBackgroundWorker.WorkerSupportsCancellation = true;
            this.backupBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backupBackgroundWorker_DoWork);
            this.backupBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backupBackgroundWorker_ProgressChanged);
            this.backupBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backupBackgroundWorker_RunWorkerCompleted);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 416);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.passwordtextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usertextbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Virtual Presta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPageUpload.ResumeLayout(false);
            this.tabPageManage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel productsPanel;
        private System.ComponentModel.BackgroundWorker uploadBackgroundWorker;
        private System.Windows.Forms.TextBox usertextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordtextbox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageUpload;
        private System.Windows.Forms.TabPage tabPageManage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button backupButton;
        private System.ComponentModel.BackgroundWorker backupBackgroundWorker;
    }
}

