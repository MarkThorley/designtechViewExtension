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
using ProtoCore.Mirror;

namespace designtechViewExtension
{
    class LiveWatchViewModel : NotificationObject
    {
        #region Fields
        private ViewLoadedParams readyParams;
        private string liveWatchString;
        private bool node_SelectionChanged;
        #endregion

        #region Property
        // Displays error nodes in the workspace
        public string LiveWatchString
        {
            get
            {
                liveWatchString = getSelectedNodeOutput();
                return liveWatchString;
            }
        }

        #endregion

        #region Functions
        // Helper function that counts the number of nodes
        public string getSelectedNodeOutput()
        {
            List<string> dataList = new List<string>();
            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                if (node.IsSelected)
                {
                    var cachedData = node.CachedValue;
                    List<MirrorData> elements = cachedData.GetElements().ToList();
                    foreach (MirrorData md in elements)
                    {
                        ClassMirror cm = md.Class;
                    }
                    string stringData = cachedData.StringData;
                    dataList.Add(stringData);
                }
            }
            if (dataList.Count == 0)
            {
                return "Select an executed node in the canvas";
            }
            if (dataList.Count > 1)
            {
                return "Select only one node";
            }
            else
            {
                return dataList[0];
            }
        }
        #endregion

        #region ReadyParams
        public LiveWatchViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
        }

        #endregion
        
        #region Dispose Methods
        public void Dispose()
        {
        }
        #endregion
    }
}
