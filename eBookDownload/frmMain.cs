using eBookDownloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBookDownload
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private static bool IsChecked(ListViewItem item)
        {
            return item.Checked;
        }

        private static bool IsUnChecked(ListViewItem item)
        {
            return !item.Checked;
        }

        private static bool IsExist(object item, string val)
        {
            return item as string == val;
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
            btnAddKeyword.Enabled = btnClearKeywords.Enabled= lbKeywords.Enabled = (radSelectedKeywords.Checked);
            btnRemoveKeyword.Enabled = lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void radInputKeyword_CheckedChanged(object sender, EventArgs e)
        {
            txtKeyword.Enabled = radInputKeyword.Checked;
            btnAddKeyword.Enabled  = btnClearKeywords.Enabled = lbKeywords.Enabled = (!radInputKeyword.Checked);
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
            btnDownload.Enabled = false;

            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();

            if (radInputKeyword.Checked)
                backgroundWorker1.RunWorkerAsync(new object[] { downloader, txtKeyword.Text, chbAutoDownload.Checked });
            else
                backgroundWorker1.RunWorkerAsync(new object[] { downloader, lbKeywords.Items , chbAutoDownload.Checked });
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

            bool bAllUnChecked = listView1.Items.OfType<ListViewItem>().ToList().TrueForAll(IsUnChecked);
            btnDownload.Enabled = !bAllUnChecked;
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
                cbbProviders.InvokeRequired || btnDownload.InvokeRequired)
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
                cbbProviders.InvokeRequired || btnDownload.InvokeRequired || listView1.InvokeRequired)
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
            bool bAutoDownload = false;

            if (parameters.Length > 2)
                bAutoDownload = (bool)parameters[2];

            if(parameters[1] is string)
            {
                string strKeyword = parameters[1] as string;
                downloader.Search(strKeyword, bAutoDownload);
            }
            else
            {
                ListBox.ObjectCollection items = parameters[1] as ListBox.ObjectCollection;
                foreach(string strKeyword in items)
                {
                    downloader.Search(strKeyword, bAutoDownload);
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
                {
                    backgroundWorker1.CancelAsync();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                DialogResult res = MessageBox.Show("Do you want to abort current operation?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                    return;
                else
                {
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.CancelAsync();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddKeyword_Click(object sender, EventArgs e)
        {
            frmInputKeyword frm = new frmInputKeyword();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            if (!lbKeywords.Items.Contains(frm.Keyword))
                lbKeywords.Items.Add(frm.Keyword);

            btnRemoveKeyword.Enabled = lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void btnClearKeywords_Click(object sender, EventArgs e)
        {
            lbKeywords.Items.Clear();
            btnRemoveKeyword.Enabled = lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void btnRemoveKeyword_Click(object sender, EventArgs e)
        {
            lbKeywords.Items.RemoveAt(lbKeywords.SelectedIndex);
            btnRemoveKeyword.Enabled = lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void lbKeywords_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveKeyword.Enabled = lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }
    }
}
