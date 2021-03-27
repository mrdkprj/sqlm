namespace MasudaManager.Views
{
    partial class MainMenuView
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuView));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btConnect = new System.Windows.Forms.ToolStripButton();
            this.btDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btAddTab = new System.Windows.Forms.ToolStripButton();
            this.btOpenText = new System.Windows.Forms.ToolStripButton();
            this.btSaveText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btExecuteSql = new System.Windows.Forms.ToolStripButton();
            this.btCancelSql = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.btSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btEditResult = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btPreference = new System.Windows.Forms.ToolStripButton();
            this.baseSplitContainer = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ssZoomRatio = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssCaps = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseSplitContainer)).BeginInit();
            this.baseSplitContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btConnect,
            this.btDisconnect,
            this.toolStripSeparator5,
            this.btAddTab,
            this.btOpenText,
            this.btSaveText,
            this.toolStripSeparator1,
            this.btExecuteSql,
            this.btCancelSql,
            this.toolStripSeparator2,
            this.btExport,
            this.btSearch,
            this.toolStripSeparator4,
            this.btEditResult,
            this.toolStripSeparator8,
            this.btPreference});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(939, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // connectToolStrip
            // 
            this.btConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btConnect.Image = ((System.Drawing.Image)(resources.GetObject("connectToolStrip.Image")));
            this.btConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btConnect.Name = "connectToolStrip";
            this.btConnect.Size = new System.Drawing.Size(23, 22);
            this.btConnect.Text = "Connect";
            this.btConnect.ToolTipText = "Connect (Ctrl + D)";
            this.btConnect.Click += new System.EventHandler(this.connectToolStrip_Click);
            // 
            // disconnToolStrip
            // 
            this.btDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("disconnToolStrip.Image")));
            this.btDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDisconnect.Name = "disconnToolStrip";
            this.btDisconnect.Size = new System.Drawing.Size(23, 22);
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.Click += new System.EventHandler(this.disconnToolStrip_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // addTabToolStrip
            // 
            this.btAddTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAddTab.Image = ((System.Drawing.Image)(resources.GetObject("addTabToolStrip.Image")));
            this.btAddTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAddTab.Name = "addTabToolStrip";
            this.btAddTab.Size = new System.Drawing.Size(23, 22);
            this.btAddTab.Text = "New tab";
            this.btAddTab.ToolTipText = "New tab(Ctrl + N)";
            this.btAddTab.Click += new System.EventHandler(this.addTabToolStrip_Click);
            // 
            // openTextToolStrip
            // 
            this.btOpenText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btOpenText.Image = ((System.Drawing.Image)(resources.GetObject("openTextToolStrip.Image")));
            this.btOpenText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btOpenText.Name = "openTextToolStrip";
            this.btOpenText.Size = new System.Drawing.Size(23, 22);
            this.btOpenText.Text = "Open file";
            this.btOpenText.ToolTipText = "Open file(Ctrl + O)";
            this.btOpenText.Click += new System.EventHandler(this.openTextToolStrip_Click);
            // 
            // saveTextToolStrip
            // 
            this.btSaveText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveText.Image = ((System.Drawing.Image)(resources.GetObject("saveTextToolStrip.Image")));
            this.btSaveText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveText.Name = "saveTextToolStrip";
            this.btSaveText.Size = new System.Drawing.Size(23, 22);
            this.btSaveText.Text = "Save text";
            this.btSaveText.ToolTipText = "Save text(Ctrl + S)";
            this.btSaveText.Click += new System.EventHandler(this.saveTextToolStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // executeToolStrip
            // 
            this.btExecuteSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExecuteSql.Image = ((System.Drawing.Image)(resources.GetObject("executeToolStrip.Image")));
            this.btExecuteSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExecuteSql.Name = "executeToolStrip";
            this.btExecuteSql.Size = new System.Drawing.Size(23, 22);
            this.btExecuteSql.Text = "Execute SQL";
            this.btExecuteSql.ToolTipText = "Execute SQL(Ctrl + A)";
            this.btExecuteSql.Click += new System.EventHandler(this.executeToolStrip_Click);
            // 
            // abortToolStrip
            // 
            this.btCancelSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCancelSql.Image = ((System.Drawing.Image)(resources.GetObject("abortToolStrip.Image")));
            this.btCancelSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCancelSql.Name = "abortToolStrip";
            this.btCancelSql.Size = new System.Drawing.Size(23, 22);
            this.btCancelSql.Text = "Cacnel";
            this.btCancelSql.ToolTipText = "Cancel(Esc)";
            this.btCancelSql.Click += new System.EventHandler(this.abortToolStrip_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btExportToolStrip
            // 
            this.btExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExportToolStrip.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExportToolStrip";
            this.btExport.Size = new System.Drawing.Size(23, 22);
            this.btExport.Text = "Export";
            this.btExport.Click += new System.EventHandler(this.btExportToolStrip_Click);
            // 
            // searchToolStrip
            // 
            this.btSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSearch.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStrip.Image")));
            this.btSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSearch.Name = "searchToolStrip";
            this.btSearch.Size = new System.Drawing.Size(23, 22);
            this.btSearch.Text = "Search";
            this.btSearch.ToolTipText = "Search (Ctrl + F)";
            this.btSearch.Click += new System.EventHandler(this.searchToolStrip_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // sqlgridToolStrip
            // 
            this.btEditResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btEditResult.Image = ((System.Drawing.Image)(resources.GetObject("sqlgridToolStrip.Image")));
            this.btEditResult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btEditResult.Name = "sqlgridToolStrip";
            this.btEditResult.Size = new System.Drawing.Size(23, 22);
            this.btEditResult.Text = "EditGrid";
            this.btEditResult.Click += new System.EventHandler(this.sqlgridToolStrip_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // settingToolStrip
            // 
            this.btPreference.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btPreference.Image = ((System.Drawing.Image)(resources.GetObject("settingToolStrip.Image")));
            this.btPreference.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btPreference.Name = "settingToolStrip";
            this.btPreference.Size = new System.Drawing.Size(23, 22);
            this.btPreference.Text = "Preference";
            this.btPreference.Click += new System.EventHandler(this.settingToolStrip_Click);
            // 
            // baseSplitContainer
            // 
            this.baseSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.baseSplitContainer.Name = "baseSplitContainer";
            this.baseSplitContainer.Size = new System.Drawing.Size(939, 522);
            this.baseSplitContainer.SplitterDistance = 734;
            this.baseSplitContainer.TabIndex = 1;
            this.baseSplitContainer.TabStop = false;
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.ssProgressBar,
            this.ssZoomRatio,
            this.ssInfo,
            this.ssCaps,
            this.ssNum,
            this.ssMode});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 547);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(939, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 19);
            this.lblStatus.Text = "Ready";
            // 
            // ssProgressBar
            // 
            this.ssProgressBar.Name = "ssProgressBar";
            this.ssProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // ssZoomRatio
            // 
            this.ssZoomRatio.AutoSize = false;
            this.ssZoomRatio.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssZoomRatio.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.ssZoomRatio.Name = "ssZoomRatio";
            this.ssZoomRatio.Size = new System.Drawing.Size(39, 18);
            this.ssZoomRatio.Spring = true;
            this.ssZoomRatio.Text = "100%";
            // 
            // ssInfo
            // 
            this.ssInfo.AutoSize = false;
            this.ssInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Size = new System.Drawing.Size(114, 18);
            this.ssInfo.Spring = true;
            this.ssInfo.Text = "info";
            // 
            // ssCaps
            // 
            this.ssCaps.AutoSize = false;
            this.ssCaps.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssCaps.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.ssCaps.Name = "ssCaps";
            this.ssCaps.Size = new System.Drawing.Size(50, 18);
            this.ssCaps.Spring = true;
            this.ssCaps.Text = "CAPS";
            // 
            // ssNum
            // 
            this.ssNum.AutoSize = false;
            this.ssNum.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssNum.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.ssNum.Name = "ssNum";
            this.ssNum.Size = new System.Drawing.Size(40, 18);
            this.ssNum.Spring = true;
            this.ssNum.Text = "NUM";
            // 
            // ssMode
            // 
            this.ssMode.AutoSize = false;
            this.ssMode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssMode.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.ssMode.Name = "ssMode";
            this.ssMode.Size = new System.Drawing.Size(80, 18);
            this.ssMode.Spring = true;
            this.ssMode.Text = "OVERWRITE";
            // 
            // MainMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 571);
            this.Controls.Add(this.baseSplitContainer);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenuView";
            this.Text = "MSMNG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseSplitContainer)).EndInit();
            this.baseSplitContainer.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer baseSplitContainer;
        private System.Windows.Forms.ToolStripButton btConnect;
        private System.Windows.Forms.ToolStripButton btDisconnect;
        private System.Windows.Forms.ToolStripButton btAddTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btExecuteSql;
        private System.Windows.Forms.ToolStripButton btCancelSql;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton btSearch;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar ssProgressBar;
        private System.Windows.Forms.ToolStripButton btEditResult;
        private System.Windows.Forms.ToolStripButton btSaveText;
        private System.Windows.Forms.ToolStripStatusLabel ssInfo;
        private System.Windows.Forms.ToolStripStatusLabel ssMode;
        private System.Windows.Forms.ToolStripStatusLabel ssNum;
        private System.Windows.Forms.ToolStripStatusLabel ssCaps;
        private System.Windows.Forms.ToolStripButton btPreference;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btOpenText;
        private System.Windows.Forms.ToolStripStatusLabel ssZoomRatio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    }
}

