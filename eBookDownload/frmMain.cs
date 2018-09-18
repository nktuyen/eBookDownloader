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

namespace eBookDownloader
{
    enum Work { eIdle = 0, eSearching, eDownloading }

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
            if (Downloaders.Instance.First())
            {
                BookStore downloader = Downloaders.Instance.Next();
                while (downloader != null)
                {
                    cbbProviders.DisplayMember = "Name";
                    cbbProviders.ValueMember = "Instance";
                    cbbProviders.Items.Add(downloader);
                    downloader.BookFound += new BookStore.BookFoundEventHandler(OnFileFound);
                    downloader.QueryCancel += new BookStore.QueryCancelEventHandler(IsCancel);
                    downloader.SearchStarted += new BookStore.SearchStartedEventHandler(OnKeywordSearchStarted);

                    downloader = Downloaders.Instance.Next();
                }
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
            catch (Exception ex) {; }

            lvwBooks.SmallImageList = new ImageList();
            lvwBooks.SmallImageList.Images.Add(eBookDownloader.Properties.Resources.Success);
            lvwBooks.SmallImageList.Images.Add(eBookDownloader.Properties.Resources.Error);

            searchWorker.WorkerSupportsCancellation = true;
            downloadWorker.WorkerSupportsCancellation = true;
        }

        private int OnFileFound(object sender, BookFoundEventArg e)
        {
            if (lvwBooks.InvokeRequired)
            {
                return (int)this.Invoke(new BookStore.BookFoundEventHandler(OnFileFound), new object[] { sender, e });
            }
            else
            {
                ListViewItem item = lvwBooks.Items.Add((lvwBooks.Items.Count + 1).ToString());
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

        private void OnKeywordSearchStarted(string keyword)
        {
            if (lbKeywords.InvokeRequired)
            {
                this.Invoke(new BookStore.SearchStartedEventHandler(OnKeywordSearchStarted), new object[] { keyword });
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
            btnAddKeyword.Enabled = btnClearKeywords.Enabled = lbKeywords.Enabled = (radSelectedKeywords.Checked);
            btnRemoveKeyword.Enabled = radSelectedKeywords.Checked && lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void radInputKeyword_CheckedChanged(object sender, EventArgs e)
        {
            txtKeyword.Enabled = radInputKeyword.Checked;
            btnAddKeyword.Enabled = btnClearKeywords.Enabled = lbKeywords.Enabled = (!radInputKeyword.Checked);
            btnRemoveKeyword.Enabled = radSelectedKeywords.Checked && lbKeywords.SelectedIndex != -1;
            btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Work != Work.eIdle && Work != Work.eSearching)
                return;

            if (Work == Work.eIdle)
            {
                BookStore downloader = cbbProviders.SelectedItem as BookStore;
                if (null == downloader)
                {
                    return;
                }                

                if (searchWorker.IsBusy)
                    return;

                Work = Work.eSearching;

                if (radInputKeyword.Checked)
                    searchWorker.RunWorkerAsync(new object[] { downloader, txtKeyword.Text });
                else
                    searchWorker.RunWorkerAsync(new object[] { downloader, lbKeywords.SelectedItems });
            }
            else if(Work== Work.eSearching)
            {
                if (searchWorker.IsBusy)
                {
                    DialogResult res = MessageBox.Show("Do you want to abort current operation (searching) ?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res != DialogResult.Yes)
                        return;

                    searchWorker.CancelAsync();
                }
            }
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

        private void lvwBooks_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (sender == this.ActiveControl)
            {
                bool bAllChecked = lvwBooks.Items.OfType<ListViewItem>().ToList().TrueForAll(IsChecked);
                chbCheckUnCheckAll.Checked = bAllChecked;
            }

            bool bAllUnChecked = lvwBooks.Items.OfType<ListViewItem>().ToList().TrueForAll(IsUnChecked);
            btnDownload.Enabled = !bAllUnChecked;
        }

        private void chbCheckUnCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == this.ActiveControl)
            {
                bool bChecked = chbCheckUnCheckAll.Checked;
                lvwBooks.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = bChecked);
            }
        }

        private void OnSearchWorkerStart()
        {
            if (progressBar.InvokeRequired || btnSearch.InvokeRequired ||
                radInputKeyword.InvokeRequired || radSelectedKeywords.InvokeRequired ||
                chbAutoDownload.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                cbbProviders.InvokeRequired || btnDownload.InvokeRequired || 
                lbKeywords.InvokeRequired || txtKeyword.InvokeRequired ||
                lvwBooks.InvokeRequired || chbCheckUnCheckAll.InvokeRequired)
            {
                this.Invoke(new Action(OnSearchWorkerStart), new object[] { });
            }
            else
            {
                lvwBooks.Items.Clear();
                chbCheckUnCheckAll.Checked = false;
                progressBar.Visible = true;
                chbAutoDownload.Enabled = false;
                radSelectedKeywords.Enabled = false;
                radInputKeyword.Enabled = false;
                chbCheckUnCheckAll.Enabled = false;
                cbbProviders.Enabled = false;
                lbKeywords.Enabled = false;
                txtKeyword.Enabled = false;
                lvwBooks.Enabled = false;

                if (Work == Work.eSearching)
                {
                    btnSearch.Text = eBookDownloader.Properties.Resources.IDS_CANCEL;
                    btnDownload.Enabled = false;
                }
                else if(Work == Work.eDownloading)
                {
                    btnSearch.Enabled = false;
                }
            }
        }


        private void OnSearchWorkerFinish()
        {
            if (progressBar.InvokeRequired || btnSearch.InvokeRequired ||
                radInputKeyword.InvokeRequired || radSelectedKeywords.InvokeRequired ||
                chbAutoDownload.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                cbbProviders.InvokeRequired || btnDownload.InvokeRequired ||
                lbKeywords.InvokeRequired || txtKeyword.InvokeRequired ||
                lvwBooks.InvokeRequired || chbCheckUnCheckAll.InvokeRequired)
            {
                this.Invoke(new Action(OnSearchWorkerFinish), new object[] { });
            }
            else
            {
                progressBar.Visible = false;
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

                lvwBooks.Enabled = true;

                if(Work == Work.eDownloading)
                {
                    btnSearch.Enabled = (txtKeyword.Text != string.Empty && radInputKeyword.Checked) || (lbKeywords.SelectedIndex != -1 && radSelectedKeywords.Checked);
                }
                else if(Work == Work.eSearching)
                {
                    btnSearch.Text = eBookDownloader.Properties.Resources.IDS_SEARCH;
                    lvwBooks_ItemChecked(this, null);
                }

                Work = Work.eIdle;
            }
        }

        private void searchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OnSearchWorkerStart();

            object[] parameters = e.Argument as object[];
            BookStore downloader = parameters[0] as BookStore;

            if (parameters[1] is string)
            {
                string strKeyword = parameters[1] as string;
                downloader.Search(strKeyword);
            }
            else
            {
                ListBox.SelectedObjectCollection keywords = parameters[1] as ListBox.SelectedObjectCollection;
                foreach (string strKeyword in keywords)
                {
                    downloader.Search(strKeyword);
                }
            }
        }

