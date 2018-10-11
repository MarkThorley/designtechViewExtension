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
    class ErrorNodesViewModel : NotificationObject
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
        List<nodeData> errorNodeTypes;
        #endregion

        #region Property
        // Displays error nodes in the workspace
        public List<nodeData> ErrorNodeTypes
        {
            get
            {
                errorNodeTypes = getErrorNodeTypes();
                return errorNodeTypes;
            }
        }

        #endregion

        #region Functions
        // Helper function that builds string of error nodes
        public List<nodeData> getErrorNodeTypes()
        {
            List<nodeData> output = new List<nodeData>();
            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                if (node.State.ToString() == "Warning" || node.State.ToString() == "Dead" || node.State.ToString() == "Error" || node.State.ToString() == "PersistentWarning" || node.State.ToString() == "AstBuildBroken" && node.Name != "Watch")
                {
                    output.Add(new nodeData()
                    {
                        name = node.Name,
                        guid = node.GUID.ToString(),
                        theNode = node,
                        theWSModel = readyParams
                       
                    });
                }
            }
            return output;
        }
        #endregion

        #region ReadyParams
        public ErrorNodesViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
            p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodesChanged;
            p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodesChanged;
            p.CurrentWorkspaceChanged += CurrentWorkspaceModel_WorkspaceChanged;
            p.CurrentWorkspaceChanged += ReadyParams_CurrentWorkspaceChanged;
            AddEventHandlers(p.CurrentWorkspaceModel);

        }

        #endregion

        #region ChangeProperty

        private void CurrentWorkspaceModel_NodesChanged(NodeModel obj)
        {
            RaisePropertyChanged("ErrorNodeTypes");
        }

        private void CurrentWorkspaceModel_WorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("ErrorNodeTypes");
        }

        private void ReadyParams_CurrentWorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("ErrorNodeTypes");
        }

        private void node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                RaisePropertyChanged("ErrorNodeTypes");
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
            RaisePropertyChanged("ErrorNodeTypes");
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
            readyParams.CurrentWorkspaceModel.NodeAdded -= CurrentWorkspaceModel_NodesChanged;
            readyParams.CurrentWorkspaceModel.NodeRemoved -= CurrentWorkspaceModel_NodesChanged;
        }
        #endregion
    }
}
