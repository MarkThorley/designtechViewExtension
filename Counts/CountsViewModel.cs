using Dynamo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using Dynamo.ViewModels;
using System.Windows;
using Dynamo.Graph;
using Dynamo.Wpf.Extensions;
using Dynamo.Graph.Annotations;
using Dynamo.Graph.Workspaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace designtechViewExtension
{
    class CountsViewModel : NotificationObject
    {
        private ViewLoadedParams readyParams;

        public class nodeData : INotifyPropertyChanged
        {
            public string name { get; set; }
            public string guid { get; set; }
            public ModelBase theNode { get; set; }
            public ViewLoadedParams theWSModel { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        #region Fields
        private string activeNodeCount;
        private string activeWireCount;
        #endregion

        #region Property
        //Displays the count of the nodes in the workspace
        public string ActiveNodeCount
        {
            get
            {
                activeNodeCount = getTotalNodeCount();
                return activeNodeCount;
            }
        }

        //Displays the count of the wires in the workspace
        public string ActiveWireCount
        {
            get
            {
                activeWireCount = getTotalWireCount();
                return activeWireCount;
            }
        }
        #endregion

        #region Functions
        // Helper function that counts the number of nodes
        public string getTotalNodeCount()
        {

            int count = 0;
            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                count++;
            }
            string output = count.ToString();
            return output;
        }

        // Helper function that counts the number of wires
        public string getTotalWireCount()
        {

            int count = 0;
            foreach (Dynamo.Graph.Connectors.ConnectorModel node in readyParams.CurrentWorkspaceModel.Connectors)
            {
                count++;
            }
            string output = count.ToString();
            return output;
        }
        #endregion

        #region ReadyParams
        public CountsViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
            p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodeCount;
            p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodeCount;
            p.CurrentWorkspaceModel.ConnectorAdded += CurrentWorkspaceModel_WireCount;
            p.CurrentWorkspaceModel.ConnectorDeleted += CurrentWorkspaceModel_WireCount;
            p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodesChanged;
            p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodesChanged;
            p.CurrentWorkspaceChanged += CurrentWorkspaceModel_WorkspaceChanged;
            p.CurrentWorkspaceChanged += ReadyParams_CurrentWorkspaceChanged;
            AddEventHandlers(p.CurrentWorkspaceModel);

        }

        #endregion

        #region ChangeProperty

        private void CurrentWorkspaceModel_NodeCount(NodeModel obj)
        {
            RaisePropertyChanged("ActiveNodeCount");
        }

        private void CurrentWorkspaceModel_WireCount(Dynamo.Graph.Connectors.ConnectorModel obj)
        {
            RaisePropertyChanged("ActiveWireCount");
        }

        private void CurrentWorkspaceModel_NodesChanged(NodeModel obj)
        {
            RaisePropertyChanged("ActiveNodeCount");
        }

        private void CurrentWorkspaceModel_WorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("ActiveNodeCount", "ActiveWireCount");
        }

        private void ReadyParams_CurrentWorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("ActiveNodeCount", "ActiveWireCount");
        }

        private void node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                RaisePropertyChanged("ErrorNodeTypes", "GroupTypes");
            }
        }

        private void CurrentWorkspaceModel_NodeAdded(NodeModel node)
        {
            node.PropertyChanged += node_PropertyChanged;
        }

        private void CurrentWorkspaceModel_NodeRemoved(NodeModel node)
        {
            node.PropertyChanged -= node_PropertyChanged;
        }

        private void CurrentWorkspaceModel_NodesCleared()
        {
            foreach (var node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                node.PropertyChanged -= node_PropertyChanged;
            }
            RaisePropertyChanged("ErrorNodeTypes", "GroupTypes");
        }


        #endregion

        #region Handlers
        private void AddEventHandlers(IWorkspaceModel model)
        {
            foreach (var node in model.Nodes)
            {
                node.PropertyChanged += node_PropertyChanged;
            }
            model.NodeAdded += CurrentWorkspaceModel_NodeAdded;
            model.NodeRemoved += CurrentWorkspaceModel_NodeRemoved;
            model.NodesCleared += CurrentWorkspaceModel_NodesCleared;
        }
        #endregion

        #region Dispose Methods
        public void Dispose()
        {
            readyParams.CurrentWorkspaceModel.NodeAdded -= CurrentWorkspaceModel_NodeCount;
            readyParams.CurrentWorkspaceModel.NodeRemoved -= CurrentWorkspaceModel_NodeCount;
            readyParams.CurrentWorkspaceModel.ConnectorAdded -= CurrentWorkspaceModel_WireCount;
            readyParams.CurrentWorkspaceModel.ConnectorDeleted -= CurrentWorkspaceModel_WireCount;
            readyParams.CurrentWorkspaceModel.NodeAdded -= CurrentWorkspaceModel_NodesChanged;
            readyParams.CurrentWorkspaceModel.NodeRemoved -= CurrentWorkspaceModel_NodesChanged;
        }
        #endregion
    }
}
