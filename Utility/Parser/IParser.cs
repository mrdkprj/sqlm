
namespace MasudaManager
{
    public interface IParser<T>
    {
        T Parse(string input);
    }
}
