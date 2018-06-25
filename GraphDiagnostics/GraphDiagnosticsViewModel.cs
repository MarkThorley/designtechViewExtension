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
    class GraphDiagnosticsViewModel : NotificationObject, IDisposable
    {
        public class nodeData
        {
            public string name { get; set; }
            public Guid guid { get; set; }
        }

        #region Fields
        private string graphFileName;
        private string graphFilePath;
        private string graphDescription;
        private string graphLastSaved;
        private string activeNodeCount;
        private string activeWireCount;
        List<string> errorNodeTypes;
        private ReadyParams readyParams;

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
        public List<string> ErrorNodeTypes
        {
            get
            {
                errorNodeTypes = getErrorNodeTypes();
                return errorNodeTypes;
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
        public List<string> getErrorNodeTypes()
        {
            List<string> output = new List<string>();
            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                if (node.State.ToString() == "Warning" && node.Name != "Watch")
                {
                    output.Add(node.Name);
                }
            }
            return output;

        }


        /*
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
                        guid = node.GUID
                    });
                }
            }

            return output;

        }
        */

        #endregion

        #region ReadyParams
        public GraphDiagnosticsViewModel(ReadyParams p)
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
