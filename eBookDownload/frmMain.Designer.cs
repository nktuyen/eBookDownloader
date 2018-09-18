namespace eBookDownloader
{
    partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbbProviders = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbAutoDownload = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearKeywords = new System.Windows.Forms.Button();
            this.btnRemoveKeyword = new System.Windows.Forms.Button();
            this.btnAddKeyword = new System.Windows.Forms.Button();
            this.lbKeywords = new System.Windows.Forms.ListBox();
            this.radSelectedKeywords = new System.Windows.Forms.RadioButton();
            this.radInputKeyword = new System.Windows.Forms.RadioButton();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lvwBooks = new System.Windows.Forms.ListView();
            this.culOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chbCheckUnCheckAll = new System.Windows.Forms.CheckBox();
            this.searchWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnDownload = new System.Windows.Forms.Button();
            this.downloadWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            // 
            // cbbProviders
            // 
            this.cbbProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProviders.FormattingEnabled = true;
            this.cbbProviders.Location = new System.Drawing.Point(89, 6);
            this.cbbProviders.Name = "cbbProviders";
            this.cbbProviders.Size = new System.Drawing.Size(435, 21);
            this.cbbProviders.TabIndex = 1;
            this.cbbProviders.SelectedIndexChanged += new System.EventHandler(this.cbbProviders_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbAutoDownload);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnClearKeywords);
            this.groupBox1.Controls.Add(this.btnRemoveKeyword);
            this.groupBox1.Controls.Add(this.btnAddKeyword);
            this.groupBox1.Controls.Add(this.lbKeywords);
            this.groupBox1.Controls.Add(this.radSelectedKeywords);
            this.groupBox1.Controls.Add(this.radInputKeyword);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Location = new System.Drawing.Point(15, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 210);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // chbAutoDownload
            // 
            this.chbAutoDownload.AutoSize = true;
            this.chbAutoDownload.Location = new System.Drawing.Point(64, 129);
            this.chbAutoDownload.Name = "chbAutoDownload";
            this.chbAutoDownload.Size = new System.Drawing.Size(74, 17);
            this.chbAutoDownload.TabIndex = 7;
            this.chbAutoDownload.Text = "Download";
            this.chbAutoDownload.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(64, 92);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 31);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClearKeywords
            // 
            this.btnClearKeywords.Enabled = false;
            this.btnClearKeywords.Location = new System.Drawing.Point(433, 110);
            this.btnClearKeywords.Name = "btnClearKeywords";
            this.btnClearKeywords.Size = new System.Drawing.Size(57, 23);
            this.btnClearKeywords.TabIndex = 4;
            this.btnClearKeywords.Text = "Clear";
            this.btnClearKeywords.UseVisualStyleBackColor = true;
            this.btnClearKeywords.Click += new System.EventHandler(this.btnClearKeywords_Click);
            // 
            // btnRemoveKeyword
            // 
            this.btnRemoveKeyword.Enabled = false;
            this.btnRemoveKeyword.Location = new System.Drawing.Point(434, 81);
            this.btnRemoveKeyword.Name = "btnRemoveKeyword";
            this.btnRemoveKeyword.Size = new System.Drawing.Size(56, 23);
            this.btnRemoveKeyword.TabIndex = 4;
            this.btnRemoveKeyword.Text = "Remove";
            this.btnRemoveKeyword.UseVisualStyleBackColor = true;
            this.btnRemoveKeyword.Click += new System.EventHandler(this.btnRemoveKeyword_Click);
            // 
            // btnAddKeyword
            // 
            this.btnAddKeyword.Enabled = false;
            this.btnAddKeyword.Location = new System.Drawing.Point(434, 52);
            this.btnAddKeyword.Name = "btnAddKeyword";
            this.btnAddKeyword.Size = new System.Drawing.Size(56, 23);
            this.btnAddKeyword.TabIndex = 4;
            this.btnAddKeyword.Text = "Add";
            this.btnAddKeyword.UseVisualStyleBackColor = true;
            this.btnAddKeyword.Click += new System.EventHandler(this.btnAddKeyword_Click);
            // 
            // lbKeywords
            // 
            this.lbKeywords.Enabled = false;
            this.lbKeywords.FormattingEnabled = true;
            this.lbKeywords.Location = new System.Drawing.Point(185, 52);
            this.lbKeywords.Name = "lbKeywords";
            this.lbKeywords.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbKeywords.Size = new System.Drawing.Size(241, 108);
            this.lbKeywords.TabIndex = 3;
            this.lbKeywords.SelectedIndexChanged += new System.EventHandler(this.lbKeywords_SelectedIndexChanged);
            this.lbKeywords.DoubleClick += new System.EventHandler(this.lbKeywords_DoubleClick);
            // 
            // radSelectedKeywords
            // 
            this.radSelectedKeywords.AutoSize = true;
            this.radSelectedKeywords.Enabled = false;
            this.radSelectedKeywords.Location = new System.Drawing.Point(64, 52);
            this.radSelectedKeywords.Name = "radSelectedKeywords";
            this.radSelectedKeywords.Size = new System.Drawing.Size(116, 17);
            this.radSelectedKeywords.TabIndex = 2;
            this.radSelectedKeywords.Text = "Selected Keywords";
            this.radSelectedKeywords.UseVisualStyleBackColor = true;
            this.radSelectedKeywords.CheckedChanged += new System.EventHandler(this.radSelectedKeywords_CheckedChanged);
            // 
            // radInputKeyword
            // 
            this.radInputKeyword.AutoSize = true;
            this.radInputKeyword.Checked = true;
            this.radInputKeyword.Enabled = false;
            this.radInputKeyword.Location = new System.Drawing.Point(64, 29);
            this.radInputKeyword.Name = "radInputKeyword";
            this.radInputKeyword.Size = new System.Drawing.Size(93, 17);
            this.radInputKeyword.TabIndex = 2;
            this.radInputKeyword.TabStop = true;
            this.radInputKeyword.Text = "Enter keyword";
            this.radInputKeyword.UseVisualStyleBackColor = true;
            this.radInputKeyword.CheckedChanged += new System.EventHandler(this.radInputKeyword_CheckedChanged);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Enabled = false;
            this.txtKeyword.Location = new System.Drawing.Point(185, 28);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(305, 20);
            this.txtKeyword.TabIndex = 1;
            this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
            // 
            // lvwBooks
            // 
            this.lvwBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwBooks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwBooks.CheckBoxes = true;
            this.lvwBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.culOrder,
            this.colTitle,
            this.colLink,
            this.colStatus});
            this.lvwBooks.FullRowSelect = true;
            this.lvwBooks.GridLines = true;
            this.lvwBooks.Location = new System.Drawing.Point(15, 259);
            this.lvwBooks.Name = "lvwBooks";
            this.lvwBooks.ShowItemToolTips = true;
            this.lvwBooks.Size = new System.Drawing.Size(872, 195);
            this.lvwBooks.TabIndex = 3;
            this.lvwBooks.UseCompatibleStateImageBehavior = false;
            this.lvwBooks.View = System.Windows.Forms.View.Details;
            this.lvwBooks.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwBooks_ItemChecked);
            this.lvwBooks.SelectedIndexChanged += new System.EventHandler(this.lvwBooks_SelectedIndexChanged);
            // 
            // culOrder
            // 
            this.culOrder.Text = "#";
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 360;
            // 
            // colLink
            // 
            this.colLink.Text = "URL";
            this.colLink.Width = 360;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 120;
            // 
            // chbCheckUnCheckAll
            // 
            this.chbCheckUnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbCheckUnCheckAll.AutoSize = true;
            this.chbCheckUnCheckAll.Location = new System.Drawing.Point(15, 460);
            this.chbCheckUnCheckAll.Name = "chbCheckUnCheckAll";
            this.chbCheckUnCheckAll.Size = new System.Drawing.Size(120, 17);
            this.chbCheckUnCheckAll.TabIndex = 6;
            this.chbCheckUnCheckAll.Text = "Check/Uncheck All";
            this.chbCheckUnCheckAll.UseVisualStyleBackColor = true;
            this.chbCheckUnCheckAll.CheckedChanged += new System.EventHandler(this.chbCheckUnCheckAll_CheckedChanged);
            // 
            // searchWorker
            // 
            this.searchWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.searchWorker_DoWork);
            this.searchWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.searchWorker_ProgressChanged);
            this.searchWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.searchWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(15, 458);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(872, 18);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(893, 298);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(101, 31);
            this.btnDownload.TabIndex = 9;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // downloadWorker
            // 
            this.downloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.downloadWorker_DoWork);
            this.downloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.downloadWorker_ProgressChanged);
            this.downloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloadWorker_RunWorkerCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 479);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.chbCheckUnCheckAll);
            this.Controls.Add(this.lvwBooks);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbbProviders);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(600, 360);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eBookDownloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbProviders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radInputKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.RadioButton radSelectedKeywords;
        private System.Windows.Forms.ListBox lbKeywords;
        private System.Windows.Forms.ListView lvwBooks;
        private System.Windows.Forms.ColumnHeader culOrder;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colLink;
        private System.Windows.Forms.CheckBox chbCheckUnCheckAll;
        private System.ComponentModel.BackgroundWorker searchWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnDownload;
        private System.ComponentModel.BackgroundWorker downloadWorker;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Button btnClearKeywords;
        private System.Windows.Forms.Button btnRemoveKeyword;
        private System.Windows.Forms.Button btnAddKeyword;
        private System.Windows.Forms.CheckBox chbAutoDownload;
        private System.Windows.Forms.Button btnSearch;
    }
}

