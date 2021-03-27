using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface ISearchGridView : IView<SearchGridModel>
    {
        event EventHandler<ShowSearchGridEventArgs> Initiated;
        event EventHandler DisposeDialogRequested;
        event EventHandler SearchBackwardButtonClicked;
        event EventHandler SearchForwardButtonClicked;

        void AddSearchedString(string searchedString);
        void DisposeDialog();
        SearchOptionFlags GetSearchOption();
        string GetSearchString();
        void HideView();
        SearchMode SearchMode { get; }
        void ShowModeless();
    }
}
