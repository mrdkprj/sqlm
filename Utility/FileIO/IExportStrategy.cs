using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public interface IExportStrategy : IObservable
    {
        bool IsBusy { get; }

        int ExportCount { get; }

        int ExportProgress { get; }

        void Cancel();

        Task Export();

        void Release(IObserver observer);
    }
}
