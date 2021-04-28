
namespace TAOTool
{
    partial class Form1
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
            this.statusLabel = new System.Windows.Forms.Label();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.checkCSVButton = new System.Windows.Forms.Button();
            this.backgroundWorkerDBUpload = new System.ComponentModel.BackgroundWorker();
            this.dbStatusLabel = new System.Windows.Forms.Label();
            this.backgroundWorkerDBStatus = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(12, 7);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 24);
            this.statusLabel.TabIndex = 0;
            // 
            // logListBox
            // 
            this.logListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logListBox.FormattingEnabled = true;
            this.logListBox.HorizontalScrollbar = true;
            this.logListBox.ItemHeight = 22;
            this.logListBox.Location = new System.Drawing.Point(12, 34);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(468, 312);
            this.logListBox.TabIndex = 1;
            // 
            // uploadButton
            // 
            this.uploadButton.Enabled = false;
            this.uploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadButton.Location = new System.Drawing.Point(486, 97);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(137, 57);
            this.uploadButton.TabIndex = 4;
            this.uploadButton.Text = "Upload to Database";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(486, 277);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(137, 57);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.Filter = "*.csv";
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            // 
            // checkCSVButton
            // 
            this.checkCSVButton.Enabled = false;
            this.checkCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCSVButton.Location = new System.Drawing.Point(486, 34);
            this.checkCSVButton.Name = "checkCSVButton";
            this.checkCSVButton.Size = new System.Drawing.Size(137, 57);
            this.checkCSVButton.TabIndex = 7;
            this.checkCSVButton.Text = "Check CSV Format";
            this.checkCSVButton.UseVisualStyleBackColor = true;
            this.checkCSVButton.Click += new System.EventHandler(this.checkCSVButton_Click);
            // 
            // backgroundWorkerDBUpload
            // 
            this.backgroundWorkerDBUpload.WorkerSupportsCancellation = true;
            this.backgroundWorkerDBUpload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDBUpload_DoWork);
            this.backgroundWorkerDBUpload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDBUpload_RunWorkerCompleted);
            // 
            // dbStatusLabel
            // 
            this.dbStatusLabel.AutoSize = true;
            this.dbStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbStatusLabel.Location = new System.Drawing.Point(486, 7);
            this.dbStatusLabel.Name = "dbStatusLabel";
            this.dbStatusLabel.Size = new System.Drawing.Size(0, 18);
            this.dbStatusLabel.TabIndex = 8;
            // 
            // backgroundWorkerDBStatus
            // 
            this.backgroundWorkerDBStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDBStatus_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 356);
            this.Controls.Add(this.dbStatusLabel);
            this.Controls.Add(this.checkCSVButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "TaoTool - Drop CSV files into the application folder";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button exitButton;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button checkCSVButton;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDBUpload;
        private System.Windows.Forms.Label dbStatusLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDBStatus;
    }
}

