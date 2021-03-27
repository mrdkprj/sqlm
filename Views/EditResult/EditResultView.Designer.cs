namespace MasudaManager.Views
{
    partial class EditResultView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditResultView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.editResultContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolStrip = new System.Windows.Forms.ToolStrip();
            this.btApply = new System.Windows.Forms.ToolStripButton();
            this.btCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btInsert = new System.Windows.Forms.ToolStripButton();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btBulkInsert = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btUndo = new System.Windows.Forms.ToolStripButton();
            this.btRedo = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainGrid = new MasudaManager.Controls.EditableDataGridView();
            this.bulkInsertNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.editResultContextMenu.SuspendLayout();
            this.menuToolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkInsertNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // editResultContextMenu
            // 
            this.editResultContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator3,
            this.addRowToolStripMenuItem,
            this.addRowsToolStripMenuItem,
            this.deleteRowToolStripMenuItem});
            this.editResultContextMenu.Name = "contextMenuStrip1";
            this.editResultContextMenu.Size = new System.Drawing.Size(150, 120);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(146, 6);
            // 
            // addRowToolStripMenuItem
            // 
            this.addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            this.addRowToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.addRowToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.addRowToolStripMenuItem.Text = "Add row (&A)";
            this.addRowToolStripMenuItem.Click += new System.EventHandler(this.addRowToolStripMenuItem_Click);
            // 
            // addRowsToolStripMenuItem
            // 
            this.addRowsToolStripMenuItem.Name = "addRowsToolStripMenuItem";
            this.addRowsToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.addRowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.addRowsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.addRowsToolStripMenuItem.Text = "Add rows";
            this.addRowsToolStripMenuItem.Click += new System.EventHandler(this.addRowsToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete row (&D)";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // menuToolStrip
            // 
            this.menuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btApply,
            this.btCancel,
            this.toolStripSeparator1,
            this.btInsert,
            this.btDelete,
            this.toolStripSeparator4,
            this.btBulkInsert,
            this.toolStripSeparator2,
            this.btUndo,
            this.btRedo});
            this.menuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.menuToolStrip.Name = "menuToolStrip";
            this.menuToolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.menuToolStrip.Size = new System.Drawing.Size(666, 25);
            this.menuToolStrip.TabIndex = 1;
            this.menuToolStrip.Text = "toolStrip1";
            this.menuToolStrip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseClick);
            // 
            // btApply
            // 
            this.btApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btApply.Image = ((System.Drawing.Image)(resources.GetObject("btApply.Image")));
            this.btApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(23, 22);
            this.btApply.Text = "Apply changes";
            this.btApply.ToolTipText = "Apply changes (Ctrl + S)";
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // btCancel
            // 
            this.btCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCancel.Image = ((System.Drawing.Image)(resources.GetObject("btCancel.Image")));
            this.btCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(23, 22);
            this.btCancel.Text = "Cancel";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btInsert
            // 
            this.btInsert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btInsert.Image = ((System.Drawing.Image)(resources.GetObject("btInsert.Image")));
            this.btInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(23, 22);
            this.btInsert.Text = "Add row";
            this.btInsert.Click += new System.EventHandler(this.btAddRow_Click);
            // 
            // btDelete
            // 
            this.btDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(23, 22);
            this.btDelete.Text = "Delete row";
            this.btDelete.Click += new System.EventHandler(this.btDelRow_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btBulkInsert
            // 
            this.btBulkInsert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btBulkInsert.Image = ((System.Drawing.Image)(resources.GetObject("btBulkInsert.Image")));
            this.btBulkInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btBulkInsert.Name = "btBulkInsert";
            this.btBulkInsert.Size = new System.Drawing.Size(23, 22);
            this.btBulkInsert.Text = "Add rows";
            this.btBulkInsert.Click += new System.EventHandler(this.btAddRows_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btUndo
            // 
            this.btUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btUndo.Image = ((System.Drawing.Image)(resources.GetObject("btUndo.Image")));
            this.btUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btUndo.Name = "btUndo";
            this.btUndo.Size = new System.Drawing.Size(23, 22);
            this.btUndo.Text = "Undo";
            this.btUndo.Click += new System.EventHandler(this.btUndo_Click);
            // 
            // btRedo
            // 
            this.btRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRedo.Image = ((System.Drawing.Image)(resources.GetObject("btRedo.Image")));
            this.btRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRedo.Name = "btRedo";
            this.btRedo.Size = new System.Drawing.Size(23, 22);
            this.btRedo.Text = "Redo";
            this.btRedo.Click += new System.EventHandler(this.btRedo_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(666, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.statusStrip1_MouseClick);
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // MainGrid
            // 
            this.MainGrid.AllowRightClickCellSelect = false;
            this.MainGrid.AllowUserToAddRows = false;
            this.MainGrid.AllowUserToDeleteRows = false;
            this.MainGrid.AllowUserToResizeRows = false;
            this.MainGrid.CausesValidation = false;
            this.MainGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.MainGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MainGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainGrid.ContextMenuStrip = this.editResultContextMenu;
            this.MainGrid.DefaultColumnWidth = 100;
            this.MainGrid.DisplayRowNumber = false;
            this.MainGrid.DisplaySpaceCharacter = false;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.ForcePlainTextCopy = true;
            this.MainGrid.Location = new System.Drawing.Point(0, 25);
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MainGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MainGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.MainGrid.RowTemplate.Height = 21;
            this.MainGrid.Size = new System.Drawing.Size(666, 357);
            this.MainGrid.TabIndex = 4;
            this.MainGrid.ThrowOnDataError = false;
            this.MainGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.MainGrid_CellBeginEdit);
            this.MainGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainGrid_CellEndEdit);
            this.MainGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.MainGrid_DataError);
            this.MainGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainGrid_KeyDown);
            // 
            // bulkInsertNumericUpDown
            // 
            this.bulkInsertNumericUpDown.Location = new System.Drawing.Point(366, 5);
            this.bulkInsertNumericUpDown.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.bulkInsertNumericUpDown.Name = "bulkInsertNumericUpDown";
            this.bulkInsertNumericUpDown.Size = new System.Drawing.Size(46, 19);
            this.bulkInsertNumericUpDown.TabIndex = 5;
            this.bulkInsertNumericUpDown.TabStop = false;
            this.bulkInsertNumericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown1_KeyDown);
            // 
            // EditResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 404);
            this.Controls.Add(this.bulkInsertNumericUpDown);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditResultView";
            this.Text = " SqlGrid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlGrid_FormClosing);
            this.editResultContextMenu.ResumeLayout(false);
            this.menuToolStrip.ResumeLayout(false);
            this.menuToolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bulkInsertNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip editResultContextMenu;
        private System.Windows.Forms.ToolStrip menuToolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btApply;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripButton btInsert;
        private System.Windows.Forms.ToolStripButton btBulkInsert;
        private System.Windows.Forms.ToolStripButton btDelete;
        private Controls.EditableDataGridView MainGrid;
        private System.Windows.Forms.ToolStripButton btUndo;
        private System.Windows.Forms.ToolStripButton btRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.NumericUpDown bulkInsertNumericUpDown;
        private System.Windows.Forms.ToolStripButton btCancel;
    }
}