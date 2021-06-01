using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TAOTool
{
    public partial class Form1 : Form
    {
        CSVAccess csvr = new CSVAccess();
        DBAccess db = new DBAccess();
        List<string> csvChangedfiles = new List<string>();
        List<string> csvUploadfiles = new List<string>();
        List<string> csvLog = new List<string>();
        List<Reading> csvFileBuffer = new List<Reading>();
        int readingTotalRows = -1;
        public Form1()
        {
            InitializeComponent();
            //keep track of csv files
            fileSystemWatcher1.Path = Application.StartupPath;
            //log listbox on the form
            logListBox.DataSource = this.csvLog;
            //thread that keeps updating status of database connection
            backgroundWorkerDBStatus.RunWorkerAsync();

        }
        //Update method for our listbox on the form
        public void updateLog()
        {
            logListBox.DataSource = null;
            logListBox.DataSource = this.csvLog;
        }
        //Keep track of any csv in application folder that has changed
        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (!this.csvChangedfiles.Contains(e.FullPath))
            {
                //remove from upload list if the file has changed
                if (this.csvUploadfiles.Contains(e.FullPath))
                {
                    this.csvUploadfiles.Remove(e.FullPath);
                }
                this.csvChangedfiles.Add(e.FullPath);
                this.csvLog.Clear();
                this.csvLog.AddRange(csvChangedfiles);
                updateLog();
                statusLabel.Text = "Following file(s) have been found";
                checkCSVButton.Enabled = true;
            }
        }
        //Keep track of any csv in application folder that has been deleted
        private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            this.csvUploadfiles.Remove(e.FullPath);
            this.csvChangedfiles.Remove(e.FullPath);
            this.csvLog.Clear();
            this.csvLog.AddRange(csvChangedfiles);
            updateLog();
            if (!csvChangedfiles.Any())
            {
                checkCSVButton.Enabled = false;
            }
        }

        private void checkCSVButton_Click(object sender, EventArgs e)
        {
            this.csvLog.Clear();
            updateLog();
            List<string> newCSVlist = new List<string>();
            newCSVlist.AddRange(this.csvChangedfiles);
            foreach (var csvfile in this.csvChangedfiles)
            {
                //check for errors in the csv file
                List<string> result = csvr.checkCSVFile(@csvfile);
                //if errors are found display in log
                if (result.Any())
                {
                    this.csvLog.AddRange(result);
                    updateLog();
                }
                else 
                {
                    //no errors found then add to upload list
                    if (!this.csvUploadfiles.Contains(@csvfile))
                    {
                        this.csvUploadfiles.Add(@csvfile);
                    }
                    //remove from list of files we are format checking
                    newCSVlist.Remove(@csvfile);
                    this.csvLog.Add("Format Validated : "+ @csvfile);
                    updateLog();
                }
            }
            if (!newCSVlist.Any())
            {
                csvChangedfiles.Clear();
                checkCSVButton.Enabled = false;
                uploadButton.Enabled = true;
            }   
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (db.isConnected()) { 
                uploadButton.Enabled = false;
                exitButton.Enabled = false;
                this.csvLog.Clear();
                updateLog();
                statusLabel.Text = "Uploading...";          
                backgroundWorkerDBUpload.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Database is not connected. Connect and try again");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //setup our database thread
        private void backgroundWorkerDBUpload_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            BackgroundDatabaseSave(helperBW);
            if (helperBW.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        //thread specifically for saving to database so our application doesnt break during big data saves
        private void BackgroundDatabaseSave(BackgroundWorker bw)
        {
            foreach (var csvfile in this.csvUploadfiles)
            {
                try
                {
                    this.csvFileBuffer = csvr.readCSVFile(@csvfile);
                    db.createReadings(csvFileBuffer);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error - Database save failed !");
                }
            }
        }
        //sets status after database has been uploaded
        private void backgroundWorkerDBUpload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.csvLog.Clear();
            //this.csvLog.AddRange(csvUploadfiles);
            //updateLog();
            this.csvUploadfiles.Clear();
            int currentRows = db.getReadingsRows();
            int rowsAdded = currentRows - this.readingTotalRows;
            this.readingTotalRows = currentRows;
            statusLabel.Text = "Uploading Done - Added total of "+rowsAdded.ToString()+ " rows";
            exitButton.Enabled = true;
        }
        //check if our database is online or offline
        private void backgroundWorkerDBStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowprogress = 0;
            while (true)
            {
                string status = "";
                if (db.isConnected())
                {
                    dbStatusLabel.Invoke(new Action(() =>
                    {
                        dbStatusLabel.Text = "Database Online";
                        dbStatusLabel.ForeColor = System.Drawing.Color.Green;
                    }));
                    //set current rows so we can check later our progress on uploads
                    if (this.readingTotalRows == -1)
                    {
                        this.readingTotalRows = db.getReadingsRows();
                    }
                    //if the thread is busy lets update progress
                    if (backgroundWorkerDBUpload.IsBusy)
                    {
                        int Rows = db.getReadingsRows()-this.readingTotalRows;
                        dbStatusLabel.Invoke(new Action(() =>
                        {
                            statusLabel.Text = "Uploading - Added "+Rows.ToString()+" rows of "+ csvFileBuffer.Count+ " rows";
                            this.csvLog.AddRange(csvFileBuffer.GetRange(rowprogress, Rows-rowprogress).Select(s => s.ToString()));
                            rowprogress = Rows + 1;
                            updateLog();
                            logListBox.SelectedIndex = logListBox.Items.Count - 1;
                            logListBox.SelectedIndex = -1;
                        }));
                    }
                    else if (csvFileBuffer.Count>0)
                    {
                        int Rows = db.getReadingsRows() - this.readingTotalRows;
                        dbStatusLabel.Invoke(new Action(() =>
                        {
                            this.csvLog.AddRange(csvFileBuffer.GetRange(rowprogress, csvFileBuffer.Count- rowprogress).Select(s => s.ToString()));
                            updateLog();
                            logListBox.SelectedIndex = logListBox.Items.Count - 1;
                            logListBox.SelectedIndex = -1;
                            csvFileBuffer = new List<Reading>();
                            this.readingTotalRows = -1;
                        }));
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    dbStatusLabel.Invoke(new Action(() =>
                    {
                        dbStatusLabel.Text = "Database Offline";
                        dbStatusLabel.ForeColor = System.Drawing.Color.Red;
                    }));
                    //greater timeout on thread if offline
                    Thread.Sleep(10000);
                } 
            }
        }
    }
}
