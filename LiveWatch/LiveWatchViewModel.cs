using Dynamo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;

namespace designtechViewExtension
{
    class LiveWatchViewModel : NotificationObject, IDisposable
    {
        public class nodeData
        {
            public string name { get; set; }
            public Guid guid { get; set; }
        }

        #region Fields
        List<object> nodeOutput;
        private ReadyParams readyParams;

        #endregion

        #region Property
        // Displays error nodes in the workspace
        public List<object> NodeOutput
        {
            get
            {
                nodeOutput = getNodeOutput();
                return nodeOutput;
            }
        }

        #endregion

        #region Functions
        // Helper function that builds string of error nodes
        public List<object> getNodeOutput()
        {
            List<object> output = new List<object>();
            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                if (node.IsSelected)
                {
                    output.Add(node.OutputData.Description);
                    output.Add(node.OutputData.Id.ToString());
                    output.Add(node.OutputData.IntitialValue);
                    output.Add(node.OutputData.Name);
                    output.Add(node.OutputData.Type.ToString());
                    //output.Add(node.TryGetOutput(0));
                }
            }
            return output;

        }

        #endregion

        #region ReadyParams
        public LiveWatchViewModel(ReadyParams p)
        {
            readyParams = p;
            p.CurrentWorkspaceChanged += CurrentWorkspaceModel_GraphSaved;
            p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodeCount;
            p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodeCount;
            p.CurrentWorkspaceModel.ConnectorAdded += CurrentWorkspaceModel_WireCount;
            p.CurrentWorkspaceModel.ConnectorDeleted += CurrentWorkspaceModel_WireCount;
            p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodesChanged;
            p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodesChanged;
        }

        #endregion

        #region ChangeProperty

        private void CurrentWorkspaceModel_GraphSaved(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("GraphLastSaved");
        }

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
            RaisePropertyChanged("ErrorNodeTypeNames");
        }


        #endregion

        #region Dispose Methods
        public void Dispose()
        {
            readyParams.CurrentWorkspaceChanged += CurrentWorkspaceModel_GraphSaved;
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
