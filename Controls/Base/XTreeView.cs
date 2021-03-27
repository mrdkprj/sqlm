using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public class XTreeView : TreeView
    {
        Dictionary<PreferencePanelType, TreeNode> _nodeDictionary = new Dictionary<PreferencePanelType, TreeNode>();
        Dictionary<TreeNode, PreferencePanelType> _panelTypeDictionary = new Dictionary<TreeNode, PreferencePanelType>();

        public XTreeView()
            : base()
        {
        }

        public TreeNode CurrentNode { get; private set; }
        public PreferencePanelType CurrentPanelType { get; private set; }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            this.CurrentNode = e.Node;
            this.CurrentPanelType = _panelTypeDictionary.GetValueOrDefault(e.Node);
            
            base.OnAfterSelect(e);
        }

        public void AddNode(NodeData nodeData)
        {
            var node = this.Nodes.Add(nodeData.ParentName);
            _nodeDictionary.Add(nodeData.ParentPanelType, node);
            _panelTypeDictionary.Add(node, nodeData.ParentPanelType);
            this.CurrentNode = node;
        }

        public void AddChildNode(NodeData nodeData)
        {
            var parentNode = GetTreeNode(nodeData.ParentPanelType);
            var childNode = parentNode.Nodes.Add(nodeData.ChildName);
            _nodeDictionary.Add(nodeData.ChildPanelType, childNode);
            _panelTypeDictionary.Add(childNode, nodeData.ChildPanelType);
        }

        public TreeNode GetTreeNode(PreferencePanelType panelType)
        {
            return _nodeDictionary.GetValueOrDefault(panelType);
        }

        public PreferencePanelType GetPanelType(TreeNode node)
        {
            return _panelTypeDictionary.GetValueOrDefault(node);
        }
    }
}
