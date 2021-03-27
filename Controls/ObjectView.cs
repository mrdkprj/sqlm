using MasudaManager.Utility.Preference;
using System;
using System.ComponentModel;

namespace MasudaManager.Controls
{
    public partial class ObjectView : XDataGridView, IMsdControl
    {
        public event EventHandler DisplayDataClick;
        public event EventHandler CreateSelectStmtClick;
        public event EventHandler CreateSelectCountStmtClick;
        public event EventHandler CreateInsertStmtClick;
        public event EventHandler CreateDeleteStmtClick;
        public event EventHandler ExportClick;
        public event EventHandler ImportClick;
        public event EventHandler EditResultClick;

        public ObjectView()
        {
            InitializeComponent();

            this.AllowRightClickCellSelect = true;
            this.ContextMenuStrip = this.objectViewContextMenu;

            PrepareContextMenu();
        }

        #region Property
        public bool AllowDisplayData
        {
            get { return this.displayDataMenuItem.Enabled; }
            set
            {
                if (this.ContextMenuStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.displayDataMenuItem.Enabled = value));
                else
                    this.displayDataMenuItem.Enabled = value;
            }
        }

        public bool AllowCreateSql
        {
            get { return this.createSqlToolStripMenuItem.Enabled; }
            set
            {
                if (this.ContextMenuStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.createSqlToolStripMenuItem.Enabled = value));
                else
                    this.createSqlToolStripMenuItem.Enabled = value;
            }
        }

        public bool AllowEditData
        {
            get { return this.editResultMenuItem.Enabled; }
            set
            {
                if (this.ContextMenuStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.editResultMenuItem.Enabled = value));
                else
                    this.editResultMenuItem.Enabled = value;
            }
        }

        public bool AllowExport
        {
            get { return this.exportMenuItem.Enabled; }
            set
            {
                if (this.ContextMenuStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.exportMenuItem.Enabled = value));
                else
                    this.exportMenuItem.Enabled = value;
            }
        }

        public bool AllowImport
        {
            get { return this.importMenuItem.Enabled; }
            set
            {
                if (this.ContextMenuStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.importMenuItem.Enabled = value));
                else
                    this.importMenuItem.Enabled = value;
            }
        }
        #endregion

        #region Event handlers
        void OnDisplayDataClick(object sender, EventArgs e)
        {
            if (this.DisplayDataClick != null)
                this.DisplayDataClick(sender, e);
        }

        void OnCopyClick(object sender, EventArgs e)
        {
            this.CopyPlainText();
        }

        void OnCreateSelectStmtClick(object sender, EventArgs e)
        {
            if (this.CreateSelectStmtClick != null)
                this.CreateSelectStmtClick(sender, e);
        }

        void OnCreateSelectCountStmtClick(object sender, EventArgs e)
        {
            if (this.CreateSelectCountStmtClick != null)
                this.CreateSelectCountStmtClick(sender, e);
        }

        void OnCreateInsertStmtClick(object sender, EventArgs e)
        {
            if (this.CreateInsertStmtClick != null)
                this.CreateInsertStmtClick(sender, e);
        }

        void OnCreateDeleteStmtClick(object sender, EventArgs e)
        {
            if (this.CreateDeleteStmtClick != null)
                this.CreateDeleteStmtClick(sender, e);
        }

        void OnExportClick(object sender, EventArgs e)
        {
            if (this.ExportClick != null)
                this.ExportClick(sender, e);
        }

        void OnImportClick(object sender, EventArgs e)
        {
            if (this.ImportClick != null)
                this.ImportClick(sender, e);
        }

        void OnEditResultClick(object sender, EventArgs e)
        {
            if (this.EditResultClick != null)
                this.EditResultClick(sender, e);
        }
        #endregion

        #region ContextMenu
        void PrepareContextMenu()
        {
            this.displayDataMenuItem.Text = LocalizedTextProvider.ContextMenu.DisplayData;
            this.createSqlToolStripMenuItem.Text = LocalizedTextProvider.ContextMenu.CreateSql;
            this.copyMenuItem.Text = LocalizedTextProvider.ContextMenu.Copy;
            this.exportMenuItem.Text = LocalizedTextProvider.ContextMenu.Export;
            this.importMenuItem.Text = LocalizedTextProvider.ContextMenu.Import;
            this.editResultMenuItem.Text = LocalizedTextProvider.ContextMenu.Edit; 
            
            this.displayDataMenuItem.Click += OnDisplayDataClick;
            this.createSelectStmtMenuItem.Click += OnCreateSelectStmtClick;
            this.createSelectCountStmtMenuItem.Click += OnCreateSelectCountStmtClick;
            this.copyMenuItem.Click += OnCopyClick;
            this.createInsertStmtMenuItem.Click += OnCreateInsertStmtClick;
            this.createDeleteStmtMenuItem.Click += OnCreateDeleteStmtClick;
            this.exportMenuItem.Click += OnExportClick;
            this.importMenuItem.Click += OnImportClick;
            this.editResultMenuItem.Click += OnEditResultClick;
        }
        #endregion

        #region Disalbe/Enable ContextMenu
        public void DisableContextMenu()
        {
            this.ContextMenuStrip = null;
        }

        public void EnableContextMenu()
        {
            this.ContextMenuStrip = this.objectViewContextMenu;
        }
        #endregion

        #region ApplyPreference
        public void ApplyPreference()
        {
            this.Font = UserPreference.Setting.List.Font;
        }
        #endregion
    }
}
