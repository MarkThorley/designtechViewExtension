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
using System.Reflection;

namespace designtechViewExtension
{
    /// <summary>
    /// Interaction logic for FavouriteNodesWindow.xaml
    /// </summary>
    public partial class FavouriteNodesWindow : Window
    {
        private ViewLoadedParams readyParams;
        int doubleClickTime = 0;
        int threshold = 200;

        public FavouriteNodesWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {

            Button but = e.Source as Button;
            string content = but.Content.ToString();


            FrameworkElement fe = sender as FrameworkElement;
            FavouriteNodesViewModel dc = fe.DataContext as FavouriteNodesViewModel;
            ReadyParams rp = dc.ReadyParamType;
            var vlp = rp as ViewLoadedParams;

            var dynViewModel = vlp.DynamoWindow.DataContext as DynamoViewModel;
            var dm = dynViewModel.Model as DynamoModel;

            NodeSearchModel nsm = dm.SearchModel;

            List<Dynamo.Search.SearchElements.NodeSearchElement> nodes = nsm.SearchEntries.ToList();

            List<int> nodePosition = new List<int>();
            List<string> nodeNames = new List<string>();
            for (int i = 0; i < nodes.Count; i++)
            {
                nodeNames.Add(nodes[i].FullName);
                if (nodes[i].FullName.Contains(content))
                {
                    nodePosition.Add(i);
                }
            }

            if (nodePosition.Count != 0)
            {
                MethodInfo dynMethod = nodes[(nodePosition[0])].GetType().GetMethod("ConstructNewNodeModel", BindingFlags.NonPublic | BindingFlags.Instance);
                object obj = dynMethod.Invoke(nodes[(nodePosition[0])], new object[] { });
                NodeModel nM = obj as NodeModel;

                try
                {
                    dm.ExecuteCommand(new DynamoModel.CreateNodeCommand(nM, 0, 0, true, false));
                }
                catch (Exception)
                {
                }
            }
                        
            

            /*
            //csv export
            StringBuilder sb = new StringBuilder();
            string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Output";

            foreach (string s in nodeNames)
            {
                sb.AppendLine(s);
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

        private void UpdateValue(object sender, RoutedEventArgs e)
        {
            button1.Content = textBox1.Text;
            button2.Content = textBox2.Text;
            button3.Content = textBox3.Text;
            button4.Content = textBox4.Text;
            button5.Content = textBox5.Text;
            button6.Content = textBox6.Text;
            button7.Content = textBox7.Text;
            button8.Content = textBox8.Text;
        }
    }
}
