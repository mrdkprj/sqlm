namespace MasudaManager.Utility.Preference.Setting
{
    partial class OutputPanel
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
            this.flayAssist = new System.Windows.Forms.FlowLayoutPanel();
            this.grpFont = new System.Windows.Forms.GroupBox();
            this.btFont = new System.Windows.Forms.Button();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.grpCopyFormat = new System.Windows.Forms.GroupBox();
            this.cmbSeparator = new System.Windows.Forms.ComboBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.chkDisplayRowNumber = new System.Windows.Forms.CheckBox();
            this.chkDisplaySpace = new System.Windows.Forms.CheckBox();
            this.flayAssist.SuspendLayout();
            this.grpFont.SuspendLayout();
            this.grpCopyFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // flayAssist
            // 
            this.flayAssist.Controls.Add(this.grpFont);
            this.flayAssist.Controls.Add(this.grpCopyFormat);
            this.flayAssist.Controls.Add(this.chkDisplayRowNumber);
            this.flayAssist.Controls.Add(this.chkDisplaySpace);
            this.flayAssist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flayAssist.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flayAssist.Location = new System.Drawing.Point(0, 0);
            this.flayAssist.Name = "flayAssist";
            this.flayAssist.Padding = new System.Windows.Forms.Padding(5);
            this.flayAssist.Size = new System.Drawing.Size(365, 241);
            this.flayAssist.TabIndex = 3;
            this.flayAssist.WrapContents = false;
            // 
            // grpFont
            // 
            this.grpFont.Controls.Add(this.btFont);
            this.grpFont.Controls.Add(this.txtFont);
            this.grpFont.Location = new System.Drawing.Point(8, 8);
            this.grpFont.Name = "grpFont";
            this.grpFont.Size = new System.Drawing.Size(354, 52);
            this.grpFont.TabIndex = 4;
            this.grpFont.TabStop = false;
            this.grpFont.Text = "Font";
            // 
            // btFont
            // 
            this.btFont.Location = new System.Drawing.Point(321, 20);
            this.btFont.Name = "btFont";
            this.btFont.Size = new System.Drawing.Size(27, 19);
            this.btFont.TabIndex = 1;
            this.btFont.UseVisualStyleBackColor = true;
            this.btFont.Click += new System.EventHandler(this.btFont_Click);
            // 
            // txtFont
            // 
            this.txtFont.Location = new System.Drawing.Point(19, 20);
            this.txtFont.Name = "txtFont";
            this.txtFont.ReadOnly = true;
            this.txtFont.Size = new System.Drawing.Size(296, 19);
            this.txtFont.TabIndex = 0;
            // 
            // grpCopyFormat
            // 
            this.grpCopyFormat.Controls.Add(this.cmbSeparator);
            this.grpCopyFormat.Controls.Add(this.lblSeparator);
            this.grpCopyFormat.Location = new System.Drawing.Point(8, 66);
            this.grpCopyFormat.Name = "grpCopyFormat";
            this.grpCopyFormat.Size = new System.Drawing.Size(354, 47);
            this.grpCopyFormat.TabIndex = 1;
            this.grpCopyFormat.TabStop = false;
            this.grpCopyFormat.Text = "Copy data format";
            // 
            // cmbSeparator
            // 
            this.cmbSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeparator.FormattingEnabled = true;
            this.cmbSeparator.Location = new System.Drawing.Point(84, 18);
            this.cmbSeparator.Name = "cmbSeparator";
            this.cmbSeparator.Size = new System.Drawing.Size(72, 20);
            this.cmbSeparator.TabIndex = 6;
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Location = new System.Drawing.Point(19, 21);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(60, 12);
            this.lblSeparator.TabIndex = 5;
            this.lblSeparator.Text = "Separator: ";
            // 
            // chkDisplayRowNumber
            // 
            this.chkDisplayRowNumber.AutoSize = true;
            this.chkDisplayRowNumber.Location = new System.Drawing.Point(8, 119);
            this.chkDisplayRowNumber.Name = "chkDisplayRowNumber";
            this.chkDisplayRowNumber.Size = new System.Drawing.Size(125, 16);
            this.chkDisplayRowNumber.TabIndex = 7;
            this.chkDisplayRowNumber.Text = "Display row number";
            this.chkDisplayRowNumber.UseVisualStyleBackColor = true;
            // 
            // chkDisplaySpace
            // 
            this.chkDisplaySpace.AutoSize = true;
            this.chkDisplaySpace.Location = new System.Drawing.Point(8, 141);
            this.chkDisplaySpace.Name = "chkDisplaySpace";
            this.chkDisplaySpace.Size = new System.Drawing.Size(148, 16);
            this.chkDisplaySpace.TabIndex = 6;
            this.chkDisplaySpace.Text = "Display space character";
            this.chkDisplaySpace.UseVisualStyleBackColor = true;
            // 
            // OutputPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayAssist);
            this.Name = "OutputPanel";
            this.flayAssist.ResumeLayout(false);
            this.flayAssist.PerformLayout();
            this.grpFont.ResumeLayout(false);
            this.grpFont.PerformLayout();
            this.grpCopyFormat.ResumeLayout(false);
            this.grpCopyFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flayAssist;
        private System.Windows.Forms.GroupBox grpCopyFormat;
        private System.Windows.Forms.ComboBox cmbSeparator;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.GroupBox grpFont;
        private System.Windows.Forms.Button btFont;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.CheckBox chkDisplayRowNumber;
        private System.Windows.Forms.CheckBox chkDisplaySpace;
    }
}
