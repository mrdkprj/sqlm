using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    public interface IObserverActionStrategy<TContext>
    {
        IView View { get; set; }
        bool CallBackRequired { get; }
        void ActOnUpdate(TContext context);
        void ActOnComplete(TContext context);
        void ActOnError(TContext context);
    }
}
