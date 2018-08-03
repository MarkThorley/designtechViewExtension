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
    class GraphInformationViewModel : NotificationObject
    {
        private ViewLoadedParams readyParams;
        private DynamoViewModel viewModel;
        private Window dynWindow;

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

        public class groupData
        {
            public string name { get; set; }
            public string guid { get; set; }
            public ModelBase theGroup { get; set; }
            public ViewLoadedParams theWSModel { get; set; }
        }

        #region Fields
        private string graphFileName;
        private string graphFilePath;
        private string graphDescription;
        private string graphLastSaved;
        private string activeNodeCount;
        private string activeWireCount;
        List<nodeData> errorNodeTypes;
        List<groupData> groupTypes;
        #endregion

        #region Property
        // Displays the graph file name
        public string GraphFileName
        {
            get
            {
                graphFileName = getGraphFileName();
                return graphFileName;
            }
        }

        // Displays the graph file name
        public string GraphFilePath
        {
            get
            {
                graphFilePath = getGraphFilePath();
                return graphFilePath;
            }
        }

        // Displays the graph file name
        public string GraphDescription
        {
            get
            {
                graphDescription = getGraphDescription();
                return graphDescription;
            }
        }

        // Displays the graph file name
        public string GraphLastSaved
        {
            get
            {
                graphLastSaved = getGraphLastSaved();
                return graphLastSaved;
            }
        }

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

        // Displays error nodes in the workspace
        public List<nodeData> ErrorNodeTypes
        {
            get
            {
                errorNodeTypes = getErrorNodeTypes();
                return errorNodeTypes;
            }
        }

        // Displays error nodes in the workspace
        public List<groupData> GroupTypes
        {
            get
            {
                groupTypes = getGroupTypes();
                return groupTypes;
            }
        }

        #endregion

        #region Functions
        // Helper function that retrieves the name of the graph
        public string getGraphFileName()
        {
            Dynamo.Graph.Workspaces.IWorkspaceModel curWS = readyParams.CurrentWorkspaceModel;
            string f = curWS.FileName;

            if (f == "")
            {
                return "File has not been saved yet";
            }
            int ind = f.LastIndexOf("\\");
            string fN = f.Remove(0, (ind + 1));
            return fN;
        }

        // Helper function that retrieves the path of the graph
        public string getGraphFilePath()
        {
            Dynamo.Graph.Workspaces.IWorkspaceModel curWS = readyParams.CurrentWorkspaceModel;
            string f = curWS.FileName;

            if (f == "")
            {
                return "File has not been saved yet";
            }
            int ind = f.LastIndexOf("\\");
            string name = f.Remove(0, (ind + 1));
            string fP = f.Remove((ind + 1),name.Length);
            return fP;
        }

        // Helper function that retrieves the description of the graph
        public string getGraphDescription()
        {
            Dynamo.Graph.Workspaces.IWorkspaceModel curWS = readyParams.CurrentWorkspaceModel;
            string f = curWS.Description;

            if (f == "")
            {
                return "File has no description";
            }
            return f;
        }

        // Helper function that retrieves the description of the graph
        public string getGraphLastSaved()
        {
            Dynamo.Graph.Workspaces.IWorkspaceModel curWS = readyParams.CurrentWorkspaceModel;
            Dynamo.Graph.Workspaces.WorkspaceModel wS = curWS as Dynamo.Graph.Workspaces.WorkspaceModel;
            string f = wS.LastSaved.ToString();

            if (f == "")
            {
                return "File has not been saved";
            }
            return f;
        }

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
    
        // Helper function that builds string of error nodes
        public List<nodeData> getErrorNodeTypes()
        {
            List<nodeData> output = new List<nodeData>();
            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                if (node.State.ToString() == "Warning" && node.Name != "Watch")
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

        // Helper function that builds string of error nodes
        public List<groupData> getGroupTypes()
        {
            List<groupData> output = new List<groupData>();
            List<groupData> groups = new List<groupData>();

            Dynamo.Graph.Workspaces.WorkspaceModel ws = readyParams.CurrentWorkspaceModel as WorkspaceModel;

            foreach (AnnotationModel group in ws.Annotations)
            {
                if (group.AnnotationText == "")
                {
                    groups.Add(new groupData()
                    {
                        name = "*Group has no Title*",
                        guid = group.GUID.ToString(),
                        theGroup = group,
                        theWSModel = readyParams
                    });
                }
                else
                {
                    groups.Add(new groupData()
                    {
                        name = group.AnnotationText,
                        guid = group.GUID.ToString(),
                        theGroup = group,
                        theWSModel = readyParams
                    });
                }
            }
            foreach (groupData group in groups.OrderByDescending(x => x.name).ThenBy(x => x.name)) { output.Add(group); }
            return output;
        }

        #endregion

        #region ReadyParams
        public GraphInformationViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
            p.CurrentWorkspaceChanged += CurrentWorkspaceModel_GraphSaved;
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
            RaisePropertyChanged("ErrorNodeTypes");
        }

        private void CurrentWorkspaceModel_WorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("ErrorNodeTypes","GroupTypes");
        }

        private void ReadyParams_CurrentWorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("ErrorNodeTypes", "GroupTypes");
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
