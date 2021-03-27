using MasudaManager.DataAccess.OracleDatabase;

namespace MasudaManager.DataAccess
{
    public static class DataAccessProvider
    {
        static string _providerName = null;
        static IDataAccess _dataAccess;

        static DataAccessProvider()
        {
            _providerName = System.Configuration.ConfigurationManager.ConnectionStrings["DataAccess"].ProviderName;
            if (_providerName == Constants.RDBMS.Oracle)
                _dataAccess = new OracleDataAccess();
            else
                _dataAccess = new OracleDataAccess();
        }
        
        public static string ProviderName { get { return _providerName; } }

        public static IDataAccess GetDataAccess()
        {
            return _dataAccess;
        }
    }
}
