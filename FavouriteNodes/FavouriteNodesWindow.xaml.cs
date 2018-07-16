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

namespace designtechViewExtension
{
    /// <summary>
    /// Interaction logic for FavouriteNodesWindow.xaml
    /// </summary>
    public partial class FavouriteNodesWindow : Window
    {
        public FavouriteNodesWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void WatchButtonClick(object sender, RoutedEventArgs e)
        {
            /*
            ModelBase modelBase = selection.GetType().GetProperty("theNode").GetValue(selection) as ModelBase;
            ViewLoadedParams viewLoadedParams = selection.GetType().GetProperty("theWSModel").GetValue(selection) as ViewLoadedParams;
            string guid = selection.GetType().GetProperty("guid").GetValue(selection) as string;

            foreach (NodeModel node in viewLoadedParams.CurrentWorkspaceModel.Nodes)
            {
                node.Deselect();
                node.IsSelected = false;
            }

            Dynamo.Graph.Workspaces.WorkspaceModel ws = viewLoadedParams.CurrentWorkspaceModel as WorkspaceModel;
            foreach (AnnotationModel group in ws.Annotations)
            {
                group.Deselect();
                group.IsSelected = false;
            }

            foreach (NoteModel note in ws.Notes)
            {
                note.Deselect();
                note.IsSelected = false;
            }

            var VM = viewLoadedParams.DynamoWindow.DataContext as DynamoViewModel;
            
            VM.CurrentSpaceViewModel.ResetFitViewToggleCommand.Execute(null);
            VM.AddToSelectionCommand.Execute(modelBase);
            VM.FitViewCommand.Execute(null);
            */
        }

    }
}
