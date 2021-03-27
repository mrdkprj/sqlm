namespace MasudaManager.Views
{
    partial class ImportView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportView));
            this.lblTable = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.btImport = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtImportFile = new System.Windows.Forms.TextBox();
            this.btSelectFile = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.importProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.rdNoHeader = new System.Windows.Forms.RadioButton();
            this.rdWithHeader = new System.Windows.Forms.RadioButton();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.grpImportOption = new System.Windows.Forms.GroupBox();
            this.statusStrip1.SuspendLayout();
            this.grpImportOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(12, 9);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(39, 12);
            this.lblTable.TabIndex = 2;
            this.lblTable.Text = "Table :";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(52, 9);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(57, 12);
            this.lblTableName.TabIndex = 3;
            this.lblTableName.Text = "tablename";
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(141, 145);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(75, 23);
            this.btImport.TabIndex = 4;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 31);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(30, 12);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "File :";
            // 
            // txtImportFile
            // 
            this.txtImportFile.Location = new System.Drawing.Point(54, 28);
            this.txtImportFile.Name = "txtImportFile";
            this.txtImportFile.Size = new System.Drawing.Size(308, 19);
            this.txtImportFile.TabIndex = 6;
            this.txtImportFile.TextChanged += new System.EventHandler(this.txtImportFile_TextChanged);
            // 
            // btSelectFile
            // 
            this.btSelectFile.Location = new System.Drawing.Point(368, 26);
            this.btSelectFile.Name = "btSelectFile";
            this.btSelectFile.Size = new System.Drawing.Size(30, 21);
            this.btSelectFile.TabIndex = 7;
            this.btSelectFile.UseVisualStyleBackColor = true;
            this.btSelectFile.Click += new System.EventHandler(this.btSelectFile_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(323, 145);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 8;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(222, 145);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.importProgressBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 184);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(410, 23);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 18);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // importProgressBar
            // 
            this.importProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.importProgressBar.Name = "importProgressBar";
            this.importProgressBar.Size = new System.Drawing.Size(100, 17);
            // 
            // rdNoHeader
            // 
            this.rdNoHeader.AutoSize = true;
            this.rdNoHeader.Location = new System.Drawing.Point(6, 43);
            this.rdNoHeader.Name = "rdNoHeader";
            this.rdNoHeader.Size = new System.Drawing.Size(75, 16);
            this.rdNoHeader.TabIndex = 11;
            this.rdNoHeader.TabStop = true;
            this.rdNoHeader.Text = "No header";
            this.rdNoHeader.UseVisualStyleBackColor = true;
            // 
            // rdWithHeader
            // 
            this.rdWithHeader.AccessibleDescription = "s";
            this.rdWithHeader.AutoSize = true;
            this.rdWithHeader.Location = new System.Drawing.Point(87, 43);
            this.rdWithHeader.Name = "rdWithHeader";
            this.rdWithHeader.Size = new System.Drawing.Size(130, 16);
            this.rdWithHeader.TabIndex = 12;
            this.rdWithHeader.TabStop = true;
            this.rdWithHeader.Text = "Column name header";
            this.rdWithHeader.UseVisualStyleBackColor = true;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(6, 21);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(47, 12);
            this.lblFormat.TabIndex = 13;
            this.lblFormat.Text = "Format :";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(59, 17);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(75, 20);
            this.cmbFormat.TabIndex = 10;
            // 
            // grpImportOption
            // 
            this.grpImportOption.Controls.Add(this.cmbFormat);
            this.grpImportOption.Controls.Add(this.lblFormat);
            this.grpImportOption.Controls.Add(this.rdWithHeader);
            this.grpImportOption.Controls.Add(this.rdNoHeader);
            this.grpImportOption.Location = new System.Drawing.Point(12, 53);
            this.grpImportOption.Name = "grpImportOption";
            this.grpImportOption.Size = new System.Drawing.Size(386, 75);
            this.grpImportOption.TabIndex = 1;
            this.grpImportOption.TabStop = false;
            this.grpImportOption.Text = "Import option";
            // 
            // ImportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 207);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSelectFile);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.txtImportFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.grpImportOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImportView";
            this.Text = "Import";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportView_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpImportOption.ResumeLayout(false);
            this.grpImportOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtImportFile;
        private System.Windows.Forms.Button btSelectFile;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar importProgressBar;
        private System.Windows.Forms.RadioButton rdNoHeader;
        private System.Windows.Forms.RadioButton rdWithHeader;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.GroupBox grpImportOption;
    }
}