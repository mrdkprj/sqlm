using System;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public interface IImportStrategy : IObservable
    {
        bool IsBusy { get; }

        int ImportCount { get; }

        int ImportProgress { get; }

        void Cancel();

        Task Import();

        void Release(IObserver observer);
    }
}
