using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBookDownload
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private static bool IsChecked(ListViewItem item)
        {
            return item.Checked;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Downloaders.Instance.First();
            Downloader downloader = Downloaders.Instance.Next();
            while (downloader != null)
            {
                cbbProviders.DisplayMember = "Name";
                cbbProviders.ValueMember = "Instance";
                cbbProviders.Items.Add(downloader);
                downloader.FileFound += new Downloader.DownloadEventHandler(OnFileFound);
                downloader = Downloaders.Instance.Next();
            }
        }

        private void OnFileFound(object sender, DownloadEventArg e)
        {
            if (listView1.InvokeRequired)
            {
                this.Invoke(new Downloader.DownloadEventHandler(OnFileFound), new object[] { sender, e });
            }
            else
            {
                ListViewItem item = listView1.Items.Add((listView1.Items.Count + 1).ToString());
                item.SubItems.Add(e.Title);
                item.SubItems.Add(e.URL);
            }
        }

        private void radSelectedKeywords_CheckedChanged(object sender, EventArgs e)
        {
            txtKeyword.Enabled = radInputKeyword.Checked;
            btnAddKeyword.Enabled = btnRemoveKeyword.Enabled = btnClearKeywords.Enabled = lbKeywords.Enabled = (radSelectedKeywords.Checked);
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void radInputKeyword_CheckedChanged(object sender, EventArgs e)
        {
            txtKeyword.Enabled = radInputKeyword.Checked;
            btnAddKeyword.Enabled = btnRemoveKeyword.Enabled = btnClearKeywords.Enabled = lbKeywords.Enabled = (!radInputKeyword.Checked);
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Downloader downloader = cbbProviders.SelectedItem as Downloader;
            if (null == downloader)
            {
                return;
            }
            listView1.Items.Clear();
            chbCheckUnCheckAll.Checked = false;
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();

            if (radInputKeyword.Checked)
                backgroundWorker1.RunWorkerAsync(new object[] { downloader, txtKeyword.Text });
            else
                backgroundWorker1.RunWorkerAsync(new object[] { downloader, lbKeywords.Items });
        }

        private void cbbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            radSelectedKeywords.Enabled = radInputKeyword.Enabled = cbbProviders.SelectedIndex != -1;
            radInputKeyword_CheckedChanged(this, new EventArgs());
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (sender == this.ActiveControl)
            {
                bool bAllChecked = listView1.Items.OfType<ListViewItem>().ToList().TrueForAll(IsChecked);
                chbCheckUnCheckAll.Checked = bAllChecked;
            }
        }

        private void chbCheckUnCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == this.ActiveControl)
            {
                bool bChecked = chbCheckUnCheckAll.Checked;
                listView1.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = bChecked);
            }
        }

        private void OnSearchStart()
        {
            if (progressBar1.InvokeRequired || btnSearch.InvokeRequired || btnStop.InvokeRequired || 
                radInputKeyword.InvokeRequired || radSelectedKeywords.InvokeRequired || 
                chbAutoDownload.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                cbbProviders.InvokeRequired)
            {
                this.Invoke(new Action(OnSearchStart), new object[] { });
            }
            else
            {
                progressBar1.Visible = true;
                btnStop.Enabled = true;
                btnSearch.Enabled = false;
                chbAutoDownload.Enabled = false;
                radSelectedKeywords.Enabled = false;
                radInputKeyword.Enabled = false;
                chbCheckUnCheckAll.Enabled = false;
                cbbProviders.Enabled = false;
            }
        }


        private void OnSearchFinish()
        {
            if (progressBar1.InvokeRequired || btnSearch.InvokeRequired || btnStop.InvokeRequired || 
                radInputKeyword.InvokeRequired || radSelectedKeywords.InvokeRequired || 
                chbAutoDownload.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                cbbProviders.InvokeRequired)
            {
                this.Invoke(new Action(OnSearchFinish), new object[] { });
            }
            else
            {
                progressBar1.Visible = false;
                btnStop.Enabled = false;
                chbAutoDownload.Enabled = true;
                chbCheckUnCheckAll.Enabled = true;
                cbbProviders.Enabled = true;

                radSelectedKeywords.Enabled = radInputKeyword.Enabled = cbbProviders.SelectedIndex != -1;
                btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            OnSearchStart();

            object[] parameters = e.Argument as object[];
            Downloader downloader = parameters[0] as Downloader;
            
            if(parameters[1] is string)
            {
                string strKeyword = parameters[1] as string;
                downloader.Download(strKeyword);
            }
            else
            {
                ListBox.ObjectCollection items = parameters[1] as ListBox.ObjectCollection;
                foreach(string strKeyword in items)
                {
                    downloader.Download(strKeyword);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSearchFinish();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                DialogResult res = MessageBox.Show("Do you want to abort current operation?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                    e.Cancel = true;
                else
                    backgroundWorker1.CancelAsync();
            }
        }
    }
}
