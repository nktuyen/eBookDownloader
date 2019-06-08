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
            this.components = new System.ComponentModel.Container();
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
            this.mnuBookPopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTitleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.explorerFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seperator1MenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chbCheckUnCheckAll = new System.Windows.Forms.CheckBox();
            this.searchWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.downloadWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbGroupByKeyword = new System.Windows.Forms.CheckBox();
            this.btnBrowseWorkingDir = new System.Windows.Forms.Button();
            this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbOverwritenDownload = new System.Windows.Forms.CheckBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lvwBooks = new eBookDownloader.ListViewEx();
            this.culOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.mnuBookPopupMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Provider:";
            // 
            // cbbProviders
            // 
            this.cbbProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProviders.FormattingEnabled = true;
            this.cbbProviders.Location = new System.Drawing.Point(79, 6);
            this.cbbProviders.Name = "cbbProviders";
            this.cbbProviders.Size = new System.Drawing.Size(821, 21);
            this.cbbProviders.TabIndex = 1;
            this.cbbProviders.SelectedIndexChanged += new System.EventHandler(this.cbbProviders_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chbAutoDownload);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnClearKeywords);
            this.groupBox1.Controls.Add(this.btnRemoveKeyword);
            this.groupBox1.Controls.Add(this.btnAddKeyword);
            this.groupBox1.Controls.Add(this.lbKeywords);
            this.groupBox1.Controls.Add(this.radSelectedKeywords);
            this.groupBox1.Controls.Add(this.radInputKeyword);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Location = new System.Drawing.Point(15, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 179);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // chbAutoDownload
            // 
            this.chbAutoDownload.AutoSize = true;
            this.chbAutoDownload.Location = new System.Drawing.Point(292, 148);
            this.chbAutoDownload.Name = "chbAutoDownload";
            this.chbAutoDownload.Size = new System.Drawing.Size(131, 17);
            this.chbAutoDownload.TabIndex = 7;
            this.chbAutoDownload.Text = "Download immediately";
            this.chbAutoDownload.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(185, 140);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 31);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClearKeywords
            // 
            this.btnClearKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearKeywords.Enabled = false;
            this.btnClearKeywords.Location = new System.Drawing.Point(362, 110);
            this.btnClearKeywords.Name = "btnClearKeywords";
            this.btnClearKeywords.Size = new System.Drawing.Size(57, 23);
            this.btnClearKeywords.TabIndex = 4;
            this.btnClearKeywords.Text = "Clear";
            this.btnClearKeywords.UseVisualStyleBackColor = true;
            this.btnClearKeywords.Click += new System.EventHandler(this.btnClearKeywords_Click);
            // 
            // btnRemoveKeyword
            // 
            this.btnRemoveKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveKeyword.Enabled = false;
            this.btnRemoveKeyword.Location = new System.Drawing.Point(363, 81);
            this.btnRemoveKeyword.Name = "btnRemoveKeyword";
            this.btnRemoveKeyword.Size = new System.Drawing.Size(56, 23);
            this.btnRemoveKeyword.TabIndex = 4;
            this.btnRemoveKeyword.Text = "Remove";
            this.btnRemoveKeyword.UseVisualStyleBackColor = true;
            this.btnRemoveKeyword.Click += new System.EventHandler(this.btnRemoveKeyword_Click);
            // 
            // btnAddKeyword
            // 
            this.btnAddKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddKeyword.Enabled = false;
            this.btnAddKeyword.Location = new System.Drawing.Point(363, 52);
            this.btnAddKeyword.Name = "btnAddKeyword";
            this.btnAddKeyword.Size = new System.Drawing.Size(56, 23);
            this.btnAddKeyword.TabIndex = 4;
            this.btnAddKeyword.Text = "Add";
            this.btnAddKeyword.UseVisualStyleBackColor = true;
            this.btnAddKeyword.Click += new System.EventHandler(this.btnAddKeyword_Click);
            // 
            // lbKeywords
            // 
            this.lbKeywords.AllowDrop = true;
            this.lbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKeywords.Enabled = false;
            this.lbKeywords.FormattingEnabled = true;
            this.lbKeywords.Location = new System.Drawing.Point(185, 52);
            this.lbKeywords.Name = "lbKeywords";
            this.lbKeywords.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbKeywords.Size = new System.Drawing.Size(170, 82);
            this.lbKeywords.TabIndex = 3;
            this.lbKeywords.SelectedIndexChanged += new System.EventHandler(this.lbKeywords_SelectedIndexChanged);
            this.lbKeywords.DragDrop += new System.Windows.Forms.DragEventHandler(this.LbKeywords_DragDrop);
            this.lbKeywords.DragEnter += new System.Windows.Forms.DragEventHandler(this.LbKeywords_DragEnter);
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
            this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyword.Enabled = false;
            this.txtKeyword.Location = new System.Drawing.Point(185, 28);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(234, 20);
            this.txtKeyword.TabIndex = 1;
            this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
            // 
            // mnuBookPopupMenu
            // 
            this.mnuBookPopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTitleMenuItem,
            this.copyLinkMenuItem,
            this.openLinkMenuItem,
            this.openFileMenuItem,
            this.explorerFileMenuItem,
            this.seperator1MenuItem,
            this.downloadToolStripMenuItem,
            this.exportListMenuItem});
            this.mnuBookPopupMenu.Name = "mnuBookPopupMenu";
            this.mnuBookPopupMenu.Size = new System.Drawing.Size(215, 164);
            this.mnuBookPopupMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mnuBookPopupMenu_Opening);
            // 
            // copyTitleMenuItem
            // 
            this.copyTitleMenuItem.Name = "copyTitleMenuItem";
            this.copyTitleMenuItem.Size = new System.Drawing.Size(214, 22);
            this.copyTitleMenuItem.Text = "&Copy Title";
            this.copyTitleMenuItem.Click += new System.EventHandler(this.copyTitleMenuItem_Click);
            // 
            // copyLinkMenuItem
            // 
            this.copyLinkMenuItem.Name = "copyLinkMenuItem";
            this.copyLinkMenuItem.Size = new System.Drawing.Size(214, 22);
            this.copyLinkMenuItem.Text = "Copy &Link";
            this.copyLinkMenuItem.Click += new System.EventHandler(this.copyLinkMenuItem_Click);
            // 
            // openLinkMenuItem
            // 
            this.openLinkMenuItem.Name = "openLinkMenuItem";
            this.openLinkMenuItem.Size = new System.Drawing.Size(214, 22);
            this.openLinkMenuItem.Text = "Ope&n Link";
            this.openLinkMenuItem.Click += new System.EventHandler(this.browseToolStripMenuItem_Click);
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.Size = new System.Drawing.Size(214, 22);
            this.openFileMenuItem.Text = "&Open File";
            this.openFileMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // explorerFileMenuItem
            // 
            this.explorerFileMenuItem.Name = "explorerFileMenuItem";
            this.explorerFileMenuItem.Size = new System.Drawing.Size(214, 22);
            this.explorerFileMenuItem.Text = "Open in Windows &Explorer";
            this.explorerFileMenuItem.Click += new System.EventHandler(this.explorerToolStripMenuItem_Click);
            // 
            // seperator1MenuItem
            // 
            this.seperator1MenuItem.Name = "seperator1MenuItem";
            this.seperator1MenuItem.Size = new System.Drawing.Size(211, 6);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.downloadToolStripMenuItem.Text = "&Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // exportListMenuItem
            // 
            this.exportListMenuItem.Name = "exportListMenuItem";
            this.exportListMenuItem.Size = new System.Drawing.Size(214, 22);
            this.exportListMenuItem.Text = "E&xport List";
            this.exportListMenuItem.Click += new System.EventHandler(this.exportListMenuItem_Click);
            // 
            // chbCheckUnCheckAll
            // 
            this.chbCheckUnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbCheckUnCheckAll.AutoSize = true;
            this.chbCheckUnCheckAll.Location = new System.Drawing.Point(15, 542);
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
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(15, 540);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(885, 18);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // downloadWorker
            // 
            this.downloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.downloadWorker_DoWork);
            this.downloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.downloadWorker_ProgressChanged);
            this.downloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloadWorker_RunWorkerCompleted);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chbGroupByKeyword);
            this.groupBox2.Controls.Add(this.btnBrowseWorkingDir);
            this.groupBox2.Controls.Add(this.txtWorkingDirectory);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chbOverwritenDownload);
            this.groupBox2.Controls.Add(this.btnDownload);
            this.groupBox2.Location = new System.Drawing.Point(459, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 179);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download Options";
            // 
            // chbGroupByKeyword
            // 
            this.chbGroupByKeyword.AutoSize = true;
            this.chbGroupByKeyword.Location = new System.Drawing.Point(42, 86);
            this.chbGroupByKeyword.Name = "chbGroupByKeyword";
            this.chbGroupByKeyword.Size = new System.Drawing.Size(353, 17);
            this.chbGroupByKeyword.TabIndex = 15;
            this.chbGroupByKeyword.Text = "Group downloaded files into directories according searched keywords";
            this.chbGroupByKeyword.UseVisualStyleBackColor = true;
            this.chbGroupByKeyword.CheckedChanged += new System.EventHandler(this.chbGroupByKeyword_CheckedChanged);
            // 
            // btnBrowseWorkingDir
            // 
            this.btnBrowseWorkingDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseWorkingDir.Location = new System.Drawing.Point(414, 26);
            this.btnBrowseWorkingDir.Name = "btnBrowseWorkingDir";
            this.btnBrowseWorkingDir.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseWorkingDir.TabIndex = 14;
            this.btnBrowseWorkingDir.Text = "...";
            this.btnBrowseWorkingDir.UseVisualStyleBackColor = true;
            this.btnBrowseWorkingDir.Click += new System.EventHandler(this.btnBrowseWorkingDir_Click);
            // 
            // txtWorkingDirectory
            // 
            this.txtWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkingDirectory.Location = new System.Drawing.Point(144, 28);
            this.txtWorkingDirectory.Name = "txtWorkingDirectory";
            this.txtWorkingDirectory.Size = new System.Drawing.Size(267, 20);
            this.txtWorkingDirectory.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Working Directory:";
            // 
            // chbOverwritenDownload
            // 
            this.chbOverwritenDownload.AutoSize = true;
            this.chbOverwritenDownload.Location = new System.Drawing.Point(42, 58);
            this.chbOverwritenDownload.Name = "chbOverwritenDownload";
            this.chbOverwritenDownload.Size = new System.Drawing.Size(136, 17);
            this.chbOverwritenDownload.TabIndex = 11;
            this.chbOverwritenDownload.Text = "Overwriten existing files";
            this.chbOverwritenDownload.UseVisualStyleBackColor = true;
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(195, 140);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(101, 31);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblKeyword
            // 
            this.lblKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(855, 543);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(42, 13);
            this.lblKeyword.TabIndex = 12;
            this.lblKeyword.Text = "AAAAA";
            this.lblKeyword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblKeyword.Visible = false;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.FileName = "eBookDownloader.xml";
            this.saveFileDialog.Filter = "All Files(*.*)|*.*|XML Files(*.xml)|*.xml";
            this.saveFileDialog.FilterIndex = 2;
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // lvwBooks
            // 
            this.lvwBooks.AllowDrop = true;
            this.lvwBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwBooks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwBooks.CheckBoxes = true;
            this.lvwBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.culOrder,
            this.colTitle,
            this.colLink,
            this.colPath});
            this.lvwBooks.ContextMenuStrip = this.mnuBookPopupMenu;
            this.lvwBooks.FullRowSelect = true;
            this.lvwBooks.GridLines = true;
            this.lvwBooks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwBooks.HideSelection = false;
            this.lvwBooks.Location = new System.Drawing.Point(15, 233);
            this.lvwBooks.Name = "lvwBooks";
            this.lvwBooks.ShowItemToolTips = true;
            this.lvwBooks.Size = new System.Drawing.Size(885, 302);
            this.lvwBooks.TabIndex = 3;
            this.lvwBooks.UseCompatibleStateImageBehavior = false;
            this.lvwBooks.View = System.Windows.Forms.View.Details;
            this.lvwBooks.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwBooks_ItemChecked);
            this.lvwBooks.SelectedIndexChanged += new System.EventHandler(this.lvwBooks_SelectedIndexChanged);
            this.lvwBooks.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwBooks_DragDrop);
            this.lvwBooks.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwBooks_DragEnter);
            this.lvwBooks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwBooks_MouseClick);
            // 
            // culOrder
            // 
            this.culOrder.Text = "#";
            this.culOrder.Width = 70;
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
            // colPath
            // 
            this.colPath.Text = "Local Path";
            this.colPath.Width = 400;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 561);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lvwBooks);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbbProviders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.chbCheckUnCheckAll);
            this.MinimumSize = new System.Drawing.Size(928, 444);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eBookDownloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mnuBookPopupMenu.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private ListViewEx lvwBooks;
        private System.Windows.Forms.ColumnHeader culOrder;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colLink;
        private System.Windows.Forms.CheckBox chbCheckUnCheckAll;
        private System.ComponentModel.BackgroundWorker searchWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker downloadWorker;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.Button btnClearKeywords;
        private System.Windows.Forms.Button btnRemoveKeyword;
        private System.Windows.Forms.Button btnAddKeyword;
        private System.Windows.Forms.CheckBox chbAutoDownload;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckBox chbOverwritenDownload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWorkingDirectory;
        private System.Windows.Forms.Button btnBrowseWorkingDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ContextMenuStrip mnuBookPopupMenu;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem explorerFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator seperator1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbGroupByKeyword;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.ToolStripMenuItem openLinkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTitleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLinkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportListMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

