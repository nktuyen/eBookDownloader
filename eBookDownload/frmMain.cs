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
            ListViewItem item = listView1.Items.Add((listView1.Items.Count + 1).ToString());
            item.SubItems.Add(e.Title);
            item.SubItems.Add(e.URL);
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
            Dictionary<string,string> books = downloader.Download(txtKeyword.Text);
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
    }
}
