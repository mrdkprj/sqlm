using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    public interface IObserverContext<TContext>
    {
        IView View { get; set; }
        void RegisterCallBack(Action<TContext> callbackMethod);
        void ChangeStrategy(TContext context);
        void Act(ObserverActionType actionType);
        void ActOnUpdate();
        void ActOnComplete();
        void ActOnError();
    }
}
