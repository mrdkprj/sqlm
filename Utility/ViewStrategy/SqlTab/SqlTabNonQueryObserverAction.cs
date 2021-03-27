using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using WinFormsMvp;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Utility
{
    class SqlTabNonQueryObserverAction : IObserverActionStrategy<ISqlTask>
    {
        ISqlTabView _view;
        bool _callbackRequired = false;

        static SqlTabNonQueryObserverAction _instance = new SqlTabNonQueryObserverAction();
        static SqlTabNonQueryObserverAction() { }
        private SqlTabNonQueryObserverAction() { }
        public static SqlTabNonQueryObserverAction Instance
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
            _callbackRequired = false;
        }

        public void ActOnComplete(ISqlTask context)
        {
            WriteLogView(context.Guid, context.ExcutedSqlInfo.InputSql, true, false);
            WriteLogView(context.Guid, context.ExcutedSqlInfo.SqlMessage, false, true);
            _view.BringLogViewFront(context.Guid);

            if (!_view.InputViewFocused(context.Guid))
                _view.SetLogViewFocus(context.Guid);

            if (context.ExcutedSqlInfo.SqlType != SqlType.TCL && UserPreference.Setting.Sql.AllowAutoCommit)
                context.Commit();

            _callbackRequired = true;
        }

        public void ActOnError(ISqlTask context)
        {
            WriteLogView(context.Guid, context.ExcutedSqlInfo.InputSql, true, false);
            WriteLogView(context.Guid, context.ExcutedSqlInfo.SqlMessage, false, true);
            _view.BringLogViewFront(context.Guid);

            if (!_view.InputViewFocused(context.Guid))
                _view.SetLogViewFocus(context.Guid);

            _callbackRequired = true;
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
