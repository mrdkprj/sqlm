namespace MasudaManager.Utility.Preference.Setting
{
    partial class FilePanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.grpExportImport = new System.Windows.Forms.GroupBox();
            this.chkEncloseFields = new System.Windows.Forms.CheckBox();
            this.cmbSeparator = new System.Windows.Forms.ComboBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.grpEncoding = new System.Windows.Forms.GroupBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.grpOpenMode = new System.Windows.Forms.GroupBox();
            this.chkReadLock = new System.Windows.Forms.CheckBox();
            this.chkWriteLock = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpExportImport.SuspendLayout();
            this.grpEncoding.SuspendLayout();
            this.grpOpenMode.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpExportImport
            // 
            this.grpExportImport.Controls.Add(this.chkEncloseFields);
            this.grpExportImport.Controls.Add(this.cmbSeparator);
            this.grpExportImport.Controls.Add(this.lblSeparator);
            this.grpExportImport.Location = new System.Drawing.Point(8, 141);
            this.grpExportImport.Name = "grpExportImport";
            this.grpExportImport.Size = new System.Drawing.Size(354, 83);
            this.grpExportImport.TabIndex = 1;
            this.grpExportImport.TabStop = false;
            this.grpExportImport.Text = "Export/Import";
            // 
            // chkEncloseFields
            // 
            this.chkEncloseFields.AutoSize = true;
            this.chkEncloseFields.Location = new System.Drawing.Point(19, 47);
            this.chkEncloseFields.Name = "chkEncloseFields";
            this.chkEncloseFields.Size = new System.Drawing.Size(96, 16);
            this.chkEncloseFields.TabIndex = 5;
            this.chkEncloseFields.Text = "Enclose fields";
            this.chkEncloseFields.UseVisualStyleBackColor = true;
            // 
            // cmbSeparator
            // 
            this.cmbSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeparator.FormattingEnabled = true;
            this.cmbSeparator.Location = new System.Drawing.Point(82, 21);
            this.cmbSeparator.Name = "cmbSeparator";
            this.cmbSeparator.Size = new System.Drawing.Size(72, 20);
            this.cmbSeparator.TabIndex = 4;
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Location = new System.Drawing.Point(19, 24);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(60, 12);
            this.lblSeparator.TabIndex = 3;
            this.lblSeparator.Text = "Separator: ";
            // 
            // grpEncoding
            // 
            this.grpEncoding.Controls.Add(this.cmbEncoding);
            this.grpEncoding.Location = new System.Drawing.Point(8, 8);
            this.grpEncoding.Name = "grpEncoding";
            this.grpEncoding.Size = new System.Drawing.Size(354, 54);
            this.grpEncoding.TabIndex = 3;
            this.grpEncoding.TabStop = false;
            this.grpEncoding.Text = "Encoding";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Location = new System.Drawing.Point(19, 18);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(136, 20);
            this.cmbEncoding.TabIndex = 8;
            // 
            // grpOpenMode
            // 
            this.grpOpenMode.Controls.Add(this.chkReadLock);
            this.grpOpenMode.Controls.Add(this.chkWriteLock);
            this.grpOpenMode.Location = new System.Drawing.Point(8, 68);
            this.grpOpenMode.Name = "grpOpenMode";
            this.grpOpenMode.Size = new System.Drawing.Size(354, 67);
            this.grpOpenMode.TabIndex = 2;
            this.grpOpenMode.TabStop = false;
            this.grpOpenMode.Text = "SQL File";
            // 
            // chkReadLock
            // 
            this.chkReadLock.AutoSize = true;
            this.chkReadLock.Location = new System.Drawing.Point(19, 40);
            this.chkReadLock.Name = "chkReadLock";
            this.chkReadLock.Size = new System.Drawing.Size(155, 16);
            this.chkReadLock.TabIndex = 1;
            this.chkReadLock.Text = "Open file with READ lock";
            this.chkReadLock.UseVisualStyleBackColor = true;
            // 
            // chkWriteLock
            // 
            this.chkWriteLock.AutoSize = true;
            this.chkWriteLock.Location = new System.Drawing.Point(19, 18);
            this.chkWriteLock.Name = "chkWriteLock";
            this.chkWriteLock.Size = new System.Drawing.Size(158, 16);
            this.chkWriteLock.TabIndex = 0;
            this.chkWriteLock.Text = "Open file with WRITE lock";
            this.chkWriteLock.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.grpEncoding);
            this.flowLayoutPanel1.Controls.Add(this.grpOpenMode);
            this.flowLayoutPanel1.Controls.Add(this.grpExportImport);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(365, 241);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // FilePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FilePanel";
            this.grpExportImport.ResumeLayout(false);
            this.grpExportImport.PerformLayout();
            this.grpEncoding.ResumeLayout(false);
            this.grpOpenMode.ResumeLayout(false);
            this.grpOpenMode.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpExportImport;
        private System.Windows.Forms.CheckBox chkEncloseFields;
        private System.Windows.Forms.ComboBox cmbSeparator;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.GroupBox grpEncoding;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.GroupBox grpOpenMode;
        private System.Windows.Forms.CheckBox chkReadLock;
        private System.Windows.Forms.CheckBox chkWriteLock;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;


    }
}
