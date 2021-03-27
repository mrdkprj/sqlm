namespace MasudaManager.Controls
{
    partial class ObjectView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.objectViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSelectStmtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createInsertStmtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDeleteStmtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editResultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSelectCountStmtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectViewContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // objectViewContextMenu
            // 
            this.objectViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayDataMenuItem,
            this.toolStripSeparator1,
            this.copyMenuItem,
            this.createSqlToolStripMenuItem,
            this.toolStripSeparator2,
            this.editResultMenuItem,
            this.toolStripSeparator3,
            this.exportMenuItem,
            this.importMenuItem});
            this.objectViewContextMenu.Name = "objectViewContextMenu";
            this.objectViewContextMenu.Size = new System.Drawing.Size(169, 176);
            // 
            // displayDataToolStripMenuItem
            // 
            this.displayDataMenuItem.Name = "displayDataToolStripMenuItem";
            this.displayDataMenuItem.Size = new System.Drawing.Size(168, 22);
            this.displayDataMenuItem.Text = "Display data";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyMenuItem.Name = "copyToolStripMenuItem";
            this.copyMenuItem.Size = new System.Drawing.Size(168, 22);
            this.copyMenuItem.Text = "Copy";
            // 
            // createSqlToolStripMenuItem
            // 
            this.createSqlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createSelectStmtMenuItem,
            this.createSelectCountStmtMenuItem,
            this.createInsertStmtMenuItem,
            this.createDeleteStmtMenuItem});
            this.createSqlToolStripMenuItem.Name = "createSqlToolStripMenuItem";
            this.createSqlToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.createSqlToolStripMenuItem.Text = "Create SQL";
            // 
            // selectToolStripMenuItem
            // 
            this.createSelectStmtMenuItem.Name = "selectToolStripMenuItem";
            this.createSelectStmtMenuItem.Size = new System.Drawing.Size(103, 22);
            this.createSelectStmtMenuItem.Text = "Select";
            // 
            // insertToolStripMenuItem
            // 
            this.createInsertStmtMenuItem.Name = "insertToolStripMenuItem";
            this.createInsertStmtMenuItem.Size = new System.Drawing.Size(103, 22);
            this.createInsertStmtMenuItem.Text = "Insert";
            // 
            // deleteToolStripMenuItem
            // 
            this.createDeleteStmtMenuItem.Name = "deleteToolStripMenuItem";
            this.createDeleteStmtMenuItem.Size = new System.Drawing.Size(103, 22);
            this.createDeleteStmtMenuItem.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // editSqlResultsToolStripMenuItem
            // 
            this.editResultMenuItem.Name = "editSqlResultsToolStripMenuItem";
            this.editResultMenuItem.Size = new System.Drawing.Size(168, 22);
            this.editResultMenuItem.Text = "Edit";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(165, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportMenuItem.Name = "exportToolStripMenuItem";
            this.exportMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exportMenuItem.Text = "Export";
            // 
            // importToolStripMenuItem
            // 
            this.importMenuItem.Name = "importToolStripMenuItem";
            this.importMenuItem.Size = new System.Drawing.Size(168, 22);
            this.importMenuItem.Text = "Import";
            // 
            // countToolStripMenuItem
            // 
            this.createSelectCountStmtMenuItem.Name = "countToolStripMenuItem";
            this.createSelectCountStmtMenuItem.Size = new System.Drawing.Size(114, 22);
            this.createSelectCountStmtMenuItem.Text = "Count(*)";
            // 
            // ObjectView
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Location = new System.Drawing.Point(11, 71);
            this.Name = "ObjectGridView";
            this.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RowHeadersVisible = false;
            this.RowTemplate.Height = 21;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Size = new System.Drawing.Size(178, 170);
            this.StandardTab = true;
            this.TabIndex = 400;
            this.TabStop = false;
            this.objectViewContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip objectViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem displayDataMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem createSqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSelectStmtMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createInsertStmtMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDeleteStmtMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editResultMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem createSelectCountStmtMenuItem;
    }
}
