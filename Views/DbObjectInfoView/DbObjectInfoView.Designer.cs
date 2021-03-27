namespace MasudaManager.Views
{
    partial class DbObjectInfoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbObjectInfoView));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpboxObject = new System.Windows.Forms.GroupBox();
            this.btRefreshObjectView = new System.Windows.Forms.Button();
            this.cmbObjectList = new System.Windows.Forms.ComboBox();
            this.txtFilterObject = new MasudaManager.Controls.XTextBox();
            this.objectViewPanel = new System.Windows.Forms.Panel();
            this.grpboxObjectProperty = new System.Windows.Forms.GroupBox();
            this.propertyTabPanel = new System.Windows.Forms.Panel();
            this.txtFilterProperty = new MasudaManager.Controls.XTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpboxObject.SuspendLayout();
            this.grpboxObjectProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpboxObject);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpboxObjectProperty);
            this.splitContainer1.Size = new System.Drawing.Size(150, 517);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 0;
            // 
            // grpboxObject
            // 
            this.grpboxObject.Controls.Add(this.btRefreshObjectView);
            this.grpboxObject.Controls.Add(this.cmbObjectList);
            this.grpboxObject.Controls.Add(this.txtFilterObject);
            this.grpboxObject.Controls.Add(this.objectViewPanel);
            this.grpboxObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpboxObject.Location = new System.Drawing.Point(0, 0);
            this.grpboxObject.Name = "grpboxObject";
            this.grpboxObject.Size = new System.Drawing.Size(150, 255);
            this.grpboxObject.TabIndex = 1000;
            this.grpboxObject.TabStop = false;
            // 
            // btRefreshObjectView
            // 
            this.btRefreshObjectView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefreshObjectView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btRefreshObjectView.BackgroundImage")));
            this.btRefreshObjectView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btRefreshObjectView.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.btRefreshObjectView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btRefreshObjectView.Location = new System.Drawing.Point(115, 44);
            this.btRefreshObjectView.Name = "btRefreshObjectView";
            this.btRefreshObjectView.Size = new System.Drawing.Size(23, 19);
            this.btRefreshObjectView.TabIndex = 503;
            this.btRefreshObjectView.UseVisualStyleBackColor = true;
            this.btRefreshObjectView.Click += new System.EventHandler(this.btRefreshObjectView_Click);
            // 
            // cmbObjectList
            // 
            this.cmbObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbObjectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjectList.FormattingEnabled = true;
            this.cmbObjectList.Location = new System.Drawing.Point(11, 18);
            this.cmbObjectList.Name = "cmbObjectList";
            this.cmbObjectList.Size = new System.Drawing.Size(127, 20);
            this.cmbObjectList.TabIndex = 401;
            this.cmbObjectList.SelectedIndexChanged += new System.EventHandler(this.cmbObjectList_SelectedIndexChanged);
            // 
            // txtFilterObject
            // 
            this.txtFilterObject.AllowAutoSelectAll = true;
            this.txtFilterObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterObject.Location = new System.Drawing.Point(11, 44);
            this.txtFilterObject.Name = "txtFilterObject";
            this.txtFilterObject.Size = new System.Drawing.Size(98, 19);
            this.txtFilterObject.TabIndex = 300;
            this.txtFilterObject.TextChanged += new System.EventHandler(this.txtFilterObject_TextChanged);
            // 
            // objectViewPanel
            // 
            this.objectViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectViewPanel.Location = new System.Drawing.Point(11, 71);
            this.objectViewPanel.Name = "objectViewPanel";
            this.objectViewPanel.Size = new System.Drawing.Size(127, 169);
            this.objectViewPanel.TabIndex = 502;
            // 
            // grpboxObjectProperty
            // 
            this.grpboxObjectProperty.Controls.Add(this.propertyTabPanel);
            this.grpboxObjectProperty.Controls.Add(this.txtFilterProperty);
            this.grpboxObjectProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpboxObjectProperty.Location = new System.Drawing.Point(0, 0);
            this.grpboxObjectProperty.Name = "grpboxObjectProperty";
            this.grpboxObjectProperty.Size = new System.Drawing.Size(150, 258);
            this.grpboxObjectProperty.TabIndex = 1000;
            this.grpboxObjectProperty.TabStop = false;
            // 
            // propertyTabPanel
            // 
            this.propertyTabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyTabPanel.Location = new System.Drawing.Point(11, 47);
            this.propertyTabPanel.Name = "propertyTabPanel";
            this.propertyTabPanel.Size = new System.Drawing.Size(127, 205);
            this.propertyTabPanel.TabIndex = 501;
            // 
            // txtFilterProperty
            // 
            this.txtFilterProperty.AllowAutoSelectAll = true;
            this.txtFilterProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterProperty.Location = new System.Drawing.Point(11, 18);
            this.txtFilterProperty.Name = "txtFilterProperty";
            this.txtFilterProperty.Size = new System.Drawing.Size(127, 19);
            this.txtFilterProperty.TabIndex = 500;
            this.txtFilterProperty.TextChanged += new System.EventHandler(this.txtFilterProperty_TextChanged);
            // 
            // DbObjectInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DbObjectInfoView";
            this.Size = new System.Drawing.Size(150, 517);
            this.Load += new System.EventHandler(this.DbObjectInfoView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpboxObject.ResumeLayout(false);
            this.grpboxObject.PerformLayout();
            this.grpboxObjectProperty.ResumeLayout(false);
            this.grpboxObjectProperty.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpboxObject;
        private System.Windows.Forms.ComboBox cmbObjectList;
        private Controls.XTextBox txtFilterObject;
        private System.Windows.Forms.Panel objectViewPanel;
        private System.Windows.Forms.GroupBox grpboxObjectProperty;
        private System.Windows.Forms.Panel propertyTabPanel;
        private Controls.XTextBox txtFilterProperty;
        private System.Windows.Forms.Button btRefreshObjectView;
    }
}
