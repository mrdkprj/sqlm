using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface IDbObjectInfoView : IView<DbObjectInfoModel>
    {
        event EventHandler Loaded;
        event EventHandler CurrentDbObjectNameRequested;
        event EventHandler ObjectListSelectionChanged;
        event EventHandler ObjectViewSelectionChanged;
        event EventHandler<MouseEventArgs> ObjectViewItemDoubleClicked;
        event EventHandler ObjectViewFilterTextChanged;
        event EventHandler RefreshButtonClicked;
        event EventHandler DisplayDataClicked;
        event EventHandler CreateSelectStmtClicked;
        event EventHandler CreateSelectCountStmtClicked;
        event EventHandler CreateInsertStmtClicked;
        event EventHandler CreateDeleteStmtClicked;
        event EventHandler EditResultClicked;
        event EventHandler ExportClicked;
        event EventHandler ImportClicked;
        event EventHandler<MouseEventArgs> PropertyViewItemDoubleClicked;
        event EventHandler PropertyViewFilterTextChanged;
      
        void FocusOnObjectViewFilter();
        string GetSelectedDbObjectName();
        void ApplySettingToObjectView();
        void ApplySettingToPropertyView();

        ISynchronizeInvoke GetInvoker();
        
        void DisableContextMenu();
        void EnableContextMenu(); 
        bool RefreshButtonEnabled { get; set; }
        bool DisplayDataEnabled { get; set; }
        bool CreateSqlEnabled { get; set; }
        bool EditResultEnabled { get; set; }
        bool ExportEnabled { get; set; }
        bool ImportEnabled { get; set; }

        bool IsObjectListDataBound();
        int ObjectListSelectedIndex { get; }
        void SetObjectListDataSource(object datasource);

        void AdjustObjectViewColumnsWidth(DataGridViewAutoSizeColumnMode autoSizeMode);
        object GetObjectViewCellValue(Cell cell);
        Cell ObjectViewCurrentCell { get; }
        object ObjectViewCurrentValue { get; }
        string ObjectViewFilterText { get; set; }
        void SetObjectViewDataSource(object datasource);

        void AdjustPropertyViewColumnsWidth(DbObjectPropertyType dbObjectPropertyType, DataGridViewAutoSizeColumnMode autoSizeMode);
        object GetPropertyViewCellValue(Cell cell);
        Cell PropertyViewCurrentCell { get; }
        object PropertyViewCurrentValue { get; }
        string PropertyViewFilterText { get; set; }
        void ClearPropertyViews(DbObjectPropertyType dbObjectPropertyType);
        DbObjectPropertyType SelectedPropertyType { get; }
        void SetVisibleProperty(DbObjectPropertyType dbObjectPropertyType);
        void SetPropertyViewObjectName(string objectName);
     
        void SetColumnViewDataSource(object datasource);
        void SetIndexViewDataSource(object datasource);
        void SetConstraintViewDataSource(object datasource);
        void SetIndexColumnViewDataSource(object datasource);
        void SetPropertyViewDataSource(object datasource);
    }
}
