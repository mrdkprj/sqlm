using System.Collections.Generic;
using System.Reflection;
using MasudaManager.Utility.Preference;
using MasudaManager.Utility.Preference.Setting;

namespace MasudaManager
{
    public class PreferenceModel : IModel
    {
        readonly int _panelItemTabIndexIncrementablValue = 5;

        public PreferenceModel()
        {
        }

        public Dictionary<PreferencePanelType, PreferencePanelBase> PanelDictionary { get; private set; }
        public List<NodeData> NodeDataList { get; private set; }

        public void CreatePreferencePanels()
        {
            this.PanelDictionary = new Dictionary<PreferencePanelType, PreferencePanelBase>();
            this.PanelDictionary.Add(PreferencePanelType.Tab, new TabPanel());
            this.PanelDictionary.Add(PreferencePanelType.Input, new InputPanel());
            this.PanelDictionary.Add(PreferencePanelType.Editor, new EditorPanel());
            this.PanelDictionary.Add(PreferencePanelType.EditorColor, new EditorColorPanel());
            this.PanelDictionary.Add(PreferencePanelType.Output, new OutputPanel());
            this.PanelDictionary.Add(PreferencePanelType.List, new ListPanel());
            this.PanelDictionary.Add(PreferencePanelType.Sql, new SqlPanel());
            this.PanelDictionary.Add(PreferencePanelType.File, new FilePanel());
        }

        public void CreateNodeDataList()
        {
            this.NodeDataList = new List<NodeData>();
            this.NodeDataList.Add(new NodeData(PreferencePanelType.Tab, LocalizedTextProvider.Form.PrefTab));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.Tab, PreferencePanelType.Input, LocalizedTextProvider.Form.PrefInput));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.Input, PreferencePanelType.Editor, LocalizedTextProvider.Form.PrefEditor));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.Input, PreferencePanelType.EditorColor, LocalizedTextProvider.Form.PrefColor));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.Tab, PreferencePanelType.Output, LocalizedTextProvider.Form.PrefOutput));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.List, LocalizedTextProvider.Form.PrefList));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.Sql, LocalizedTextProvider.Form.PrefSql));
            this.NodeDataList.Add(new NodeData(PreferencePanelType.File, LocalizedTextProvider.Form.PrefFile));
        }

        public void SetPanelTabIndex(int tabIndex)
        {
            foreach (var panel in this.PanelDictionary.Values)
            {
                panel.TabIndex = tabIndex;
                panel.TabStop = false;
                panel.SetPanelItemTabIndex(tabIndex, _panelItemTabIndexIncrementablValue);
            }
        }

        public void ReleaseModel()
        {
            this.PanelDictionary = null;
            this.NodeDataList = null;
        }
    }
}
