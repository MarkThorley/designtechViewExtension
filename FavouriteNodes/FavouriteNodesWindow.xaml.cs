using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dynamo.Extensions;
using Dynamo.ViewModels;
using Dynamo.Graph.Nodes;
using Dynamo.Wpf.Extensions;
using designtechViewExtension;
using Dynamo.Graph;
using System.Drawing;
using Dynamo.Graph.Annotations;
using Dynamo.Graph.Workspaces;
using Dynamo.Graph.Notes;
using System.Diagnostics;
using Dynamo.Models;
using Dynamo.Graph.Nodes.ZeroTouch;
using Dynamo.Engine;
using System.IO;
using Dynamo.Search;

namespace designtechViewExtension
{
    /// <summary>
    /// Interaction logic for FavouriteNodesWindow.xaml
    /// </summary>
    public partial class FavouriteNodesWindow : Window
    {
        private ViewLoadedParams readyParams;

        public FavouriteNodesWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void ListCreateButtonClick(object sender, RoutedEventArgs e)
        {

            FrameworkElement fe = sender as FrameworkElement;
            FavouriteNodesViewModel dc = fe.DataContext as FavouriteNodesViewModel;
            ReadyParams rp = dc.ReadyParamType;
            var vlp = rp as ViewLoadedParams;

            var dynViewModel = vlp.DynamoWindow.DataContext as DynamoViewModel;
            var dm = dynViewModel.Model as DynamoModel;

            //List<FunctionGroup> builtinNodeLibraries = dm.LibraryServices.BuiltinFunctionGroups.ToList();
            //List<FunctionGroup> importedNodeLibraries = dm.LibraryServices.ImportedFunctionGroups.ToList();

            NodeSearchModel nsm = dm.SearchModel;
            List<Dynamo.Search.SearchElements.NodeSearchElement> nodes = nsm.SearchEntries.ToList();

            //List<string> nodenames = new List<string>();
            //foreach (Dynamo.Search.SearchElements.NodeSearchElement n in nodes)
            //{
            //    nodenames.Add(n.Name);
            //}


            //List<List<FunctionDescriptor>> builtinNodeList = new List<List<FunctionDescriptor>>();
            //foreach (FunctionGroup Library in builtinNodeLibraries)
            //{
            //    builtinNodeList.Add(Library.Functions.ToList());
            //}

            //List<List<FunctionDescriptor>> importedNodeList = new List<List<FunctionDescriptor>>();
            //foreach (FunctionGroup Library in importedNodeLibraries)
            //{
            //    importedNodeList.Add(Library.Functions.ToList());
            ///}


            //Dictionary<string,FunctionDescriptor> NodesDict = new Dictionary<string, FunctionDescriptor>();

            //var watch1 = System.Diagnostics.Stopwatch.StartNew();

            //foreach (List<FunctionDescriptor> nodeSet in builtinNodeList)
            //{
            //    for (int i = 0; i < nodeSet.Count(); i++)
            //    {
            //        NodesDict.Add(nodeSet[i].DisplayName + "_" + i.ToString(), nodeSet[i]);
             //   }
            //}

            //watch1.Stop();
            //var elapsedMs1 = watch1.ElapsedMilliseconds;

            //var watch2 = System.Diagnostics.Stopwatch.StartNew();

            //foreach (List<FunctionDescriptor> nodeSet in importedNodeList)
            //{
            //    for (int i = 0; i < nodeSet.Count(); i++)
            //    {
             //       if (nodeSet[i].DisplayName != null)
            //        {
             //           try
             //           {
             //               NodesDict.Add(nodeSet[i].DisplayName + "_" + i.ToString(), nodeSet[i]);
            //            }
             //           catch (Exception)
              //          {
             //           }
              //      }
             //   }
            //}

            //watch2.Stop();
            //var elapsedMs2 = watch2.ElapsedMilliseconds;

            Dictionary<string, NodeModel> matchDict = new Dictionary<string, NodeModel>();
            //foreach (KeyValuePair<string, FunctionDescriptor> item in NodesDict)
            //{
            //    if (item.Key.Contains("List.Transpose"))
            //    {
             //       matchDict.Add(item.Key, item.Value);
             //   }
           // }

            foreach (Dynamo.Search.SearchElements.NodeSearchElement n in nodes)
            {
                if (n.Name == "Watch")
                {
                    matchDict.Add(n.Name, n as NodeModel);
                }
            }

            Dynamo.Search.SearchElements.Nod

            try
            {
                var addNode = new DSFunction(matchDict.First().Value);
                dm.ExecuteCommand(new DynamoModel.CreateNodeCommand(addNode, 0, 0, true, false));
            }
            catch (Exception)
            {
            }



            /*
            //csv export
            StringBuilder sb = new StringBuilder();
            string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\dictOutput";

            foreach (KeyValuePair<string, FunctionDescriptor> item in NodesDict)
            {
                sb.AppendLine(item.Key);
            }
            if (!System.IO.Directory.Exists(FolderPath)) // If folder does not exist
            {
                try
                {
                    System.IO.Directory.CreateDirectory(FolderPath); //Create the folder
                }
                catch (Exception)
                {
                    // Quiet Fail
                }
            }
            string filePath = FolderPath + "\\" + "export.csv";
            File.WriteAllText(filePath, sb.ToString());
            */

        }

    }
}
