namespace MasudaManager.Controls
{
    partial class ObjectPropertyTab
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PropertyTab = new System.Windows.Forms.TabControl();
            this.Property = new System.Windows.Forms.TabPage();
            this.GeneralPropertyGrid = new MasudaManager.Controls.XDataGridView();
            this.Column = new System.Windows.Forms.TabPage();
            this.TableColumnGrid = new MasudaManager.Controls.XDataGridView();
            this.Index = new System.Windows.Forms.TabPage();
            this.TableIndexGrid = new MasudaManager.Controls.XDataGridView();
            this.Constraint = new System.Windows.Forms.TabPage();
            this.TableConstraintGrid = new MasudaManager.Controls.XDataGridView();
            this.IndexColumn = new System.Windows.Forms.TabPage();
            this.IndexColumnGrid = new MasudaManager.Controls.XDataGridView();
            this.propertyTabContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createColumnSelectStmtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createColumUpdateStmMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createColumnDeleteStmtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertyTab.SuspendLayout();
            this.Property.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralPropertyGrid)).BeginInit();
            this.Column.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableColumnGrid)).BeginInit();
            this.Index.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableIndexGrid)).BeginInit();
            this.Constraint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableConstraintGrid)).BeginInit();
            this.IndexColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IndexColumnGrid)).BeginInit();
            this.propertyTabContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PropertyTab
            // 
            this.PropertyTab.Controls.Add(this.Property);
            this.PropertyTab.Controls.Add(this.Column);
            this.PropertyTab.Controls.Add(this.Index);
            this.PropertyTab.Controls.Add(this.Constraint);
            this.PropertyTab.Controls.Add(this.IndexColumn);
            this.PropertyTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyTab.Location = new System.Drawing.Point(0, 0);
            this.PropertyTab.Name = "PropertyTab";
            this.PropertyTab.SelectedIndex = 0;
            this.PropertyTab.Size = new System.Drawing.Size(849, 150);
            this.PropertyTab.TabIndex = 0;
            // 
            // Property
            // 
            this.Property.Controls.Add(this.GeneralPropertyGrid);
            this.Property.Location = new System.Drawing.Point(4, 22);
            this.Property.Name = "Property";
            this.Property.Size = new System.Drawing.Size(841, 124);
            this.Property.TabIndex = 0;
            this.Property.Text = "Property";
            this.Property.UseVisualStyleBackColor = true;
            // 
            // GeneralPropertyGrid
            // 
            this.GeneralPropertyGrid.AllowRightClickCellSelect = false;
            this.GeneralPropertyGrid.AllowUserToAddRows = false;
            this.GeneralPropertyGrid.AllowUserToDeleteRows = false;
            this.GeneralPropertyGrid.AllowUserToResizeRows = false;
            this.GeneralPropertyGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GeneralPropertyGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GeneralPropertyGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GeneralPropertyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GeneralPropertyGrid.DisplayRowNumber = false;
            this.GeneralPropertyGrid.DisplaySpaceCharacter = false;
            this.GeneralPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralPropertyGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GeneralPropertyGrid.ForcePlainTextCopy = true;
            this.GeneralPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.GeneralPropertyGrid.Name = "GeneralPropertyGrid";
            this.GeneralPropertyGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GeneralPropertyGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GeneralPropertyGrid.RowHeadersVisible = false;
            this.GeneralPropertyGrid.RowTemplate.Height = 21;
            this.GeneralPropertyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GeneralPropertyGrid.Size = new System.Drawing.Size(841, 124);
            this.GeneralPropertyGrid.StandardTab = true;
            this.GeneralPropertyGrid.TabIndex = 601;
            this.GeneralPropertyGrid.TabStop = false;
            // 
            // Column
            // 
            this.Column.Controls.Add(this.TableColumnGrid);
            this.Column.Location = new System.Drawing.Point(4, 22);
            this.Column.Name = "Column";
            this.Column.Size = new System.Drawing.Size(841, 124);
            this.Column.TabIndex = 1;
            this.Column.Text = "Column";
            this.Column.UseVisualStyleBackColor = true;
            // 
            // TableColumnGrid
            // 
            this.TableColumnGrid.AllowRightClickCellSelect = false;
            this.TableColumnGrid.AllowUserToAddRows = false;
            this.TableColumnGrid.AllowUserToDeleteRows = false;
            this.TableColumnGrid.AllowUserToResizeRows = false;
            this.TableColumnGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TableColumnGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TableColumnGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.TableColumnGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableColumnGrid.DisplayRowNumber = false;
            this.TableColumnGrid.DisplaySpaceCharacter = false;
            this.TableColumnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableColumnGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableColumnGrid.ForcePlainTextCopy = true;
            this.TableColumnGrid.Location = new System.Drawing.Point(0, 0);
            this.TableColumnGrid.Name = "TableColumnGrid";
            this.TableColumnGrid.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TableColumnGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.TableColumnGrid.RowHeadersVisible = false;
            this.TableColumnGrid.RowTemplate.Height = 21;
            this.TableColumnGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TableColumnGrid.Size = new System.Drawing.Size(841, 124);
            this.TableColumnGrid.StandardTab = true;
            this.TableColumnGrid.TabIndex = 602;
            this.TableColumnGrid.TabStop = false;
            // 
            // Index
            // 
            this.Index.Controls.Add(this.TableIndexGrid);
            this.Index.Location = new System.Drawing.Point(4, 22);
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(841, 124);
            this.Index.TabIndex = 2;
            this.Index.Text = "Index";
            this.Index.UseVisualStyleBackColor = true;
            // 
            // TableIndexGrid
            // 
            this.TableIndexGrid.AllowRightClickCellSelect = false;
            this.TableIndexGrid.AllowUserToAddRows = false;
            this.TableIndexGrid.AllowUserToDeleteRows = false;
            this.TableIndexGrid.AllowUserToResizeRows = false;
            this.TableIndexGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TableIndexGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TableIndexGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.TableIndexGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableIndexGrid.DisplayRowNumber = false;
            this.TableIndexGrid.DisplaySpaceCharacter = false;
            this.TableIndexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableIndexGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableIndexGrid.ForcePlainTextCopy = true;
            this.TableIndexGrid.Location = new System.Drawing.Point(0, 0);
            this.TableIndexGrid.Name = "TableIndexGrid";
            this.TableIndexGrid.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TableIndexGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.TableIndexGrid.RowHeadersVisible = false;
            this.TableIndexGrid.RowTemplate.Height = 21;
            this.TableIndexGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TableIndexGrid.Size = new System.Drawing.Size(841, 124);
            this.TableIndexGrid.StandardTab = true;
            this.TableIndexGrid.TabIndex = 602;
            this.TableIndexGrid.TabStop = false;
            // 
            // Constraint
            // 
            this.Constraint.Controls.Add(this.TableConstraintGrid);
            this.Constraint.Location = new System.Drawing.Point(4, 22);
            this.Constraint.Name = "Constraint";
            this.Constraint.Size = new System.Drawing.Size(841, 124);
            this.Constraint.TabIndex = 3;
            this.Constraint.Text = "Constraint";
            this.Constraint.UseVisualStyleBackColor = true;
            // 
            // TableConstraintGrid
            // 
            this.TableConstraintGrid.AllowRightClickCellSelect = false;
            this.TableConstraintGrid.AllowUserToAddRows = false;
            this.TableConstraintGrid.AllowUserToDeleteRows = false;
            this.TableConstraintGrid.AllowUserToResizeRows = false;
            this.TableConstraintGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TableConstraintGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TableConstraintGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.TableConstraintGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableConstraintGrid.DisplayRowNumber = false;
            this.TableConstraintGrid.DisplaySpaceCharacter = false;
            this.TableConstraintGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableConstraintGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableConstraintGrid.ForcePlainTextCopy = true;
            this.TableConstraintGrid.Location = new System.Drawing.Point(0, 0);
            this.TableConstraintGrid.Name = "TableConstraintGrid";
            this.TableConstraintGrid.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TableConstraintGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.TableConstraintGrid.RowHeadersVisible = false;
            this.TableConstraintGrid.RowTemplate.Height = 21;
            this.TableConstraintGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TableConstraintGrid.Size = new System.Drawing.Size(841, 124);
            this.TableConstraintGrid.StandardTab = true;
            this.TableConstraintGrid.TabIndex = 602;
            this.TableConstraintGrid.TabStop = false;
            // 
            // IndexColumn
            // 
            this.IndexColumn.Controls.Add(this.IndexColumnGrid);
            this.IndexColumn.Location = new System.Drawing.Point(4, 22);
            this.IndexColumn.Name = "IndexColumn";
            this.IndexColumn.Size = new System.Drawing.Size(841, 124);
            this.IndexColumn.TabIndex = 4;
            this.IndexColumn.Text = "Column";
            this.IndexColumn.UseVisualStyleBackColor = true;
            // 
            // IndexColumnGrid
            // 
            this.IndexColumnGrid.AllowRightClickCellSelect = false;
            this.IndexColumnGrid.AllowUserToAddRows = false;
            this.IndexColumnGrid.AllowUserToDeleteRows = false;
            this.IndexColumnGrid.AllowUserToResizeRows = false;
            this.IndexColumnGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.IndexColumnGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IndexColumnGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.IndexColumnGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IndexColumnGrid.DisplayRowNumber = false;
            this.IndexColumnGrid.DisplaySpaceCharacter = false;
            this.IndexColumnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IndexColumnGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.IndexColumnGrid.ForcePlainTextCopy = true;
            this.IndexColumnGrid.Location = new System.Drawing.Point(0, 0);
            this.IndexColumnGrid.Name = "IndexColumnGrid";
            this.IndexColumnGrid.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IndexColumnGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.IndexColumnGrid.RowHeadersVisible = false;
            this.IndexColumnGrid.RowTemplate.Height = 21;
            this.IndexColumnGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.IndexColumnGrid.Size = new System.Drawing.Size(841, 124);
            this.IndexColumnGrid.StandardTab = true;
            this.IndexColumnGrid.TabIndex = 603;
            this.IndexColumnGrid.TabStop = false;
            // 
            // propertyTabContextMenu
            // 
            this.propertyTabContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenuItem,
            this.createSqlToolStripMenuItem});
            this.propertyTabContextMenu.Name = "propertyTabContextMenu";
            this.propertyTabContextMenu.Size = new System.Drawing.Size(130, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyMenuItem.Name = "copyToolStripMenuItem";
            this.copyMenuItem.Size = new System.Drawing.Size(129, 22);
            this.copyMenuItem.Text = "Copy";
            // 
            // createSqlToolStripMenuItem
            // 
            this.createSqlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createColumnSelectStmtMenuItem,
            this.createColumUpdateStmMenuItem,
            this.createColumnDeleteStmtMenuItem});
            this.createSqlToolStripMenuItem.Name = "createSqlToolStripMenuItem";
            this.createSqlToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.createSqlToolStripMenuItem.Text = "Create SQL";
            // 
            // selectToolStripMenuItem
            // 
            this.createColumnSelectStmtMenuItem.Name = "selectToolStripMenuItem";
            this.createColumnSelectStmtMenuItem.Size = new System.Drawing.Size(106, 22);
            this.createColumnSelectStmtMenuItem.Text = "Select";
            // 
            // updateToolStripMenuItem
            // 
            this.createColumUpdateStmMenuItem.Name = "updateToolStripMenuItem";
            this.createColumUpdateStmMenuItem.Size = new System.Drawing.Size(106, 22);
            this.createColumUpdateStmMenuItem.Text = "Update";
            // 
            // deleteToolStripMenuItem
            // 
            this.createColumnDeleteStmtMenuItem.Name = "deleteToolStripMenuItem";
            this.createColumnDeleteStmtMenuItem.Size = new System.Drawing.Size(106, 22);
            this.createColumnDeleteStmtMenuItem.Text = "Delete";
            // 
            // ObjectPropertyTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PropertyTab);
            this.Name = "ObjectPropertyTab";
            this.Size = new System.Drawing.Size(849, 150);
            this.PropertyTab.ResumeLayout(false);
            this.Property.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GeneralPropertyGrid)).EndInit();
            this.Column.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableColumnGrid)).EndInit();
            this.Index.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableIndexGrid)).EndInit();
            this.Constraint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableConstraintGrid)).EndInit();
            this.IndexColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IndexColumnGrid)).EndInit();
            this.propertyTabContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl PropertyTab;
        private System.Windows.Forms.TabPage Property;
        private XDataGridView GeneralPropertyGrid;
        private System.Windows.Forms.TabPage Column;
        private System.Windows.Forms.TabPage Index;
        private System.Windows.Forms.TabPage Constraint;
        private XDataGridView TableColumnGrid;
        private XDataGridView TableIndexGrid;
        private XDataGridView TableConstraintGrid;
        private System.Windows.Forms.TabPage IndexColumn;
        private XDataGridView IndexColumnGrid;
        private System.Windows.Forms.ContextMenuStrip propertyTabContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createColumnSelectStmtMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createColumUpdateStmMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createColumnDeleteStmtMenuItem;

    }
}
