using MasudaManager.Utility.Preference;
using MasudaManager.Utility.Preference.Setting;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using WinFormsMvp;
using WinFormsMvp.Binder;

namespace MasudaManager.Presenters
{
    public class PreferencePresenter : Presenter<IPreferenceView>
    {
        bool _saveRequired = false;
        bool _isReset = false;

        readonly int _panelTabIndex = 100;

        public PreferencePresenter(IPreferenceView view)
            : base(view)
        {
            View.Model = new PreferenceModel();
            RegisterHandlers();
        }

        #region EventHandlers
        void RegisterHandlers()
        {
            View.ComponentInitialized += View_ComponentInitialized;
            View.Initiated += View_Initiated;
            View.NodeSelectionChanging += View_NodeSelectionChanging;
            View.NodeSelectionChanged += View_NodeSelectionChanged;
            View.OkButtonClicked += View_OKButtonClicked;
            View.ResetButtonClicked += View_ResetButtonClicked;
            View.CancelButtonClicked += View_CancelButtonClicked;
            View.CloseButtonClicked += View_CloseButtonClicked;
            View.ViewClosing += View_ViewClosing;
            View.ReleaseRequested += View_ReleaseRequested;
        }
         
        void View_ComponentInitialized(object sender, EventArgs e)
        {
            PrepareSubComponents();
        }

        void View_Initiated(object sender, GenericEventArgs<object> e)
        {
            OnInitiated();
        }
        
        void View_ReleaseRequested(object sender, CancelEventArgs e)
        {
            ReleasePresenter();         
        }

        void View_ViewClosing(object sender, CancelEventArgs e)
        {
            OnViewClosing();
        }

        void View_NodeSelectionChanging(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            DisableCurrentPanelTabStop();
        }

        void View_NodeSelectionChanged(object sender, EventArgs e)
        {
            DisplaySelectedPanel();
        }
        
        void View_OKButtonClicked(object sender, EventArgs e)
        {
            _saveRequired = true;
            View.CloseView();
        }

        void View_ResetButtonClicked(object sender, EventArgs e)
        {
            ResetSetting();
        }

        void View_CancelButtonClicked(object sender, EventArgs e)
        {
            _saveRequired = false;
            View.CloseView();
        }

        void View_CloseButtonClicked(object sender, EventArgs e)
        {
            _saveRequired = false;
            View.CloseView();
        }
        #endregion

        #region Messages
        void RequestApplyInputSetting()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.InputSettingToken);
        }

        void RequestApplyOutputSetting()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.OutputSettingToken);
        }

        void RequestApplyListSetting()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.ListSettingToken);
        }

        void RequestApplyTabSetting()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.TabSettingToken);
        }
        #endregion

        #region Methods

        #region ReleasePresenter
        void ReleasePresenter()
        {
            CancelReset();
            View.Model.ReleaseModel();
            PresenterBinder.Factory.Release(this);
        }
        #endregion

        #region PrepareSubComponents
        void PrepareSubComponents()
        {
            View.Model.CreateNodeDataList();
            SetTreeNodes();

            View.Model.CreatePreferencePanels();
            View.Model.SetPanelTabIndex(_panelTabIndex);
            SetPreferencePanels();
        }
        
        void SetTreeNodes()
        {
            foreach (var nodeData in View.Model.NodeDataList)
            {
                if (nodeData.HasParent)
                    View.AddChildNode(nodeData);
                else
                    View.AddTreeNode(nodeData);
            }
        }

        void SetPreferencePanels()
        {
            View.SetPanels(View.Model.PanelDictionary.Values);
        }
        #endregion

        #region OnInitiated
        void OnInitiated()
        {
            _saveRequired = false;
            _isReset = false;

            View.ExpandAllNodes();

            DisplaySettingValuesOnPanel();

            View.FocusOnTreeView();

            View.ShowModal();
        }
        #endregion

        #region OnViewClosing
        void OnViewClosing()
        {
            if (_saveRequired)
                SaveSetting();
            else
                CancelReset();
        }
        #endregion

        #region Save/Reset/Reload setting
        void SaveSetting()
        {
            foreach (PreferencePanelBase panel in View.Model.PanelDictionary.Values)
            {
                panel.RetrievePreference();
            }

            UserPreference.Proxy.SaveSetting();

            NotifyPreferenceChanged();
        }

        void ResetSetting()
        {
            _isReset = true;
            UserPreference.Proxy.RestoreDefault();
            DisplaySettingValuesOnPanel();
        }

        void CancelReset()
        {
            if (!_isReset)
                return;

            UserPreference.Proxy.ReloadSetting();
        }
        #endregion

        #region Display setting values on panel
        void DisplaySettingValuesOnPanel()
        {
            foreach (PreferencePanelBase panel in View.Model.PanelDictionary.Values)
            {
                panel.DisplayPreference();
            }
        }
        #endregion

        #region NotifyPreferenceChange
        void NotifyPreferenceChanged()
        {
            if (UserPreference.Proxy.InputViewSettingChanged || UserPreference.Proxy.EditorSettingChanged)
                RequestApplyInputSetting();

            if (UserPreference.Proxy.OutputViewSettingChanged)
                RequestApplyOutputSetting();

            if (UserPreference.Proxy.ListViewSettingChanged)
                RequestApplyListSetting();

            if (UserPreference.Proxy.TabSettingChanged)
                RequestApplyTabSetting();
        }
        #endregion

        #region Display selected panel
        void DisplaySelectedPanel()
        {
            var panel = View.Model.PanelDictionary.GetValueOrDefault(View.CurrentPanelType);
            if (panel == null)
                return;

            panel.TabStop = true;
            panel.BringToFront();
        }
        #endregion

        #region Disable CurrentPanel TabStop
        void DisableCurrentPanelTabStop()
        {
            var panel = View.Model.PanelDictionary.GetValueOrDefault(View.CurrentPanelType);
            if (panel == null)
                return;

            panel.TabStop = false;
        }
        #endregion

        #endregion
    }
}