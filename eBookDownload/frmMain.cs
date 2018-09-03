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
using System.Net;
using System.IO;

namespace eBookDownload
{
    enum Work { eIdle=0, eSearching, eDownloading }

    public partial class frmMain : Form
    {
        private Work Work { get; set; }
        public frmMain()
        {
            InitializeComponent();
            searchWorker.WorkerSupportsCancellation = true;
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
                downloader.FileFound += new Downloader.FileFoundEventHandler(OnFileFound);
                downloader.QueryCancel += new Downloader.QueryCancelEventHandler(IsCancel);
                downloader.FileDownloaded += new Downloader.FileDownloadedEventHandler(OnFileDownloaded);
                downloader.AddKeyword += new Downloader.AddKeywordEventHandler(OnKeywordAdded);

                downloader = Downloaders.Instance.Next();
            }

            lbKeywords.Items.Clear();

            try
            {
                StreamReader reader = new StreamReader("Keywords.txt");
                string keyword = string.Empty;
                while ((keyword = reader.ReadLine()) != null)
                {
                    if (keyword != string.Empty)
                        lbKeywords.Items.Add(keyword);
                }
                reader.Close();
            }
            catch(Exception ex) {; }

            listView1.SmallImageList = new ImageList();
            listView1.SmallImageList.Images.Add(eBookDownloader.Properties.Resources.Success);
            listView1.SmallImageList.Images.Add(eBookDownloader.Properties.Resources.Error);
        }

        private int OnFileFound(object sender, DownloadEventArg e)
        {
            if (listView1.InvokeRequired)
            {
                return (int)this.Invoke(new Downloader.FileFoundEventHandler(OnFileFound), new object[] { sender, e });
            }
            else
            {
                ListViewItem item = listView1.Items.Add((listView1.Items.Count + 1).ToString());
                item.ImageIndex = -1;
                item.SubItems.Add(e.Title);
                item.SubItems.Add(e.URL);

                return item.Index;
            }
        }

        private bool IsCancel()
        {
            if (Work == Work.eSearching)
                return searchWorker.CancellationPending;
            else
                return downloadWorker.CancellationPending;
        }

        private void OnFileDownloaded(object sender, DownloadEventArg e)
        {
            if (e.ID == -1)
                return;
            if (listView1.InvokeRequired)
            {
                this.Invoke(new Downloader.FileDownloadedEventHandler(OnFileDownloaded), new object[] { sender, e });
            }
            else
            {
                ListViewItem item = listView1.Items[e.ID];
                if (e.Success)
                    item.ImageIndex = 0;
                else
                    item.ImageIndex = 1;
            }
        }

        private void OnKeywordAdded(string keyword)
        {
            if (lbKeywords.InvokeRequired)
            {
                this.Invoke(new Downloader.AddKeywordEventHandler(OnKeywordAdded), new object[] { keyword });
            }
            else
            {
                int index = lbKeywords.FindStringExact(keyword);
                if (index == -1)
                    lbKeywords.Items.Add(keyword);
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
            Work = Work.eSearching;

            if (searchWorker.IsBusy)
                searchWorker.CancelAsync();

            if (radInputKeyword.Checked)
                searchWorker.RunWorkerAsync(new object[] { downloader, txtKeyword.Text, chbAutoDownload.Checked });
            else
                searchWorker.RunWorkerAsync(new object[] { downloader, lbKeywords.SelectedItems, chbAutoDownload.Checked });
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
            if (searchWorker.CancellationPending)
                return;
            if (progressBar1.InvokeRequired || btnSearch.InvokeRequired || btnStop.InvokeRequired || 
                radInputKeyword.InvokeRequired || radSelectedKeywords.InvokeRequired || 
                chbAutoDownload.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                cbbProviders.InvokeRequired || btnDownload.InvokeRequired || lbKeywords.InvokeRequired || txtKeyword.InvokeRequired)
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
                lbKeywords.Enabled = false;
                txtKeyword.Enabled = false;
            }
        }


        private void OnSearchFinish()
        {
            if (searchWorker.CancellationPending)
                return;
            if (progressBar1.InvokeRequired || btnSearch.InvokeRequired || btnStop.InvokeRequired || 
                radInputKeyword.InvokeRequired || radSelectedKeywords.InvokeRequired || 
                chbAutoDownload.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                cbbProviders.InvokeRequired || btnDownload.InvokeRequired || listView1.InvokeRequired ||
                lbKeywords.InvokeRequired || txtKeyword.InvokeRequired)
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
                txtKeyword.Enabled = radInputKeyword.Checked;
                lbKeywords.Enabled = radSelectedKeywords.Checked;
                radSelectedKeywords.Enabled = radInputKeyword.Enabled = cbbProviders.SelectedIndex != -1;
                if (radSelectedKeywords.Checked)
                    lbKeywords_SelectedIndexChanged(this, new EventArgs());
                else
                    txtKeyword_TextChanged(this, new EventArgs());

                btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);

                Work = Work.eIdle;
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
                ListBox.SelectedObjectCollection keywords = parameters[1] as ListBox.SelectedObjectCollection;
                foreach(string strKeyword in keywords)
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
            if (searchWorker.IsBusy)
            {
                DialogResult res = MessageBox.Show("Do you want to abort current operation?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                    e.Cancel = true;
                else
                {
                    searchWorker.CancelAsync();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (searchWorker.IsBusy || downloadWorker.IsBusy)
            {
                DialogResult res = MessageBox.Show("Do you want to abort current operation?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                    return;
                else
                {
                    if (Work == Work.eSearching)
                    {
                        searchWorker.WorkerSupportsCancellation = true;
                        searchWorker.CancelAsync();
                    }
                    else if(Work== Work.eDownloading)
                    {
                        downloadWorker.WorkerSupportsCancellation = true;
                        downloadWorker.CancelAsync();
                    }
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

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("Keywords.txt");
            foreach(string keyword in lbKeywords.Items)
            {
                writer.WriteLine(keyword);
            }
            writer.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Work = Work.eDownloading;

            if (downloadWorker.IsBusy)
                downloadWorker.CancelAsync();

            Downloader downloader = cbbProviders.SelectedItem as Downloader;
            if (null == downloader)
            {
                return;
            }

            object[] selectedItems = new object[listView1.CheckedItems.Count];
            listView1.CheckedItems.CopyTo(selectedItems, 0);
            downloadWorker.RunWorkerAsync(new object[] { downloader, selectedItems });
        }

        private void downloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(listView1.InvokeRequired)
            {
                Invoke(new System.ComponentModel.DoWorkEventHandler(downloadWorker_DoWork), new object[]{sender, e });
            }
            WebClient webClient = new WebClient();
            object[] arguments = e.Argument as object[];
            string url = string.Empty;
            string title = string.Empty;
            string location = string.Empty;
            int id = -1;
            Downloader downloader = arguments[0] as Downloader;
            object[] items = arguments[1] as object[];
            foreach (ListViewItem item in items)
            {
                id = item.Index;
                title = item.SubItems[1].Text;
                url = item.SubItems[2].Text;
                try
                {
                    location = downloader.DownloadFile(url, title, id);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
        }

        private void downloadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void downloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
