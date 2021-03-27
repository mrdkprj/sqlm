namespace MasudaManager.Utility.Preference.Setting
{
    partial class TabPanel
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
            this.flayRun = new System.Windows.Forms.FlowLayoutPanel();
            this.grpFont = new System.Windows.Forms.GroupBox();
            this.btFont = new System.Windows.Forms.Button();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdShowFilePath = new System.Windows.Forms.RadioButton();
            this.rdShowText = new System.Windows.Forms.RadioButton();
            this.chkShowToolstrip = new System.Windows.Forms.CheckBox();
            this.flayRun.SuspendLayout();
            this.grpFont.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flayRun
            // 
            this.flayRun.Controls.Add(this.grpFont);
            this.flayRun.Controls.Add(this.groupBox1);
            this.flayRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flayRun.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flayRun.Location = new System.Drawing.Point(0, 0);
            this.flayRun.Name = "flayRun";
            this.flayRun.Padding = new System.Windows.Forms.Padding(5);
            this.flayRun.Size = new System.Drawing.Size(365, 241);
            this.flayRun.TabIndex = 0;
            this.flayRun.WrapContents = false;
            // 
            // grpFont
            // 
            this.grpFont.Controls.Add(this.btFont);
            this.grpFont.Controls.Add(this.txtFont);
            this.grpFont.Location = new System.Drawing.Point(8, 8);
            this.grpFont.Name = "grpFont";
            this.grpFont.Size = new System.Drawing.Size(354, 52);
            this.grpFont.TabIndex = 0;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdShowFilePath);
            this.groupBox1.Controls.Add(this.rdShowText);
            this.groupBox1.Controls.Add(this.chkShowToolstrip);
            this.groupBox1.Location = new System.Drawing.Point(8, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // rdShowFilePath
            // 
            this.rdShowFilePath.AutoSize = true;
            this.rdShowFilePath.Location = new System.Drawing.Point(21, 44);
            this.rdShowFilePath.Name = "rdShowFilePath";
            this.rdShowFilePath.Size = new System.Drawing.Size(96, 16);
            this.rdShowFilePath.TabIndex = 2;
            this.rdShowFilePath.TabStop = true;
            this.rdShowFilePath.Text = "Show file path";
            this.rdShowFilePath.UseVisualStyleBackColor = true;
            // 
            // rdShowText
            // 
            this.rdShowText.AutoSize = true;
            this.rdShowText.Location = new System.Drawing.Point(21, 22);
            this.rdShowText.Name = "rdShowText";
            this.rdShowText.Size = new System.Drawing.Size(103, 16);
            this.rdShowText.TabIndex = 1;
            this.rdShowText.TabStop = true;
            this.rdShowText.Text = "Show input text";
            this.rdShowText.UseVisualStyleBackColor = true;
            // 
            // chkShowToolstrip
            // 
            this.chkShowToolstrip.AutoSize = true;
            this.chkShowToolstrip.Location = new System.Drawing.Point(6, 0);
            this.chkShowToolstrip.Name = "chkShowToolstrip";
            this.chkShowToolstrip.Size = new System.Drawing.Size(121, 16);
            this.chkShowToolstrip.TabIndex = 0;
            this.chkShowToolstrip.Text = "Show toolstrip text";
            this.chkShowToolstrip.UseVisualStyleBackColor = true;
            this.chkShowToolstrip.CheckedChanged += new System.EventHandler(this.chkShowToolstrip_CheckedChanged);
            // 
            // TabPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayRun);
            this.Name = "TabPanel";
            this.flayRun.ResumeLayout(false);
            this.grpFont.ResumeLayout(false);
            this.grpFont.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flayRun;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdShowFilePath;
        private System.Windows.Forms.RadioButton rdShowText;
        private System.Windows.Forms.CheckBox chkShowToolstrip;
        private System.Windows.Forms.GroupBox grpFont;
        private System.Windows.Forms.Button btFont;
        private System.Windows.Forms.TextBox txtFont;

    }
}
