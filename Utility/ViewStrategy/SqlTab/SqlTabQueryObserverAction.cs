using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Utility.Preference;
using MasudaManager.Views;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    class SqlTabQueryObserverAction : IObserverActionStrategy<ISqlTask>
    {
        ISqlTabView _view;
        bool _callbackRequired = false;

        static SqlTabQueryObserverAction _instance = new SqlTabQueryObserverAction();
        static SqlTabQueryObserverAction() { }
        private SqlTabQueryObserverAction() { }
        public static SqlTabQueryObserverAction Instance
        {
            get { return _instance; }
        }

        public bool CallBackRequired { get { return _callbackRequired; } }

        public IView View
        {
            get { return _view; }
            set
            {
                if (value is ISqlTabView)
                    _view = value as ISqlTabView;
                else
                    _view = null;
            }
        }

        public void ActOnUpdate(ISqlTask context)
        {
            BeginBind(context);
            _view.BringResultViewFront(context.Guid);
            //_callbackRequired = true;
            _callbackRequired = UserPreference.Setting.Sql.DisplayProgress;
        }

        public void ActOnComplete(ISqlTask context)
        {
            BeginBind(context);
            CompleteBind(context);
            WriteLogView(context.Guid, context.ExcutedSqlInfo.InputSql, true, false);
            WriteLogView(context.Guid, context.ExcutedSqlInfo.SqlMessage, false, true);
            if (!_view.InputViewFocused(context.Guid))
                _view.SetResultViewFocus(context.Guid);
            _callbackRequired = true;
        }

        public void ActOnError(ISqlTask context)
        {
            BeginBind(context);
            CompleteBind(context);
            WriteLogView(context.Guid, context.ExcutedSqlInfo.InputSql, true, false);

            if (context.Status == SqlTaskStatus.Cancelled)
                WriteLogView(context.Guid, context.ExcutedSqlInfo.SqlMessage, false, true);
            else
                WriteLogView(context.Guid, context.ExcutedSqlInfo.SqlException.Message, false, true);

            if(context.Status != SqlTaskStatus.Cancelled)
                _view.BringLogViewFront(context.Guid);

            if (_view.InputViewFocused(context.Guid))
                _view.SetLogViewFocus(context.Guid);

            _callbackRequired = true;
        }

        void BeginBind(ISqlTask context)
        {
            if (!UserPreference.Setting.Sql.DisplayProgress)
                return;

            if (IsViewAlreadyBound(context))
                return;

            _view.SetResultViewDataSource(context.Guid, context.BindingModel);
            _view.AdjustResultViewColumnHeaderHeight(context.Guid);
        }

        void CompleteBind(ISqlTask context)
        {
            if (UserPreference.Setting.Sql.DisplayProgress)
                _view.AdjustResultViewRowHeaderWidth(context.Guid);
            else
                BindOnQueryComplete(context);
        }

        bool IsViewAlreadyBound(ISqlTask context)
        {
            return object.ReferenceEquals(_view.GetResultViewDataSource(context.Guid), context.BindingModel);
        }

        void BindOnQueryComplete(ISqlTask context)
        {
            _view.SuspendResultViewSettings(context.Guid);
            _view.SetResultViewDataSource(context.Guid, context.BindingModel);
            _view.ResumeResultViewSettings(context.Guid);
        }

        void WriteLogView(object guid, string message, bool writeDate, bool writeLine)
        {
            if (writeDate)
                _view.SetLogViewText(guid, message, DateTime.Now, writeLine);
            else
                _view.SetLogViewText(guid, message, null, writeLine);
        }
    }
}
