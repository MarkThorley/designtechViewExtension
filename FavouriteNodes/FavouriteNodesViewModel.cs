using System;
using Dynamo.Core;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using System.Windows;
using Dynamo.ViewModels;
using Dynamo.Models;
using System.Linq;
using Dynamo.Graph.Nodes.ZeroTouch;
using Dynamo.Wpf.Extensions;

namespace designtechViewExtension
{
    public class FavouriteNodesViewModel : NotificationObject, IDisposable
        {
            private string activeNodeTypes;
            private ReadyParams readyParams;

            // Displays active nodes in the workspace
            public string ActiveNodeTypes
            {
                get
                {
                    activeNodeTypes = getNodeTypes();
                    return activeNodeTypes;
                }
            }

            // Displays active nodes in the workspace
            public ReadyParams ReadyParamType
            {
                get
                {
                    readyParams = getReadyParams();
                    return readyParams;
                }
            }

            // Helper function that builds string of active nodes
            public string getNodeTypes()
            {
                string output = "Active nodes:\n";

                foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
                {
                    string nickName = node.Name;
                    output += nickName + "\n";
                }

                return output;
            }

            // Helper function that builds string of active nodes
            public ReadyParams getReadyParams()
            {
                return readyParams;
            }

            public FavouriteNodesViewModel(ReadyParams p)
            {
                readyParams = p;
                p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodesChanged;
                p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodesChanged;
            }

            private void CurrentWorkspaceModel_NodesChanged(NodeModel obj)
            {
                RaisePropertyChanged("ActiveNodeTypes");
            }

            public void Dispose()
            {
                readyParams.CurrentWorkspaceModel.NodeAdded -= CurrentWorkspaceModel_NodesChanged;
                readyParams.CurrentWorkspaceModel.NodeRemoved -= CurrentWorkspaceModel_NodesChanged;
            }
        }
}
