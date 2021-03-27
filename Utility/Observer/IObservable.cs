using System;

namespace MasudaManager
{
    public interface IObservable
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();

        void NotifyComplete();

        void NotifyError(Exception e);

    }
}
