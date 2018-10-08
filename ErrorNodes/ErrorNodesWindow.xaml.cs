using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Dynamo.ViewModels;
using Dynamo.Graph.Nodes;
using Dynamo.Wpf.Extensions;
using Dynamo.Graph;
using Dynamo.Graph.Annotations;
using Dynamo.Graph.Workspaces;
using Dynamo.Graph.Notes;
using System.Diagnostics;

namespace designtechViewExtension
{
    /// <summary>
    /// Interaction logic for designtechWindow.xaml
    /// </summary>
    public partial class ErrorNodesWindow : Window
    {

        public ErrorNodesWindow()
        {
            InitializeComponent();
        }


        private void listBoxErrorNodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            var selection = lb.SelectedItem;
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
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
