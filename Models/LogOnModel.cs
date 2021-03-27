
namespace MasudaManager
{
    public class LogOnModel : IModel
    {
        public ConnectionData InitialConnectionData { get; set; }
        public ConnectionData NewConnectionData { get; set; }

        public void ReleaseModel()
        {
        }
    }
}
