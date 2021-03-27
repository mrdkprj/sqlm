namespace MasudaManager.Views
{
    partial class ExportView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportView));
            this.txtSql = new System.Windows.Forms.RichTextBox();
            this.grpExportOption = new System.Windows.Forms.GroupBox();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.rdColumnNameHeader = new System.Windows.Forms.RadioButton();
            this.rdNoHeader = new System.Windows.Forms.RadioButton();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.btExport = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtExportFile = new System.Windows.Forms.TextBox();
            this.btSelectFile = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.exportProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.grpExportOption.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSql
            // 
            this.txtSql.Location = new System.Drawing.Point(53, 28);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(347, 67);
            this.txtSql.TabIndex = 0;
            this.txtSql.Text = "a\nb\nc\nd\ne";
            // 
            // grpExportOption
            // 
            this.grpExportOption.Controls.Add(this.cmbFormat);
            this.grpExportOption.Controls.Add(this.lblFormat);
            this.grpExportOption.Controls.Add(this.rdColumnNameHeader);
            this.grpExportOption.Controls.Add(this.rdNoHeader);
            this.grpExportOption.Location = new System.Drawing.Point(14, 135);
            this.grpExportOption.Name = "grpExportOption";
            this.grpExportOption.Size = new System.Drawing.Size(386, 72);
            this.grpExportOption.TabIndex = 1;
            this.grpExportOption.TabStop = false;
            this.grpExportOption.Text = "Export option";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(60, 16);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(75, 20);
            this.cmbFormat.TabIndex = 10;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(7, 20);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(47, 12);
            this.lblFormat.TabIndex = 11;
            this.lblFormat.Text = "Format :";
            // 
            // rdColumnNameHeader
            // 
            this.rdColumnNameHeader.AutoSize = true;
            this.rdColumnNameHeader.Location = new System.Drawing.Point(90, 42);
            this.rdColumnNameHeader.Name = "rdColumnNameHeader";
            this.rdColumnNameHeader.Size = new System.Drawing.Size(130, 16);
            this.rdColumnNameHeader.TabIndex = 12;
            this.rdColumnNameHeader.TabStop = true;
            this.rdColumnNameHeader.Text = "Column name header";
            this.rdColumnNameHeader.UseVisualStyleBackColor = true;
            // 
            // rdNoHeader
            // 
            this.rdNoHeader.AutoSize = true;
            this.rdNoHeader.Location = new System.Drawing.Point(9, 42);
            this.rdNoHeader.Name = "rdNoHeader";
            this.rdNoHeader.Size = new System.Drawing.Size(75, 16);
            this.rdNoHeader.TabIndex = 11;
            this.rdNoHeader.TabStop = true;
            this.rdNoHeader.Text = "No header";
            this.rdNoHeader.UseVisualStyleBackColor = true;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(12, 9);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(43, 12);
            this.lblTable.TabIndex = 2;
            this.lblTable.Text = "Table : ";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(53, 9);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(57, 12);
            this.lblTableName.TabIndex = 3;
            this.lblTableName.Text = "tablename";
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(143, 221);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(75, 23);
            this.btExport.TabIndex = 4;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 110);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(34, 12);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "File : ";
            // 
            // txtExportFile
            // 
            this.txtExportFile.Location = new System.Drawing.Point(53, 107);
            this.txtExportFile.Name = "txtExportFile";
            this.txtExportFile.Size = new System.Drawing.Size(307, 19);
            this.txtExportFile.TabIndex = 6;
            this.txtExportFile.TextChanged += new System.EventHandler(this.txtExportFile_TextChanged);
            // 
            // btSelectFile
            // 
            this.btSelectFile.Location = new System.Drawing.Point(370, 107);
            this.btSelectFile.Name = "btSelectFile";
            this.btSelectFile.Size = new System.Drawing.Size(30, 21);
            this.btSelectFile.TabIndex = 7;
            this.btSelectFile.UseVisualStyleBackColor = true;
            this.btSelectFile.Click += new System.EventHandler(this.btSelectFile_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(325, 221);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 8;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(224, 221);
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
            this.exportProgressBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 256);
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
            this.lblStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // exportProgressBar
            // 
            this.exportProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exportProgressBar.Name = "exportProgressBar";
            this.exportProgressBar.Size = new System.Drawing.Size(100, 17);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "SQL : ";
            // 
            // ExportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 279);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSelectFile);
            this.Controls.Add(this.txtExportFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.grpExportOption);
            this.Controls.Add(this.txtSql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ExportView";
            this.Text = "Export";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportView_FormClosing);
            this.grpExportOption.ResumeLayout(false);
            this.grpExportOption.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtSql;
        private System.Windows.Forms.GroupBox grpExportOption;
        private System.Windows.Forms.RadioButton rdColumnNameHeader;
        private System.Windows.Forms.RadioButton rdNoHeader;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtExportFile;
        private System.Windows.Forms.Button btSelectFile;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar exportProgressBar;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label label4;
    }
}