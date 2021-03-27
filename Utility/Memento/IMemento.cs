
namespace MasudaManager.Utility
{
    public interface IMemento<T>
    {
        T Restore();
    }
}
