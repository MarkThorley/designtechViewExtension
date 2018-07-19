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
using System.IO;

namespace designtechViewExtension
{
    public class FavouriteNodesViewModel : NotificationObject, IDisposable
        {
            private ReadyParams readyParams;

            // Displays active nodes in the workspace
            public ReadyParams ReadyParamType
            {
                get
                {
                    readyParams = getReadyParams();
                    return readyParams;
                }
            }

            private string dynFavSettingsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                "designtechViewExtension",
                                                "dynFavSettings.csv");

            // Helper function that builds string of active nodes
            public ReadyParams getReadyParams()
            {
                return readyParams;
            }

            public FavouriteNodesViewModel(ReadyParams p)
            {
                readyParams = p;
            }

            public void Dispose()
            {
            }
        }
}
