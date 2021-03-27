
namespace MasudaManager.Utility
{
    public interface IMessageProvider
    {
        string GetMessage();
        string GetMessage(SqlBaseInfo sqlBaseInfo);
    }
}
