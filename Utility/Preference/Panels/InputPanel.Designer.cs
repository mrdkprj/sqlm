namespace MasudaManager.Utility.Preference.Setting
{
    partial class InputPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowColumnList = new System.Windows.Forms.CheckBox();
            this.chkShowObjectList = new System.Windows.Forms.CheckBox();
            this.chkUseAssistant = new System.Windows.Forms.CheckBox();
            this.flayAssist.SuspendLayout();
            this.grpFont.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flayAssist
            // 
            this.flayAssist.Controls.Add(this.grpFont);
            this.flayAssist.Controls.Add(this.groupBox1);
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
            this.grpFont.TabIndex = 2;
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
            this.groupBox1.Controls.Add(this.chkShowColumnList);
            this.groupBox1.Controls.Add(this.chkShowObjectList);
            this.groupBox1.Controls.Add(this.chkUseAssistant);
            this.groupBox1.Location = new System.Drawing.Point(8, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 75);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // chkShowColumnList
            // 
            this.chkShowColumnList.AutoSize = true;
            this.chkShowColumnList.Location = new System.Drawing.Point(19, 44);
            this.chkShowColumnList.Name = "chkShowColumnList";
            this.chkShowColumnList.Size = new System.Drawing.Size(171, 16);
            this.chkShowColumnList.TabIndex = 2;
            this.chkShowColumnList.Text = "Enable column name support";
            this.chkShowColumnList.UseVisualStyleBackColor = true;
            // 
            // chkShowObjectList
            // 
            this.chkShowObjectList.AutoSize = true;
            this.chkShowObjectList.Location = new System.Drawing.Point(19, 22);
            this.chkShowObjectList.Name = "chkShowObjectList";
            this.chkShowObjectList.Size = new System.Drawing.Size(160, 16);
            this.chkShowObjectList.TabIndex = 1;
            this.chkShowObjectList.Text = "Enable table name support";
            this.chkShowObjectList.UseVisualStyleBackColor = true;
            // 
            // chkUseAssistant
            // 
            this.chkUseAssistant.AutoSize = true;
            this.chkUseAssistant.Location = new System.Drawing.Point(7, 0);
            this.chkUseAssistant.Name = "chkUseAssistant";
            this.chkUseAssistant.Size = new System.Drawing.Size(185, 16);
            this.chkUseAssistant.TabIndex = 0;
            this.chkUseAssistant.Text = "Show input support after period";
            this.chkUseAssistant.UseVisualStyleBackColor = true;
            this.chkUseAssistant.CheckedChanged += new System.EventHandler(this.chkUseAssistant_CheckedChanged);
            // 
            // InputPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayAssist);
            this.Name = "InputPanel";
            this.flayAssist.ResumeLayout(false);
            this.grpFont.ResumeLayout(false);
            this.grpFont.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flayAssist;
        private System.Windows.Forms.CheckBox chkUseAssistant;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowColumnList;
        private System.Windows.Forms.CheckBox chkShowObjectList;
        private System.Windows.Forms.GroupBox grpFont;
        private System.Windows.Forms.Button btFont;
        private System.Windows.Forms.TextBox txtFont;
    }
}
