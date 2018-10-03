using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBookDownloader
{
    public partial class ListViewEx : ListView
    {
        public ListViewEx()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }
    }
}
