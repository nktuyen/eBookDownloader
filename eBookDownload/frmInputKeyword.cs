using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBookDownloader
{
    public partial class frmInputKeyword : Form
    {
        public string Keyword { get; set; }
        public int Index { get; set; }
        public frmInputKeyword()
        {
            InitializeComponent();
            Index = -1;
            Keyword = string.Empty;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            if (txtKeyword == this.ActiveControl)
            {
                this.Keyword = txtKeyword.Text;
            }
        }

        private void frmInputKeyword_Load(object sender, EventArgs e)
        {
            txtKeyword.Text = Keyword;
        }
    }
}
