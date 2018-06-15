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

namespace designtechViewExtension
{
    /// <summary>
    /// Interaction logic for designtechWindow.xaml
    /// </summary>
    public partial class designtechWindow : Window
    {

        public designtechWindow()
        {
            InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            double objectCenterX = 0;
            double objectCenterY = 0;
            string guid = "b1518d85-82fe-4f29-a68a-1e6d1a0d3d96";

            //List<NodeModel> nodeList = readyParams.CurrentWorkspaceModel.Nodes.ToList();

            var v = readyParams.CurrentWorkspaceModel;

            foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
            {
                node.Deselect();
                node.IsSelected = false;
            }

            //var VM = dgrWS.DynamoWindow.DataContext as DynamoViewModel;
            viewModel.CurrentSpaceViewModel.ResetFitViewToggleCommand.Execute(null);
            //viewModel.AddToSelectionCommand.Execute(nodeList[0]);
            //viewModel.FitViewCommand.Execute(null);




            /*
            var zoomNode2 = readyParams.CurrentWorkspaceModel.Nodes;
            var zoomNode = readyParams.CurrentWorkspaceModel.Nodes.First(x => x.GUID.ToString() == guid);
            objectCenterX = zoomNode.CenterX;
            objectCenterY = zoomNode.CenterY;

            var maxZoom = 4d;
            var corrX = -objectCenterX * maxZoom + dynWindow.ActualWidth / 2.2;
            var corrY = -objectCenterY * maxZoom + dynWindow.ActualHeight / 2.2;
            viewModel.Zoom = maxZoom;
            viewModel.X = corrX;
            viewModel.Y = corrY;
            */
            //int index = listBox.SelectedIndex;
        }
    }
}
