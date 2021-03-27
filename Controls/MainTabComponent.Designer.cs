namespace MasudaManager.Controls
{
    partial class MainTabComponent
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
            this.InputViewCopyFromObjectViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.XtcInputView = new MasudaManager.Controls.SqlInputView();
            this.XtcResultView = new MasudaManager.Controls.SqlResultView();
            this.XtcLogView = new MasudaManager.Controls.SqlLogView();
            this.InputViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.InputViewCutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewPasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.InputViewCopyToObjectViewFilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewCopyToPropertyViewFilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.InputViewZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewResetZoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ResultViewAutoResizeHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ResultViewCopyTextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultViewCopyHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultViewCopyTextWithHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ResultViewEditResultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ResultViewClearResultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultViewSwitchViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewToUpperMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputViewToLowerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XtcResultView)).BeginInit();
            this.InputViewContextMenu.SuspendLayout();
            this.ResultViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputViewCopyFromObjectViewMenuItem
            // 
            this.InputViewCopyFromObjectViewMenuItem.Name = "InputViewCopyFromObjectViewMenuItem";
            this.InputViewCopyFromObjectViewMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewCopyFromObjectViewMenuItem.Text = "Copy from Object View";
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
            this.splitContainer1.Panel1.Controls.Add(this.XtcInputView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.XtcResultView);
            this.splitContainer1.Panel2.Controls.Add(this.XtcLogView);
            this.splitContainer1.Size = new System.Drawing.Size(150, 150);
            this.splitContainer1.SplitterDistance = 72;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // XtcInputView
            // 
            this.XtcInputView.AdditionalCaretsBlink = false;
            this.XtcInputView.AdditionalCaretsVisible = false;
            this.XtcInputView.DisplayControlChar = false;
            this.XtcInputView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XtcInputView.FilePath = null;
            this.XtcInputView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.XtcInputView.Lexer = ScintillaNET.Lexer.Sql;
            this.XtcInputView.Location = new System.Drawing.Point(0, 0);
            this.XtcInputView.MouseDwellTime = 100;
            this.XtcInputView.Name = "XtcInputView";
            this.XtcInputView.Size = new System.Drawing.Size(150, 72);
            this.XtcInputView.TabIndex = 101;
            this.XtcInputView.WhitespaceSize = 2;
            this.XtcInputView.WrapMode = ScintillaNET.WrapMode.Char;
            // 
            // XtcResultView
            // 
            this.XtcResultView.AllowRightClickCellSelect = false;
            this.XtcResultView.AllowUserToAddRows = false;
            this.XtcResultView.AllowUserToDeleteRows = false;
            this.XtcResultView.AllowUserToResizeRows = false;
            this.XtcResultView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.XtcResultView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.XtcResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.XtcResultView.DefaultColumnWidth = 100;
            this.XtcResultView.DisplayRowNumber = false;
            this.XtcResultView.DisplaySpaceCharacter = false;
            this.XtcResultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XtcResultView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.XtcResultView.ForcePlainTextCopy = true;
            this.XtcResultView.Location = new System.Drawing.Point(0, 0);
            this.XtcResultView.Name = "XtcResultView";
            this.XtcResultView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.XtcResultView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.XtcResultView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.XtcResultView.RowTemplate.Height = 21;
            this.XtcResultView.Size = new System.Drawing.Size(150, 74);
            this.XtcResultView.StandardTab = true;
            this.XtcResultView.TabIndex = 200;
            this.XtcResultView.ThrowOnDataError = false;
            this.XtcResultView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtcGridView_KeyDown);
            // 
            // XtcLogView
            // 
            this.XtcLogView.BackColor = System.Drawing.SystemColors.Window;
            this.XtcLogView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.XtcLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XtcLogView.HideSelection = false;
            this.XtcLogView.IsOverwriteMode = false;
            this.XtcLogView.Location = new System.Drawing.Point(0, 0);
            this.XtcLogView.Name = "XtcLogView";
            this.XtcLogView.ReadOnly = true;
            this.XtcLogView.Size = new System.Drawing.Size(150, 74);
            this.XtcLogView.TabIndex = 200;
            this.XtcLogView.Text = "";
            this.XtcLogView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtcLogView_KeyDown);
            // 
            // InputViewContextMenu
            // 
            this.InputViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InputViewCutMenuItem,
            this.InputViewCopyMenuItem,
            this.InputViewPasteMenuItem,
            this.toolStripSeparator1,
            this.InputViewCopyFromObjectViewMenuItem,
            this.InputViewCopyToObjectViewFilterMenuItem,
            this.InputViewCopyToPropertyViewFilterMenuItem,
            this.toolStripSeparator2,
            this.InputViewToUpperMenuItem,
            this.InputViewToLowerMenuItem,
            this.InputViewZoomInMenuItem,
            this.InputViewZoomOutMenuItem,
            this.InputViewResetZoomMenuItem});
            this.InputViewContextMenu.Name = "contextMenuStrip3";
            this.InputViewContextMenu.Size = new System.Drawing.Size(239, 280);
            // 
            // InputViewCutMenuItem
            // 
            this.InputViewCutMenuItem.Name = "InputViewCutMenuItem";
            this.InputViewCutMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewCutMenuItem.Text = "Cut";
            // 
            // InputViewCopyMenuItem
            // 
            this.InputViewCopyMenuItem.Name = "InputViewCopyMenuItem";
            this.InputViewCopyMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewCopyMenuItem.Text = "Copy";
            // 
            // InputViewPasteMenuItem
            // 
            this.InputViewPasteMenuItem.Name = "InputViewPasteMenuItem";
            this.InputViewPasteMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewPasteMenuItem.Text = "Paste";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(235, 6);
            // 
            // InputViewCopyToObjectViewFilterMenuItem
            // 
            this.InputViewCopyToObjectViewFilterMenuItem.Name = "InputViewCopyToObjectViewFilterMenuItem";
            this.InputViewCopyToObjectViewFilterMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewCopyToObjectViewFilterMenuItem.Text = "Copy to Object View filter";
            // 
            // InputViewCopyToPropertyViewFilterMenuItem
            // 
            this.InputViewCopyToPropertyViewFilterMenuItem.Name = "InputViewCopyToPropertyViewFilterMenuItem";
            this.InputViewCopyToPropertyViewFilterMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewCopyToPropertyViewFilterMenuItem.Text = "Copy to Property View filter";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(235, 6);
            // 
            // InputViewZoomInMenuItem
            // 
            this.InputViewZoomInMenuItem.Name = "InputViewZoomInMenuItem";
            this.InputViewZoomInMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewZoomInMenuItem.Text = "Zoom in";
            // 
            // InputViewZoomOutMenuItem
            // 
            this.InputViewZoomOutMenuItem.Name = "InputViewZoomOutMenuItem";
            this.InputViewZoomOutMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewZoomOutMenuItem.Text = "Zoom out";
            // 
            // InputViewResetZoomMenuItem
            // 
            this.InputViewResetZoomMenuItem.Name = "InputViewResetZoomMenuItem";
            this.InputViewResetZoomMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewResetZoomMenuItem.Text = "Reset zoom";
            // 
            // ResultViewContextMenu
            // 
            this.ResultViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResultViewAutoResizeHeaderMenuItem,
            this.toolStripSeparator4,
            this.ResultViewCopyTextMenuItem,
            this.ResultViewCopyHeaderMenuItem,
            this.ResultViewCopyTextWithHeaderMenuItem,
            this.toolStripSeparator3,
            this.ResultViewEditResultMenuItem,
            this.toolStripSeparator8,
            this.ResultViewClearResultMenuItem,
            this.ResultViewSwitchViewMenuItem});
            this.ResultViewContextMenu.Name = "contextMenuStrip4";
            this.ResultViewContextMenu.Size = new System.Drawing.Size(207, 176);
            // 
            // ResultViewAutoResizeHeaderMenuItem
            // 
            this.ResultViewAutoResizeHeaderMenuItem.Name = "ResultViewAutoResizeHeaderMenuItem";
            this.ResultViewAutoResizeHeaderMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewAutoResizeHeaderMenuItem.Text = "Adjust header width";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(203, 6);
            // 
            // ResultViewCopyTextMenuItem
            // 
            this.ResultViewCopyTextMenuItem.Name = "ResultViewCopyTextMenuItem";
            this.ResultViewCopyTextMenuItem.ShortcutKeyDisplayString = "Ctrl + C";
            this.ResultViewCopyTextMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewCopyTextMenuItem.Text = "Copy text";
            // 
            // ResultViewCopyHeaderMenuItem
            // 
            this.ResultViewCopyHeaderMenuItem.Name = "ResultViewCopyHeaderMenuItem";
            this.ResultViewCopyHeaderMenuItem.ShortcutKeyDisplayString = "Ctrl + H";
            this.ResultViewCopyHeaderMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewCopyHeaderMenuItem.Text = "Copy header";
            // 
            // ResultViewCopyTextWithHeaderMenuItem
            // 
            this.ResultViewCopyTextWithHeaderMenuItem.Name = "ResultViewCopyTextWithHeaderMenuItem";
            this.ResultViewCopyTextWithHeaderMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewCopyTextWithHeaderMenuItem.Text = "Copy text with header";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(203, 6);
            // 
            // ResultViewEditResultMenuItem
            // 
            this.ResultViewEditResultMenuItem.Name = "ResultViewEditResultMenuItem";
            this.ResultViewEditResultMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewEditResultMenuItem.Text = "Edit";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(203, 6);
            // 
            // ResultViewClearResultMenuItem
            // 
            this.ResultViewClearResultMenuItem.Name = "ResultViewClearResultMenuItem";
            this.ResultViewClearResultMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewClearResultMenuItem.Text = "Clear";
            // 
            // ResultViewSwitchViewMenuItem
            // 
            this.ResultViewSwitchViewMenuItem.Name = "ResultViewSwitchViewMenuItem";
            this.ResultViewSwitchViewMenuItem.ShortcutKeyDisplayString = "Ctrl + S";
            this.ResultViewSwitchViewMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ResultViewSwitchViewMenuItem.Text = "Switch view";
            // 
            // InputViewToUpperMenuItem
            // 
            this.InputViewToUpperMenuItem.Name = "InputViewToUpperMenuItem";
            this.InputViewToUpperMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewToUpperMenuItem.Text = "To upper case";
            // 
            // InputViewToLowerMenuItem
            // 
            this.InputViewToLowerMenuItem.Name = "InputViewToLowerMenuItem";
            this.InputViewToLowerMenuItem.Size = new System.Drawing.Size(238, 22);
            this.InputViewToLowerMenuItem.Text = "To lower case";
            // 
            // MainTabComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainTabComponent";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.XtcResultView)).EndInit();
            this.InputViewContextMenu.ResumeLayout(false);
            this.ResultViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SqlResultView XtcResultView;
        private SqlLogView XtcLogView;
        private System.Windows.Forms.ContextMenuStrip InputViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem InputViewCopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewPasteMenuItem;
        private System.Windows.Forms.ContextMenuStrip ResultViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ResultViewCopyHeaderMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem ResultViewClearResultMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResultViewSwitchViewMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem InputViewCopyToObjectViewFilterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewCopyToPropertyViewFilterMenuItem;
        private SqlInputView XtcInputView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem InputViewZoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewZoomOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewResetZoomMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResultViewCopyTextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResultViewCopyTextWithHeaderMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ResultViewEditResultMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewCutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewCopyFromObjectViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResultViewAutoResizeHeaderMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem InputViewToUpperMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputViewToLowerMenuItem;
    }
}
