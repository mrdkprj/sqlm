using System;

namespace MasudaManager
{
    public interface IObserver
    {
        void Update(object sender);
        void Complete(object sender);
        void Error(object sender, Exception e);
    }
}