        private void searchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void searchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSearchWorkerFinish();
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
                    else if (Work == Work.eDownloading)
                    {
                        downloadWorker.WorkerSupportsCancellation = true;
                        downloadWorker.CancelAsync();
                    }
                }
            }
        }

        private void lvwBooks_SelectedIndexChanged(object sender, EventArgs e)
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
            try
            {
                StreamWriter writer = new StreamWriter("Keywords.txt");
                foreach (string keyword in lbKeywords.Items)
                {
                    writer.WriteLine(keyword);
                }
                writer.Close();
            }
            catch (Exception ex) { }

            if (Downloaders.Instance.First())
            {
                BookStore downloader = Downloaders.Instance.Next();
                while (downloader != null)
                {
                    downloader.BookFound -= new BookStore.BookFoundEventHandler(OnFileFound);
                    downloader.QueryCancel -= new BookStore.QueryCancelEventHandler(IsCancel);
                    downloader.SearchStarted -= new BookStore.SearchStartedEventHandler(OnKeywordSearchStarted);

                    downloader = Downloaders.Instance.Next();
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (Work != Work.eIdle && Work != Work.eSearching)
                return;

            if (downloadWorker.IsBusy)
                return;

            if (Work == Work.eIdle)
            {
                Work = Work.eDownloading;
                downloadWorker.RunWorkerAsync(new object[] { lvwBooks.CheckedItems.Count });
            }
            else if(Work == Work.eDownloading)
            {
                if (downloadWorker.IsBusy)
                {
                    DialogResult res = MessageBox.Show("Do you want to abort current operation (downloading) ?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res != DialogResult.Yes)
                        return;

                    downloadWorker.CancelAsync();
                }
            }
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

        }

        private void downloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (lvwBooks.InvokeRequired)
            {
                Invoke(new System.ComponentModel.DoWorkEventHandler(downloadWorker_DoWork), new object[] { sender, e });
            }

            object[] arguments = e.Argument as object[];
            string url = string.Empty;
            string title = string.Empty;
            string location = string.Empty;
            int id = -1;
            object[] items = arguments[1] as object[];

            foreach (ListViewItem item in items)
            {
                id = item.Index;
                title = item.SubItems[1].Text;
                url = item.SubItems[2].Text;
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);

                    webClient.DownloadFileAsync(new Uri(url), title);
                }
                catch (Exception ex)
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

        private void lbKeywords_DoubleClick(object sender, EventArgs e)
        {
            frmInputKeyword frm = new frmInputKeyword();
            frm.Keyword = lbKeywords.SelectedItem as string;
            frm.Index = lbKeywords.SelectedIndex;

            if (frm.ShowDialog() != DialogResult.OK)
                return;

            lbKeywords.Items[frm.Index] = frm.Keyword;
        }
    }
}
