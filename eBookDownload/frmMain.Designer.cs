namespace eBookDownload
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
            this.btnClearKeywords = new System.Windows.Forms.Button();
            this.btnRemoveKeyword = new System.Windows.Forms.Button();
            this.btnAddKeyword = new System.Windows.Forms.Button();
            this.lbKeywords = new System.Windows.Forms.ListBox();
            this.radSelectedKeywords = new System.Windows.Forms.RadioButton();
            this.radInputKeyword = new System.Windows.Forms.RadioButton();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.culOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSearch = new System.Windows.Forms.Button();
            this.chbAutoDownload = new System.Windows.Forms.CheckBox();
            this.chbCheckUnCheckAll = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.cbbProviders.Location = new System.Drawing.Point(89, 6);
            this.cbbProviders.Name = "cbbProviders";
            this.cbbProviders.Size = new System.Drawing.Size(906, 21);
            this.cbbProviders.TabIndex = 1;
            this.cbbProviders.SelectedIndexChanged += new System.EventHandler(this.cbbProviders_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnClearKeywords);
            this.groupBox1.Controls.Add(this.btnRemoveKeyword);
            this.groupBox1.Controls.Add(this.btnAddKeyword);
            this.groupBox1.Controls.Add(this.lbKeywords);
            this.groupBox1.Controls.Add(this.radSelectedKeywords);
            this.groupBox1.Controls.Add(this.radInputKeyword);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Location = new System.Drawing.Point(15, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 169);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Books";
            // 
            // btnClearKeywords
            // 
            this.btnClearKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearKeywords.Enabled = false;
            this.btnClearKeywords.Location = new System.Drawing.Point(810, 110);
            this.btnClearKeywords.Name = "btnClearKeywords";
            this.btnClearKeywords.Size = new System.Drawing.Size(57, 23);
            this.btnClearKeywords.TabIndex = 4;
            this.btnClearKeywords.Text = "Clear";
            this.btnClearKeywords.UseVisualStyleBackColor = true;
            // 
            // btnRemoveKeyword
            // 
            this.btnRemoveKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveKeyword.Enabled = false;
            this.btnRemoveKeyword.Location = new System.Drawing.Point(811, 81);
            this.btnRemoveKeyword.Name = "btnRemoveKeyword";
            this.btnRemoveKeyword.Size = new System.Drawing.Size(56, 23);
            this.btnRemoveKeyword.TabIndex = 4;
            this.btnRemoveKeyword.Text = "Remove";
            this.btnRemoveKeyword.UseVisualStyleBackColor = true;
            // 
            // btnAddKeyword
            // 
            this.btnAddKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddKeyword.Enabled = false;
            this.btnAddKeyword.Location = new System.Drawing.Point(811, 52);
            this.btnAddKeyword.Name = "btnAddKeyword";
            this.btnAddKeyword.Size = new System.Drawing.Size(56, 23);
            this.btnAddKeyword.TabIndex = 4;
            this.btnAddKeyword.Text = "Add";
            this.btnAddKeyword.UseVisualStyleBackColor = true;
            // 
            // lbKeywords
            // 
            this.lbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKeywords.Enabled = false;
            this.lbKeywords.FormattingEnabled = true;
            this.lbKeywords.Location = new System.Drawing.Point(186, 52);
            this.lbKeywords.Name = "lbKeywords";
            this.lbKeywords.Size = new System.Drawing.Size(621, 108);
            this.lbKeywords.TabIndex = 3;
            // 
            // radSelectedKeywords
            // 
            this.radSelectedKeywords.AutoSize = true;
            this.radSelectedKeywords.Enabled = false;
            this.radSelectedKeywords.Location = new System.Drawing.Point(74, 52);
            this.radSelectedKeywords.Name = "radSelectedKeywords";
            this.radSelectedKeywords.Size = new System.Drawing.Size(104, 17);
            this.radSelectedKeywords.TabIndex = 2;
            this.radSelectedKeywords.Text = "Select Keywords";
            this.radSelectedKeywords.UseVisualStyleBackColor = true;
            this.radSelectedKeywords.CheckedChanged += new System.EventHandler(this.radSelectedKeywords_CheckedChanged);
            // 
            // radInputKeyword
            // 
            this.radInputKeyword.AutoSize = true;
            this.radInputKeyword.Checked = true;
            this.radInputKeyword.Enabled = false;
            this.radInputKeyword.Location = new System.Drawing.Point(74, 29);
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
            this.txtKeyword.Size = new System.Drawing.Size(682, 20);
            this.txtKeyword.TabIndex = 1;
            this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.culOrder,
            this.colTitle,
            this.colLink});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(15, 218);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(872, 236);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // culOrder
            // 
            this.culOrder.Text = "#";
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 400;
            // 
            // colLink
            // 
            this.colLink.Text = "URL";
            this.colLink.Width = 400;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(894, 71);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 31);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chbAutoDownload
            // 
            this.chbAutoDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAutoDownload.AutoSize = true;
            this.chbAutoDownload.Checked = true;
            this.chbAutoDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAutoDownload.Location = new System.Drawing.Point(896, 110);
            this.chbAutoDownload.Name = "chbAutoDownload";
            this.chbAutoDownload.Size = new System.Drawing.Size(99, 17);
            this.chbAutoDownload.TabIndex = 5;
            this.chbAutoDownload.Text = "Auto-Download";
            this.chbAutoDownload.UseVisualStyleBackColor = true;
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
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(15, 458);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(872, 18);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(894, 445);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(101, 31);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 479);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chbCheckUnCheckAll);
            this.Controls.Add(this.chbAutoDownload);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbbProviders);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(600, 360);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eBookDownload";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
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
        private System.Windows.Forms.Button btnAddKeyword;
        private System.Windows.Forms.Button btnClearKeywords;
        private System.Windows.Forms.Button btnRemoveKeyword;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chbAutoDownload;
        private System.Windows.Forms.ColumnHeader culOrder;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colLink;
        private System.Windows.Forms.CheckBox chbCheckUnCheckAll;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnStop;
    }
}

