namespace MasudaManager.Views
{
    partial class SearchGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchGridView));
            this.btNext = new System.Windows.Forms.Button();
            this.combSearchText = new System.Windows.Forms.ComboBox();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.rdSuffix = new System.Windows.Forms.RadioButton();
            this.rdPrefix = new System.Windows.Forms.RadioButton();
            this.rdPartial = new System.Windows.Forms.RadioButton();
            this.rdExact = new System.Windows.Forms.RadioButton();
            this.chkAutoClose = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.chkSearchHeader = new System.Windows.Forms.CheckBox();
            this.btPrevious = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.grpSearchOption = new System.Windows.Forms.GroupBox();
            this.grpMode.SuspendLayout();
            this.grpSearchOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(273, 10);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(58, 23);
            this.btNext.TabIndex = 2;
            this.btNext.Text = "Next";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // combSearchText
            // 
            this.combSearchText.FormattingEnabled = true;
            this.combSearchText.Location = new System.Drawing.Point(12, 12);
            this.combSearchText.Name = "combSearchText";
            this.combSearchText.Size = new System.Drawing.Size(255, 20);
            this.combSearchText.TabIndex = 3;
            this.combSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combSearchText_KeyDown);
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.rdSuffix);
            this.grpMode.Controls.Add(this.rdPrefix);
            this.grpMode.Controls.Add(this.rdPartial);
            this.grpMode.Controls.Add(this.rdExact);
            this.grpMode.Location = new System.Drawing.Point(12, 38);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(255, 48);
            this.grpMode.TabIndex = 4;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "Mode";
            // 
            // rdSuffix
            // 
            this.rdSuffix.AutoSize = true;
            this.rdSuffix.Location = new System.Drawing.Point(188, 20);
            this.rdSuffix.Name = "rdSuffix";
            this.rdSuffix.Size = new System.Drawing.Size(53, 16);
            this.rdSuffix.TabIndex = 3;
            this.rdSuffix.Text = "Suffix";
            this.rdSuffix.UseVisualStyleBackColor = true;
            this.rdSuffix.CheckedChanged += new System.EventHandler(this.rdSuffix_CheckedChanged);
            // 
            // rdPrefix
            // 
            this.rdPrefix.AutoSize = true;
            this.rdPrefix.Location = new System.Drawing.Point(129, 20);
            this.rdPrefix.Name = "rdPrefix";
            this.rdPrefix.Size = new System.Drawing.Size(53, 16);
            this.rdPrefix.TabIndex = 2;
            this.rdPrefix.Text = "Prefix";
            this.rdPrefix.UseVisualStyleBackColor = true;
            this.rdPrefix.CheckedChanged += new System.EventHandler(this.rdPrefix_CheckedChanged);
            // 
            // rdPartial
            // 
            this.rdPartial.AutoSize = true;
            this.rdPartial.Location = new System.Drawing.Point(67, 20);
            this.rdPartial.Name = "rdPartial";
            this.rdPartial.Size = new System.Drawing.Size(56, 16);
            this.rdPartial.TabIndex = 1;
            this.rdPartial.Text = "Partial";
            this.rdPartial.UseVisualStyleBackColor = true;
            this.rdPartial.CheckedChanged += new System.EventHandler(this.rdPartial_CheckedChanged);
            // 
            // rdExact
            // 
            this.rdExact.AutoSize = true;
            this.rdExact.Checked = true;
            this.rdExact.Location = new System.Drawing.Point(9, 20);
            this.rdExact.Name = "rdExact";
            this.rdExact.Size = new System.Drawing.Size(52, 16);
            this.rdExact.TabIndex = 0;
            this.rdExact.TabStop = true;
            this.rdExact.Text = "Exact";
            this.rdExact.UseVisualStyleBackColor = true;
            this.rdExact.CheckedChanged += new System.EventHandler(this.rdExact_CheckedChanged);
            // 
            // chkAutoClose
            // 
            this.chkAutoClose.AutoSize = true;
            this.chkAutoClose.Location = new System.Drawing.Point(129, 18);
            this.chkAutoClose.Name = "chkAutoClose";
            this.chkAutoClose.Size = new System.Drawing.Size(119, 16);
            this.chkAutoClose.TabIndex = 2;
            this.chkAutoClose.Text = "Close after search";
            this.chkAutoClose.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(9, 39);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(102, 16);
            this.chkCaseSensitive.TabIndex = 1;
            this.chkCaseSensitive.Text = "Case-sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkSearchHeader
            // 
            this.chkSearchHeader.AutoSize = true;
            this.chkSearchHeader.Location = new System.Drawing.Point(9, 17);
            this.chkSearchHeader.Name = "chkSearchHeader";
            this.chkSearchHeader.Size = new System.Drawing.Size(97, 16);
            this.chkSearchHeader.TabIndex = 0;
            this.chkSearchHeader.Text = "Search header";
            this.chkSearchHeader.UseVisualStyleBackColor = true;
            // 
            // btPrevious
            // 
            this.btPrevious.Location = new System.Drawing.Point(273, 40);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(58, 23);
            this.btPrevious.TabIndex = 5;
            this.btPrevious.Text = "Prev";
            this.btPrevious.UseVisualStyleBackColor = true;
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(273, 92);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(58, 23);
            this.btClose.TabIndex = 6;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // grpSearchOption
            // 
            this.grpSearchOption.Controls.Add(this.chkSearchHeader);
            this.grpSearchOption.Controls.Add(this.chkAutoClose);
            this.grpSearchOption.Controls.Add(this.chkCaseSensitive);
            this.grpSearchOption.Location = new System.Drawing.Point(12, 92);
            this.grpSearchOption.Name = "grpSearchOption";
            this.grpSearchOption.Size = new System.Drawing.Size(255, 61);
            this.grpSearchOption.TabIndex = 7;
            this.grpSearchOption.TabStop = false;
            this.grpSearchOption.Text = "Option";
            // 
            // SearchGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 162);
            this.Controls.Add(this.grpSearchOption);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btPrevious);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.combSearchText);
            this.Controls.Add(this.btNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(351, 151);
            this.Name = "SearchGridView";
            this.Text = "Search";
            this.Activated += new System.EventHandler(this.SearchGrid_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchGrid_FormClosing);
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.grpSearchOption.ResumeLayout(false);
            this.grpSearchOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.ComboBox combSearchText;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.CheckBox chkAutoClose;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.CheckBox chkSearchHeader;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.RadioButton rdPrefix;
        private System.Windows.Forms.RadioButton rdPartial;
        private System.Windows.Forms.RadioButton rdExact;
        private System.Windows.Forms.RadioButton rdSuffix;
        private System.Windows.Forms.GroupBox grpSearchOption;
    }
}