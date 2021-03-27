using MasudaManager.Utility.Preference;
using MasudaManager.Utility.Preference.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface IPreferenceView : IView<PreferenceModel>
    {
        event EventHandler ComponentInitialized;
        event EventHandler<GenericEventArgs<object>> Initiated;
        event EventHandler<TreeViewCancelEventArgs> NodeSelectionChanging;
        event EventHandler NodeSelectionChanged;
        event EventHandler OkButtonClicked;
        event EventHandler ResetButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler CloseButtonClicked;
        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler<CancelEventArgs> ReleaseRequested;

        void AddChildNode(NodeData nodeData);
        void AddTreeNode(NodeData nodeData);
        void CloseView();
        PreferencePanelType CurrentPanelType { get; }
        void ExpandAllNodes();
        void FocusOnTreeView();
        void SetPanels(IEnumerable<PreferencePanelBase> panels);
        void ShowModal();
    }
}
