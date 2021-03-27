namespace MasudaManager.Utility.Preference.Setting
{
    partial class EditorPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEnableWordwrap = new System.Windows.Forms.CheckBox();
            this.rdWorwrapChar = new System.Windows.Forms.RadioButton();
            this.rdWorwrapWord = new System.Windows.Forms.RadioButton();
            this.rdWorwrapSpace = new System.Windows.Forms.RadioButton();
            this.chkBraceHighlight = new System.Windows.Forms.CheckBox();
            this.chkShowSelectionMargin = new System.Windows.Forms.CheckBox();
            this.chkShowLineNumber = new System.Windows.Forms.CheckBox();
            this.flayEdit = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.flayEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdWorwrapSpace);
            this.groupBox1.Controls.Add(this.rdWorwrapWord);
            this.groupBox1.Controls.Add(this.rdWorwrapChar);
            this.groupBox1.Controls.Add(this.chkEnableWordwrap);
            this.groupBox1.Location = new System.Drawing.Point(8, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // chkEnableWordwrap
            // 
            this.chkEnableWordwrap.AutoSize = true;
            this.chkEnableWordwrap.Location = new System.Drawing.Point(6, 0);
            this.chkEnableWordwrap.Name = "chkEnableWordwrap";
            this.chkEnableWordwrap.Size = new System.Drawing.Size(110, 16);
            this.chkEnableWordwrap.TabIndex = 1;
            this.chkEnableWordwrap.Text = "Enable wordwarp";
            this.chkEnableWordwrap.UseVisualStyleBackColor = true;
            this.chkEnableWordwrap.CheckedChanged += new System.EventHandler(this.chkEnableWordwrap_CheckedChanged);
            // 
            // rdWorwrapChar
            // 
            this.rdWorwrapChar.AutoSize = true;
            this.rdWorwrapChar.Location = new System.Drawing.Point(6, 22);
            this.rdWorwrapChar.Name = "rdWorwrapChar";
            this.rdWorwrapChar.Size = new System.Drawing.Size(47, 16);
            this.rdWorwrapChar.TabIndex = 2;
            this.rdWorwrapChar.TabStop = true;
            this.rdWorwrapChar.Text = "Char";
            this.rdWorwrapChar.UseVisualStyleBackColor = true;
            this.rdWorwrapChar.CheckedChanged += new System.EventHandler(this.rdWorwrapChar_CheckedChanged);
            // 
            // rdWorwrapWord
            // 
            this.rdWorwrapWord.AutoSize = true;
            this.rdWorwrapWord.Location = new System.Drawing.Point(59, 22);
            this.rdWorwrapWord.Name = "rdWorwrapWord";
            this.rdWorwrapWord.Size = new System.Drawing.Size(48, 16);
            this.rdWorwrapWord.TabIndex = 3;
            this.rdWorwrapWord.TabStop = true;
            this.rdWorwrapWord.Text = "Word";
            this.rdWorwrapWord.UseVisualStyleBackColor = true;
            this.rdWorwrapWord.CheckedChanged += new System.EventHandler(this.rdWorwrapWord_CheckedChanged);
            // 
            // rdWorwrapSpace
            // 
            this.rdWorwrapSpace.AutoSize = true;
            this.rdWorwrapSpace.Location = new System.Drawing.Point(113, 22);
            this.rdWorwrapSpace.Name = "rdWorwrapSpace";
            this.rdWorwrapSpace.Size = new System.Drawing.Size(54, 16);
            this.rdWorwrapSpace.TabIndex = 4;
            this.rdWorwrapSpace.TabStop = true;
            this.rdWorwrapSpace.Text = "Space";
            this.rdWorwrapSpace.UseVisualStyleBackColor = true;
            this.rdWorwrapSpace.CheckedChanged += new System.EventHandler(this.rdWorwrapSpace_CheckedChanged);
            // 
            // chkBraceHighlight
            // 
            this.chkBraceHighlight.AutoSize = true;
            this.chkBraceHighlight.Location = new System.Drawing.Point(8, 52);
            this.chkBraceHighlight.Name = "chkBraceHighlight";
            this.chkBraceHighlight.Size = new System.Drawing.Size(161, 16);
            this.chkBraceHighlight.TabIndex = 1;
            this.chkBraceHighlight.Text = "Highlight matching bracket";
            this.chkBraceHighlight.UseVisualStyleBackColor = true;
            // 
            // chkSelectionMargin
            // 
            this.chkShowSelectionMargin.AutoSize = true;
            this.chkShowSelectionMargin.Location = new System.Drawing.Point(8, 30);
            this.chkShowSelectionMargin.Name = "chkSelectionMargin";
            this.chkShowSelectionMargin.Size = new System.Drawing.Size(139, 16);
            this.chkShowSelectionMargin.TabIndex = 3;
            this.chkShowSelectionMargin.Text = "Show selection margin";
            this.chkShowSelectionMargin.UseVisualStyleBackColor = true;
            // 
            // chkShowLineNumber
            // 
            this.chkShowLineNumber.AutoSize = true;
            this.chkShowLineNumber.Location = new System.Drawing.Point(8, 8);
            this.chkShowLineNumber.Name = "chkShowLineNumber";
            this.chkShowLineNumber.Size = new System.Drawing.Size(114, 16);
            this.chkShowLineNumber.TabIndex = 0;
            this.chkShowLineNumber.Text = "Show line number";
            this.chkShowLineNumber.UseVisualStyleBackColor = true;
            // 
            // flayEdit
            // 
            this.flayEdit.Controls.Add(this.chkShowLineNumber);
            this.flayEdit.Controls.Add(this.chkShowSelectionMargin);
            this.flayEdit.Controls.Add(this.chkBraceHighlight);
            this.flayEdit.Controls.Add(this.groupBox1);
            this.flayEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flayEdit.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flayEdit.Location = new System.Drawing.Point(0, 0);
            this.flayEdit.Name = "flayEdit";
            this.flayEdit.Padding = new System.Windows.Forms.Padding(5);
            this.flayEdit.Size = new System.Drawing.Size(365, 241);
            this.flayEdit.TabIndex = 5;
            this.flayEdit.WrapContents = false;
            // 
            // EditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayEdit);
            this.Name = "EditorPanel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flayEdit.ResumeLayout(false);
            this.flayEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdWorwrapSpace;
        private System.Windows.Forms.RadioButton rdWorwrapWord;
        private System.Windows.Forms.RadioButton rdWorwrapChar;
        private System.Windows.Forms.CheckBox chkEnableWordwrap;
        private System.Windows.Forms.CheckBox chkBraceHighlight;
        private System.Windows.Forms.CheckBox chkShowSelectionMargin;
        private System.Windows.Forms.CheckBox chkShowLineNumber;
        private System.Windows.Forms.FlowLayoutPanel flayEdit;

    }
}
