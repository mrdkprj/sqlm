using MasudaManager.Views;
using System;

namespace MasudaManager.Presenters
{
    public static class SearchDialogPresenter
    {
        static SearchGridView _dialog = new SearchGridView();

        static SearchDialogPresenter()
        {
            _dialog.Visible = false;
        }

        public static SearchGridView Dialog { get { return _dialog; } }

        public static void DisposeDialog()
        {
            if (_dialog == null)
                return;

            _dialog.DisposeDialog();
            _dialog = null;
        }
    }
}
