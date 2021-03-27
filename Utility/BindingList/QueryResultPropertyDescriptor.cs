using System;
using System.ComponentModel;

namespace MasudaManager.Utility
{
    class QueryResultPropertyDescriptor : PropertyDescriptor
    {
        Type _propertyType;
        int _propertyId;
        string _changeTypeErrorFormat = "PropertyDescriptor: ChangeType exception [{0}: {1}]";

        public QueryResultPropertyDescriptor(string name, int id) : base(name, null)
        {
            _propertyId = id;
        }

        public override Type PropertyType { get { return _propertyType; } }

        public override bool IsReadOnly { get { return false; } }

        public override Type ComponentType { get { return typeof(QueryResult); } }

        public override object GetValue(object component)
        {
            if (((QueryResult)component)[_propertyId] == null)
                return null;

            try
            {
                return Convert.ChangeType(((QueryResult)component)[_propertyId], _propertyType);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(String.Format(_changeTypeErrorFormat, e.GetType().FullName, e.Message));
                return ((QueryResult)component)[_propertyId];
            }
        }

        public override void SetValue(object component, object value)
        {
            ((QueryResult)component)[_propertyId] = Convert.ToString(value);
        }

        public override void ResetValue(object component)
        {
            ((QueryResult)component)[_propertyId] = null;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return ((QueryResult)component)[_propertyId] != null;
        }

        public void SetPropertyType(Type propertyType)
        {
            _propertyType = propertyType;
        }
    }
}
