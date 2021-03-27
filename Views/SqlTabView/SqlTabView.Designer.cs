namespace MasudaManager.Views
{
    partial class SqlTabView
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
            this.components = new System.ComponentModel.Container();
            this.MainTab = new MasudaManager.Controls.MainTabControl();
            this.sqlTabContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlTabContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.AllowUserTabMove = true;
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.Location = new System.Drawing.Point(0, 0);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.ShowCloseButton = true;
            this.MainTab.ShowToolTips = true;
            this.MainTab.Size = new System.Drawing.Size(150, 150);
            this.MainTab.TabIndex = 0;
            // 
            // sqlTabContextMenuStrip
            // 
            this.sqlTabContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabMenuItem});
            this.sqlTabContextMenuStrip.Name = "mainTabContextMenuStrip";
            this.sqlTabContextMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // closeTabMenuItem
            // 
            this.closeTabMenuItem.Name = "closeTabMenuItem";
            this.closeTabMenuItem.ShortcutKeyDisplayString = "";
            this.closeTabMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeTabMenuItem.Text = "Close tab(&C)";
            this.closeTabMenuItem.ToolTipText = "Close tab";
            this.closeTabMenuItem.Click += new System.EventHandler(this.MainTab_CloseTabButtonClick);
            // 
            // SqlTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainTab);
            this.Name = "SqlTabView";
            this.Load += new System.EventHandler(this.SqlTabView_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SqlTabView_MouseDoubleClick);
            this.sqlTabContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MainTabControl MainTab;
        private System.Windows.Forms.ContextMenuStrip sqlTabContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem closeTabMenuItem;


    }
}
