using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Presenters
{
    public static class PresenterTokens
    {
        public static readonly string ConnectionOpenToken = "ConnectionOpenToken";
        public static readonly string ConnectionChangedToken = "ConnectionChangedToken";
        public static readonly string ConnectionCloseToken = "ConnectionCloseToken";
        public static readonly string ForceDisconnectToken = "ForceDisconnectToken";
        
        public static readonly string SqlTabViewApplySettingToken = "SqlTabViewApplySettingToken";
        public static readonly string InputViewApplySettingToken = "InputViewApplySettingToken";
        public static readonly string ResultViewApplySettingToken = "ResultViewApplySettingToken";
        public static readonly string DbObjectInfoViewApplySettingToken = "DbObjectInfoViewApplySettingToken";

        public static readonly string RequestDisposeToken = "RequestDisposeToken";
        public static readonly string RejectDisposeToken = "RejectDisposeToken";
        public static readonly string AcceptDisposeToken = "AcceptDisposeToken";

        public static readonly string RequestDisconnectToken = "RequestDisconnectToken";
        public static readonly string RejectDisconnectToken = "RejectDisposeToken";
        public static readonly string AcceptDisconnectToken = "AcceptDisposeToken";

        public static readonly string ShowExportViewToken = "ShowExportViewToken";
        public static readonly string ShowImportViewToken = "ShowImportViewToken";
        public static readonly string ShowEditSqlResults = "ShowEditSqlResults";
        public static readonly string ShowSearchViewToken = "ShowSearchViewToken";
        public static readonly string SqlTaskStartToken = "SqlTaskStartToken";
        public static readonly string SqlTaskCompleteToken = "SqlTaskCompleteToken";
        public static readonly string SqlTaskStatusChangedToken = "SqlTaskStatusChangeToken";

        public static readonly string AddSqlTabToken = "AddSqlTabToken";
        public static readonly string SelectedTabChangedToken = "SelectedTabChangedToken";
        public static readonly string InsertTextToken = "InsertTextToken";
        public static readonly string OpenFileToken = "OpenFileToken";
        public static readonly string SaveFileToken = "SaveFileToken";
        public static readonly string ShowSaveFileDialogToken = "ShowSaveFileDialogToken";
        public static readonly string ExecuteSqlToken = "ExecuteSqlToken";
        public static readonly string DisplayDataToken = "DisplayDataToken";
        public static readonly string CancelSqlToken = "CancelSqlToken";

        public static readonly string ExecuteSearchToken = "ExecuteSearchToken";
        public static readonly string RequestSearchContextToken = "RequestSearchContextToken";
        public static readonly string SearchContextPreparedToken = "SearchContextPrepared";
        public static readonly string SearchCompleteToken = "SearchCompleteToken";
        
        public static readonly string DisposeDbObjectSqlTaskToken = "DisposeDbObjectSqlTaskToken";
        public static readonly string RefreshObjectViewToken = "RefreshObjectViewToken";
        public static readonly string CopyToObjectViewFilterToken = "CopyToObjectViewFilterToken";
        public static readonly string CopyToPropertyViewFilterToken = "CopyToPropertyViewFilterToken";
        public static readonly string CopyFromObjectViewToken = "CopyFromObjectViewToken";

        public static readonly string InputSettingToken = "InputSettingToken";
        public static readonly string OutputSettingToken = "OutputSettingToken";
        public static readonly string ListSettingToken = "ListSettingToken";
        public static readonly string TabSettingToken = "TabSettingToken";
    }
}
