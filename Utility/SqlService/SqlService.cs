using MasudaManager.DataAccess;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public abstract class SqlService : ISqlService, IObservable
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        Task _task;
        List<IObserver> _observers = new List<IObserver>();
        SqlResult _sqlResult = new SqlResult();
        SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);
        bool _disposed = false;

        #region Constructor
        public SqlService()
        {
        }
        #endregion

        #region Property
        public bool IsBusy { get { return _task == null ? false : !_task.IsCompleted; } }
        public bool NotifyOnUpdate { get; set; }
        public SqlResult SqlResult { get { return _sqlResult; } }
        protected Task Task
        {
            get { return _task; }
            set { _task = value; }
        }

        protected List<IObserver> Observers
        {
            get { return _observers; }
            set { _observers = value; }
        }

        protected SqlResult CurrentSqlResult
        {
            get { return _sqlResult; }
            set { _sqlResult = value; }
        }
        #endregion

        #region Abstract method
        public abstract IEnumerable<SqlResult> ExecuteSql(string sql);
        protected abstract Task Execute(IDbCommand dbCommand, CancellationToken token);
        
        public async Task ExecuteSqlAsync(IDbCommand dbCommand, CancellationToken token)
        {
            dbCommand.CommandTimeout = _dataAccess.DefaultCommandTimeout;

            await Execute(dbCommand, token);
        }
        #endregion

        #region Commit/Rollback
        public bool Commit()
        {
            return _dataAccess.CommitTransaction();
        }

        public bool Rollback()
        {
            return _dataAccess.RollbackTransaction();
        }
        #endregion

        #region Get SqlResult
        protected IEnumerable<SqlResult> GetSqlResultsFromDataTable(DataTable dataTable)
        {
            List<string> columnNames = GetColumnNamesFromDataTable(dataTable).ToList();
            List<Type> columnTypes = GetColumnTypesFromDataTable(dataTable).ToList();

            foreach (DataRow row in dataTable.Rows)
            {
                SqlResult result = new SqlResult();
                result.ColumnNames = columnNames;
                result.ColumnTypes = columnTypes;
                result.RowValues = GetVaulesFromDataRow(row).ToList();
                yield return result;
            }
        }

        protected IEnumerable<string> GetColumnNamesFromDataTable(DataTable dataTable)
        {
            return dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName);
        }

        protected IEnumerable<Type> GetColumnTypesFromDataTable(DataTable dataTable)
        {
            return dataTable.Columns.Cast<DataColumn>().Select(x => _dataAccess.Converter.GetBindableType(x.DataType));
        }

        protected IEnumerable<string> GetVaulesFromDataRow(DataRow dataRow)
        {
            return dataRow.ItemArray.AsEnumerable().Select(x => _dataAccess.Converter.GetString(x));
        }

        protected IEnumerable<string> GetColumnNamesFromReader(DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                yield return reader.GetName(i);
            }
        }

        protected IEnumerable<Type> GetColumnTypesFromReader(DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                yield return _dataAccess.Converter.GetBindableType(reader, i);
            }
        }

        protected IEnumerable<string> GetValuesFromReader(DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                yield return _dataAccess.Converter.GetString(reader, i);
            }
        }
        #endregion

        #region Observable
        public virtual void Attach(IObserver observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }
        }

        public virtual void Detach(IObserver observer)
        {
            lock (_observers)
            {
                _observers.Remove(observer);
            }
        }

        public virtual void Notify()
        {
            if (!this.NotifyOnUpdate)
                return;
            
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }
        }

        public virtual void NotifyComplete()
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.Complete(this);
                }
            }
        }

        public virtual void NotifyError(Exception e)
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.Error(this, e);
                }
            }
        }
        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            _disposed = true;
        }

        #endregion
    }
}
