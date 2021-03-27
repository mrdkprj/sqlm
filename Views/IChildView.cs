using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager
{
    public interface IChildView
    {
        bool SuppressFormClosingEvent { get; set; }

        Action ViewClosedAction { get; set; }
        void Release(object sender, CancelEventArgs e);
    }

    public interface IChildView<TArgs> : IChildView
    {
        event EventHandler<GenericEventArgs<TArgs>> Initiated;

        void Initiate(IWin32Window owner, GenericEventArgs<TArgs> args);
    }
}
