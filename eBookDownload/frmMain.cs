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
using System.Diagnostics;

namespace eBookDownloader
{
    enum Work { eIdle = 0, eSearching, eDownloading }

    public partial class frmMain : Form
    {
        private Work Work { get; set; }
        private Dictionary<string, BookEventArg> _files = new Dictionary<string, BookEventArg>();
        private Dictionary<Control, bool> _controlStates = new Dictionary<Control, bool>();

        private bool CanOpenFile
        {
            get
            {
                if(lvwBooks.SelectedItems.Count==1)
                {
                    ListViewItem item = lvwBooks.SelectedItems[0];
                    BookEventArg book = item.Tag as BookEventArg;

                    if (book != null)
                    {
                        return File.Exists(book.Path);
                    }
                }

                return false;
            }
        }
        public frmMain()
        {
            InitializeComponent();
            searchWorker.WorkerSupportsCancellation = true;
        }

        private static bool IsChecked(ListViewItem item)
        {
            return item.Checked;
        }

        private void DisableControl(Control control)
        {
            _controlStates[control] = control.Enabled;
            control.Enabled = false;
        }

        private void RestoreControlState(Control control)
        {
            if (_controlStates.ContainsKey(control))
            {
                control.Enabled = _controlStates[control];
                _controlStates.Remove(control);
            }
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
            if (Providers.Instance.First())
            {
                Provider provider = Providers.Instance.Next();
                while (provider != null)
                {
                    cbbProviders.DisplayMember = "Name";
                    cbbProviders.ValueMember = "Instance";
                    cbbProviders.Items.Add(provider);
                    provider.BookFound += new Provider.BookFoundEventHandler(OnFileFound);
                    provider.QueryCancel += new Provider.QueryCancelEventHandler(IsCancel);
                    provider.BookDownloading += new Provider.BookDownloadingEventHandler(OnFileDownloading);
                    provider.BookDownloadCompleted += new Provider.BookDownloadCompletedEventHandler(OnFileDownloaded);
                    provider.SearchStarted += new Provider.SearchStartedEventHandler(OnKeywordSearchStarted);

                    provider = Providers.Instance.Next();
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
            txtWorkingDirectory.Text = System.IO.Directory.GetCurrentDirectory();
        }

        private string ReplaceSpecialCharacters(string input)
        {
            string str1 = input.Replace('\\', '_');
            string str2 = str1.Replace('*', '_');
            string str3 = str2.Replace('/', '_');
            string str4 = str3.Replace('<', '_');
            string str5 = str4.Replace('>', '_');
            string str6 = str5.Replace('?', '_');
            string str7 = str6.Replace(':', '_');
            string str8 = str7.Replace(';', '_');
            return str8;
        }

        private void OnFileFound(object sender, BookEventArg e)
        {
            if (sender == this)
            {
                string strDir = txtWorkingDirectory.Text;
                if (strDir.Substring(strDir.Length - 1, 1) != "\\")
                    strDir += "\\";

                string strExt = string.Empty;
                string strCategory = string.Empty;
                int nPos = e.URL.LastIndexOf(".");

                if (nPos > 0)
                {
                    strExt = e.URL.Substring(nPos + 1);
                }

                if (e.Group)
                    strCategory = e.Category;

                string strFullPath = strDir;

                if (strExt != string.Empty)
                {
                    if (strCategory != string.Empty)
                    {
                        strFullPath += strCategory;
                        strFullPath += "\\";
                    }
                    strFullPath += (ReplaceSpecialCharacters(e.Title) + "." + strExt);
                }
                else
                {
                    strFullPath += ReplaceSpecialCharacters(e.Title);
                }

                e.Path = strFullPath;

                lvwBooks.Items[e.Index].SubItems[3].Text = e.Path;
            }
            else
            {
                if (lvwBooks.InvokeRequired)
                {
                    this.Invoke(new Provider.BookFoundEventHandler(OnFileFound), new object[] { sender, e });
                }
                else
                {
                    ListViewItem item = lvwBooks.Items.Add((lvwBooks.Items.Count + 1).ToString());
                    item.ImageIndex = -1;
                    item.SubItems.Add(e.Title);
                    item.SubItems.Add(e.URL);
                    lvwBooks.EnsureVisible(item.Index);
                    if (!lblKeyword.Visible)
                    {
                        progressBar.Width = lblKeyword.Left - progressBar.Left;
                        lblKeyword.Visible = true;
                    }

                    string category = WebUtility.UrlDecode(WebUtility.UrlDecode(e.Category));
                    if (lblKeyword.Text != category)
                    {
                        lblKeyword.Text = category;
                        progressBar.Width = lblKeyword.Left - progressBar.Left;
                    }

                    e.Overwriten = chbOverwritenDownload.Checked;
                    e.Download = chbAutoDownload.Checked;
                    e.Group = chbGroupByKeyword.Checked;
                    e.Group = chbGroupByKeyword.Checked;

                    string strDir = txtWorkingDirectory.Text;
                    if (strDir.Substring(strDir.Length - 1, 1) != "\\")
                        strDir += "\\";

                    string strExt = string.Empty;
                    string strCategory = string.Empty;
                    int nPos = e.URL.LastIndexOf(".");
                    if (nPos > 0)
                    {
                        strExt = e.URL.Substring(nPos + 1);
                    }

                    if (e.Group)
                        strCategory = e.Category;

                    string strFullPath = strDir;

                    if (strExt != string.Empty)
                    {
                        if (strCategory != string.Empty)
                        {
                            strFullPath += strCategory;
                            strFullPath += "\\";
                        }
                        strFullPath += (ReplaceSpecialCharacters(e.Title) + "." + strExt);
                    }
                    else
                    {
                        strFullPath += ReplaceSpecialCharacters(e.Title);
                    }

                    e.Path = strFullPath;

                    item.SubItems.Add(e.Path);
                    item.Tag = e;

                    e.Index = item.Index;
                }
            }
        }

        private void OnFileDownloading(object sender, BookEventArg e)
        {
            if (lvwBooks.InvokeRequired)
            {
                this.Invoke(new Provider.BookDownloadingEventHandler(OnFileDownloading), new object[] { sender, e });
            }
            else
            {
                ListViewItem item = lvwBooks.Items[e.Index];
                item.SubItems[3].Text = e.Progress.ToString();
            }
        }

        private void OnFileDownloaded(object sender, BookEventArg e)
        {
            if (lvwBooks.InvokeRequired)
            {
                this.Invoke(new Provider.BookDownloadCompletedEventHandler(OnFileDownloaded), new object[] { sender, e });
            }
            else
            {
                ListViewItem item = lvwBooks.Items[e.Index];
                item.SubItems[3].Text = e.Path;
                if (e.Status == 0) //Success
                {
                    item.ImageIndex = 0;
                }
                else if (e.Status == -1) //Aborted
                {

                }
                else //Failed
                {
                    item.ImageIndex = 1;
                }
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
                this.Invoke(new Provider.SearchStartedEventHandler(OnKeywordSearchStarted), new object[] { keyword });
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

            Provider provider = cbbProviders.SelectedItem as Provider;
            if (Work == Work.eIdle)
            {
                if (null == provider)
                {
                    return;
                }                

                if (searchWorker.IsBusy)
                    return;

                Work = Work.eSearching;

                if (radInputKeyword.Checked)
                    searchWorker.RunWorkerAsync(new object[] { provider, txtKeyword.Text });
                else
                    searchWorker.RunWorkerAsync(new object[] { provider, lbKeywords.SelectedItems });
            }
            else if(Work== Work.eSearching)
            {
                if (searchWorker.IsBusy)
                {
                    DialogResult res = MessageBox.Show("Do you want to abort current operation (searching) ?", "Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res != DialogResult.Yes)
                        return;

                    provider.Cleanup();
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
                lvwBooks.InvokeRequired || chbCheckUnCheckAll.InvokeRequired ||
                btnBrowseWorkingDir.InvokeRequired || chbOverwritenDownload.InvokeRequired ||
                chbGroupByKeyword.InvokeRequired || lblKeyword.InvokeRequired)
            {
                this.Invoke(new Action(OnSearchWorkerStart), new object[] { });
            }
            else
            {
                lvwBooks.Items.Clear();
                chbCheckUnCheckAll.Checked = false;
                progressBar.Visible = true;
                DisableControl(chbAutoDownload);
                DisableControl(radSelectedKeywords);
                DisableControl(radInputKeyword);
                DisableControl(chbCheckUnCheckAll);
                DisableControl(cbbProviders);
                DisableControl(lbKeywords);
                DisableControl(txtKeyword);
                DisableControl(lvwBooks);
                DisableControl(btnBrowseWorkingDir);
                DisableControl(chbOverwritenDownload);
                DisableControl(chbGroupByKeyword);
                DisableControl(btnAddKeyword);
                DisableControl(btnRemoveKeyword);
                DisableControl(btnClearKeywords);
                progressBar.Width = lvwBooks.Width;
                chbCheckUnCheckAll.Visible = false;
                _files.Clear();

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
                lvwBooks.InvokeRequired || chbCheckUnCheckAll.InvokeRequired || 
                btnBrowseWorkingDir.InvokeRequired ||chbOverwritenDownload.InvokeRequired ||
                chbGroupByKeyword.InvokeRequired || lblKeyword.InvokeRequired)
            {
                this.Invoke(new Action(OnSearchWorkerFinish), new object[] { });
            }
            else
            {
                progressBar.Visible = false;
                RestoreControlState(chbAutoDownload);
                RestoreControlState(chbCheckUnCheckAll);
                RestoreControlState(cbbProviders);
                txtKeyword.Enabled = radInputKeyword.Checked;
                lbKeywords.Enabled = radSelectedKeywords.Checked;
                RestoreControlState(btnBrowseWorkingDir);
                RestoreControlState(chbOverwritenDownload);
                RestoreControlState(chbGroupByKeyword);
                RestoreControlState(btnAddKeyword);
                RestoreControlState(btnRemoveKeyword);
                RestoreControlState(btnClearKeywords);
                lblKeyword.Visible = false;
                lblKeyword.Text = string.Empty;
                chbCheckUnCheckAll.Visible = true;
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
            Provider provider = parameters[0] as Provider;

            if (parameters[1] is string)
            {
                string strKeyword = parameters[1] as string;
                provider.Search(strKeyword);
            }
            else
            {
                ListBox.SelectedObjectCollection keywords = parameters[1] as ListBox.SelectedObjectCollection;
                foreach (string strKeyword in keywords)
                {
                    provider.Search(strKeyword);
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
            int idx = lbKeywords.SelectedIndex;
            lbKeywords.Items.RemoveAt(idx);
            if (lbKeywords.Items.Count > idx)
                lbKeywords.SelectedIndex = idx;

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

            if (Providers.Instance.First())
            {
                Provider provider = Providers.Instance.Next();
                while (provider != null)
                {
                    provider.BookFound -= new Provider.BookFoundEventHandler(OnFileFound);
                    provider.BookDownloading -= new Provider.BookDownloadingEventHandler(OnFileDownloading);
                    provider.BookDownloadCompleted -= new Provider.BookDownloadCompletedEventHandler(OnFileDownloaded);
                    provider.QueryCancel -= new Provider.QueryCancelEventHandler(IsCancel);
                    provider.SearchStarted -= new Provider.SearchStartedEventHandler(OnKeywordSearchStarted);

                    provider = Providers.Instance.Next();
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
            BookEventArg book = e.UserState as BookEventArg;
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BookEventArg book = e.UserState as BookEventArg;
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

        private void btnBrowseWorkingDir_Click(object sender, EventArgs e)
        {
            
            folderBrowserDialog.SelectedPath = txtWorkingDirectory.Text;
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            if(txtWorkingDirectory.Text != folderBrowserDialog.SelectedPath)
            {
                txtWorkingDirectory.Text = folderBrowserDialog.SelectedPath;

                foreach(ListViewItem item in lvwBooks.Items)
                {
                    BookEventArg book = item.Tag as BookEventArg;
                    if (book != null)
                        OnFileFound(this, book);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwBooks.SelectedItems.Count <= 0)
            {
                return;
            }

            ListViewItem item = lvwBooks.SelectedItems[0];
            BookEventArg book = item.Tag as BookEventArg;

            Process.Start(book.Path);
        }

        private void explorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwBooks.SelectedItems.Count<=0)
            {
                return;
            }

            ListViewItem item = lvwBooks.SelectedItems[0];
            BookEventArg book = item.Tag as BookEventArg;

            Process.Start("EXPLORER", "/SELECT," + book.Path);
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private void mnuBookPopupMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = lvwBooks.SelectedIndices.Count <= 0;
            openToolStripMenuItem.Visible = CanOpenFile;
            explorerToolStripMenuItem.Visible = CanOpenFile;
        }

        private void chbGroupByKeyword_CheckedChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem item in lvwBooks.Items)
            {
                BookEventArg book = item.Tag as BookEventArg;
                if (book != null)
                {
                    book.Group = chbGroupByKeyword.Checked;
                    OnFileFound(this, book);
                }
            }
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            progressBar.Width = lblKeyword.Left - progressBar.Left;
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwBooks.SelectedItems.Count <= 0)
            {
                return;
            }

            ListViewItem item = lvwBooks.SelectedItems[0];
            BookEventArg book = item.Tag as BookEventArg;

            Process.Start(book.URL);
        }

        private void copyTitleMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwBooks.SelectedItems.Count <= 0)
            {
                return;
            }

            ListViewItem item = lvwBooks.SelectedItems[0];
            BookEventArg book = item.Tag as BookEventArg;
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(book.Title);
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private void copyLinkMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwBooks.SelectedItems.Count <= 0)
            {
                return;
            }

            ListViewItem item = lvwBooks.SelectedItems[0];
            BookEventArg book = item.Tag as BookEventArg;
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(book.URL);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}
