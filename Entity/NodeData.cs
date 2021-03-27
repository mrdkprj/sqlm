using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Utility.Preference;

namespace MasudaManager
{
    public class NodeData
    {
        public NodeData() { }

        public NodeData(PreferencePanelType panelType, string nodeName)
        {
            this.ParentPanelType = panelType;
            this.ParentName = nodeName;
            this.ChildPanelType = PreferencePanelType.None;
            this.ChildName = null;
            this.HasParent = false;
        }

        public NodeData(PreferencePanelType parentPanelType, PreferencePanelType childPanelType, string childName)
        {
            this.ParentPanelType = parentPanelType;
            this.ParentName = null;
            this.ChildPanelType = childPanelType;
            this.ChildName = childName;
            this.HasParent = true;
        }

        public PreferencePanelType ParentPanelType { get; set; }
        public string ParentName { get; set; }
        public PreferencePanelType ChildPanelType { get; set; }
        public string ChildName { get; set; }
        public bool HasParent { get; set; }
    }
}
