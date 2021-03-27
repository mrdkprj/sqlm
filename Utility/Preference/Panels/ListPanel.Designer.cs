namespace MasudaManager.Utility.Preference.Setting
{
    partial class ListPanel
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
            this.flayDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.grpFont = new System.Windows.Forms.GroupBox();
            this.btFont = new System.Windows.Forms.Button();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.grpInsert = new System.Windows.Forms.GroupBox();
            this.chkInsertObjectName = new System.Windows.Forms.CheckBox();
            this.chkEncloseObjectName = new System.Windows.Forms.CheckBox();
            this.chkInserPropertyValue = new System.Windows.Forms.CheckBox();
            this.chkEnclosePropertyValue = new System.Windows.Forms.CheckBox();
            this.flayDisplay.SuspendLayout();
            this.grpFont.SuspendLayout();
            this.grpInsert.SuspendLayout();
            this.SuspendLayout();
            // 
            // flayDisplay
            // 
            this.flayDisplay.Controls.Add(this.grpFont);
            this.flayDisplay.Controls.Add(this.grpInsert);
            this.flayDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flayDisplay.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flayDisplay.Location = new System.Drawing.Point(0, 0);
            this.flayDisplay.Name = "flayDisplay";
            this.flayDisplay.Padding = new System.Windows.Forms.Padding(5);
            this.flayDisplay.Size = new System.Drawing.Size(365, 241);
            this.flayDisplay.TabIndex = 1;
            this.flayDisplay.WrapContents = false;
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
            // grpInsert
            // 
            this.grpInsert.Controls.Add(this.chkInsertObjectName);
            this.grpInsert.Controls.Add(this.chkEncloseObjectName);
            this.grpInsert.Controls.Add(this.chkInserPropertyValue);
            this.grpInsert.Controls.Add(this.chkEnclosePropertyValue);
            this.grpInsert.Location = new System.Drawing.Point(8, 66);
            this.grpInsert.Name = "grpInsert";
            this.grpInsert.Size = new System.Drawing.Size(354, 106);
            this.grpInsert.TabIndex = 5;
            this.grpInsert.TabStop = false;
            this.grpInsert.Text = "Insert";
            // 
            // chkInsertObjectName
            // 
            this.chkInsertObjectName.AutoSize = true;
            this.chkInsertObjectName.Location = new System.Drawing.Point(19, 14);
            this.chkInsertObjectName.Name = "chkInsertObjectName";
            this.chkInsertObjectName.Size = new System.Drawing.Size(202, 16);
            this.chkInsertObjectName.TabIndex = 6;
            this.chkInsertObjectName.Text = "Insert Object name by double click";
            this.chkInsertObjectName.UseVisualStyleBackColor = true;
            // 
            // chkEncloseObjectName
            // 
            this.chkEncloseObjectName.AutoSize = true;
            this.chkEncloseObjectName.Location = new System.Drawing.Point(19, 36);
            this.chkEncloseObjectName.Name = "chkEncloseObjectName";
            this.chkEncloseObjectName.Size = new System.Drawing.Size(251, 16);
            this.chkEncloseObjectName.TabIndex = 5;
            this.chkEncloseObjectName.Text = "Enclose Object name with double quotations";
            this.chkEncloseObjectName.UseVisualStyleBackColor = true;
            // 
            // chkInserPropertyValue
            // 
            this.chkInserPropertyValue.AutoSize = true;
            this.chkInserPropertyValue.Location = new System.Drawing.Point(19, 58);
            this.chkInserPropertyValue.Name = "chkInserPropertyValue";
            this.chkInserPropertyValue.Size = new System.Drawing.Size(212, 16);
            this.chkInserPropertyValue.TabIndex = 7;
            this.chkInserPropertyValue.Text = "Insert Property value by double click";
            this.chkInserPropertyValue.UseVisualStyleBackColor = true;
            // 
            // chkEnclosePropertyValue
            // 
            this.chkEnclosePropertyValue.AutoSize = true;
            this.chkEnclosePropertyValue.Location = new System.Drawing.Point(19, 80);
            this.chkEnclosePropertyValue.Name = "chkEnclosePropertyValue";
            this.chkEnclosePropertyValue.Size = new System.Drawing.Size(261, 16);
            this.chkEnclosePropertyValue.TabIndex = 4;
            this.chkEnclosePropertyValue.Text = "Enclose Property value with double quotations";
            this.chkEnclosePropertyValue.UseVisualStyleBackColor = true;
            // 
            // ListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayDisplay);
            this.Name = "ListPanel";
            this.flayDisplay.ResumeLayout(false);
            this.grpFont.ResumeLayout(false);
            this.grpFont.PerformLayout();
            this.grpInsert.ResumeLayout(false);
            this.grpInsert.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flayDisplay;
        private System.Windows.Forms.GroupBox grpFont;
        private System.Windows.Forms.Button btFont;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.GroupBox grpInsert;
        private System.Windows.Forms.CheckBox chkInsertObjectName;
        private System.Windows.Forms.CheckBox chkEncloseObjectName;
        private System.Windows.Forms.CheckBox chkInserPropertyValue;
        private System.Windows.Forms.CheckBox chkEnclosePropertyValue;
    }
}
