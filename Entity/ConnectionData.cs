using System;

namespace MasudaManager
{
    [Serializable]
    public class ConnectionData : IEquatable<ConnectionData>
    {
        public string DataSource { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Mode { get; set; }
        public bool IsConnected { get; set; }
        public string ConnectionString { get; set; }

        public bool Equals(ConnectionData other)
        {
            if (DataSource != other.DataSource)
                return false;

            if (this.UserId != other.UserId)
                return false;

            if (this.Password != other.Password)
                return false;

            if (this.Mode != other.Mode)
                return false;

            return true;
        }
    }
}
