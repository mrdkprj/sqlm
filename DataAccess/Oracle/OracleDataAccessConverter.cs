using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace MasudaManager.DataAccess.OracleDatabase
{
    class OracleDataAccessConverter : IDataAccessConverter
    {
        static OracleDataAccessConverter _instance = new OracleDataAccessConverter();
        static OracleDataAccessConverter() { }
        private OracleDataAccessConverter() { }
        public static OracleDataAccessConverter GetInstance()
        {
            return _instance;
        }

        static Dictionary<Type, Type> _typeDictionary = new Dictionary<Type, Type>()
        {
            {typeof(OracleBFile), typeof(object)},
            {typeof(OracleBinary), typeof(object)},
            {typeof(OracleBlob), typeof(object)},
            {typeof(OracleClob), typeof(object)},
            {typeof(OracleDate), typeof(DateTime)},
            {typeof(OracleDecimal), typeof(decimal)},
            {typeof(OracleTimeStamp), typeof(object)},
            {typeof(OracleTimeStampLTZ), typeof(object)},
            {typeof(OracleTimeStampTZ), typeof(object)},
            {typeof(OracleIntervalYM), typeof(object)},
            {typeof(OracleIntervalDS), typeof(TimeSpan)},
            {typeof(OracleString), typeof(string)},
            {typeof(OracleRefCursor), typeof(object)},
            {typeof(OracleXmlStream), typeof(object)},
            {typeof(OracleXmlType), typeof(object)}
       };

        public Type GetBindableType(Type sourceType)
        {
            return _typeDictionary.GetValueOrDefault(sourceType);
        }

        public Type GetBindableType(DbDataReader reader, int index)
        {
            OracleDataReader oracleReader = reader as OracleDataReader;
            
            Type oracleType = oracleReader.GetProviderSpecificFieldType(index);

            return _typeDictionary.GetValueOrDefault(oracleType);
        }

        public string GetString(object value)
        {
            if (value is INullable && (value as INullable).IsNull)
                    return null;

            if (value.GetType().Equals(typeof(OracleClob)))
                return GetClobString((OracleClob)value);

            if (IsByteArray(value.GetType()))
                return RemoveExtraBytesString(BitConverter.ToString(GetByteArray(value)));           

            return Convert.ToString(value);
        }

        public string GetString(DbDataReader reader, int index)
        {
            OracleDataReader oracleReader = reader as OracleDataReader;

            object value = oracleReader.GetValue(index);
            object oracleValue = oracleReader.GetOracleValue(index);
            //if (Convert.IsDBNull(oracleReader.GetValue(index)))
            //    return null;

            //if (oracleReader.GetOracleValue(index).GetType().Equals(typeof(OracleClob)))
            //    return GetClobString((OracleClob)oracleReader.GetOracleValue(index)); 
            
            //if (IsByteArray(oracleReader.GetOracleValue(index).GetType()))
            //    return RemoveExtraBytesString(BitConverter.ToString((byte[])oracleReader.GetValue(index)));

            //return oracleReader.GetOracleValue(index).ToStringOrNull();
            if (Convert.IsDBNull(value))
                return null;

            if (oracleValue.GetType().Equals(typeof(OracleClob)))
                return GetClobString((OracleClob)oracleValue);

            if (IsByteArray(oracleValue.GetType()))
                return RemoveExtraBytesString(BitConverter.ToString((byte[])value));

            return oracleValue.ToStringOrNull();
        }

        bool IsByteArray(Type oracleType)
        {
            if (oracleType.Equals(typeof(OracleBFile)))
                return true;

            if (oracleType.Equals(typeof(OracleBlob)))
                return true;

            if (oracleType.Equals(typeof(OracleBinary)))
                return true;

            return false;
        }

        byte[] GetByteArray(object bytesData)
        {
            if (bytesData.GetType().Equals(typeof(OracleBFile)))
                return GetByteArray((OracleBFile)bytesData);

            if (bytesData.GetType().Equals(typeof(OracleBinary)))
                return GetByteArray((OracleBinary)bytesData);

            if (bytesData.GetType().Equals(typeof(OracleBlob)))
                return GetByteArray((OracleBlob)bytesData);

            throw new Exception("Failed to convert byte array");
        }

        byte[] GetByteArray(OracleBFile oracleBFile)
        {
            return oracleBFile.Value;
        }

        byte[] GetByteArray(OracleBinary oracleBinary)
        {       
            return oracleBinary.Value;
        }

        byte[] GetByteArray(OracleBlob oracleBlob)
        {
            return oracleBlob.Value;
        }

        string GetClobString(OracleClob oracleClob)
        {
            return oracleClob.Value;
        }

        string RemoveExtraBytesString(string bytesString)
        {
            return bytesString.Replace("-", "");
        }

    }
}
