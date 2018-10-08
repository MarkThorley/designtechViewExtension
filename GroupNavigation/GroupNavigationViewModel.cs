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
    class GroupNavigationViewModel : NotificationObject
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

        public class groupData
        {
            public string name { get; set; }
            public string guid { get; set; }
            public ModelBase theGroup { get; set; }
            public ViewLoadedParams theWSModel { get; set; }
        }

        #region Fields
        List<groupData> groupTypes;
        #endregion

        #region Property
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
            foreach (groupData group in groups.OrderByDescending(x => x.name)) { output.Add(group); }
            output.Reverse();
            return output;
        }

        #endregion

        #region ReadyParams
        public GroupNavigationViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
            p.CurrentWorkspaceChanged += CurrentWorkspaceModel_WorkspaceChanged;
            p.CurrentWorkspaceChanged += ReadyParams_CurrentWorkspaceChanged;
            AddEventHandlers(p.CurrentWorkspaceModel);

        }

        #endregion

        #region ChangeProperty
        private void CurrentWorkspaceModel_WorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("GroupTypes");
        }

        private void ReadyParams_CurrentWorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            RaisePropertyChanged("GroupTypes");
        }

        private void node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                RaisePropertyChanged("GroupTypes");
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
            RaisePropertyChanged("GroupTypes");
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
        }
        #endregion
    }
}
