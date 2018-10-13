using System;
using Dynamo.Wpf.Extensions;
using System.Windows.Controls;
using Dynamo.ViewModels;

namespace designtechViewExtension
{
    public class designtechViewExtension : IViewExtension
    {
        private MenuItem designtechMenuItem;
        private MenuItem designtechAboutMenuItem;
        private MenuItem designtechNodeConnectorCountsMenuItem;
        private MenuItem designtechMetadataMenuItem;
        private MenuItem designtechToggleFreezeMenuItem;
        private MenuItem designtechGroupNavigationMenuItem;
        private MenuItem designtechErrorNodesMenuItem;
        private MenuItem designtechFavouriteNodesMenuItem;

        public void Dispose()
        {
        }

        public void Startup(ViewStartupParams p)
        {
        }

        public void Loaded(ViewLoadedParams p)
        {
            designtechMenuItem = new MenuItem { Header = "designtech" };
            var VM = p.DynamoWindow.DataContext as DynamoViewModel;

            #region About
            designtechAboutMenuItem = new MenuItem { Header = "About" };
            designtechAboutMenuItem.Click += (sender, args) =>
            {
                //var viewModel = new GraphInformationViewModel(p);
                var window = new AboutWindow
                {
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechAboutMenuItem);
            #endregion

            #region Metadata
            designtechMetadataMenuItem = new MenuItem { Header = "Graph Metadata" };
            designtechMetadataMenuItem.Click += (sender, args) =>
            {
                //var viewModel = new GraphInformationViewModel(p);
                var window = new GraphMetadataWindow
                {
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechMetadataMenuItem);
            #endregion

            #region Node/Connector 
            designtechNodeConnectorCountsMenuItem = new MenuItem { Header = "Node/Connector Counts" };
            designtechNodeConnectorCountsMenuItem.Click += (sender, args) =>
            {
                var viewModel = new CountsViewModel(p);
                var window = new CountsWindow
                {
                    // Set the data context for the main grid in the window.
                    MainGrid = { DataContext = viewModel },

                    // Set the owner of the window to the Dynamo window.
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechNodeConnectorCountsMenuItem);
            #endregion

            #region Toggle Freeze
            designtechToggleFreezeMenuItem = new MenuItem { Header = "Toggle Freeze" };
            designtechToggleFreezeMenuItem.Click += (sender, args) =>
            {
                var viewModel = new ToggleFreezeViewModel(p);
                var window = new ToggleFreezeWindow
                {
                    // Set the data context for the main grid in the window.
                    MainGrid = { DataContext = viewModel },

                    // Set the owner of the window to the Dynamo window.
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechToggleFreezeMenuItem);
            #endregion

            #region Group Navigation
            designtechGroupNavigationMenuItem = new MenuItem { Header = "Group Navigation" };
            designtechGroupNavigationMenuItem.Click += (sender, args) =>
            {
                var viewModel = new GroupNavigationViewModel(p);
                var window = new GroupNavigationWindow
                {
                    // Set the data context for the main grid in the window.
                    MainGrid = { DataContext = viewModel },

                    // Set the owner of the window to the Dynamo window.
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechGroupNavigationMenuItem);
            #endregion

            #region Error Nodes
            designtechErrorNodesMenuItem = new MenuItem { Header = "Error Nodes" };
            designtechErrorNodesMenuItem.Click += (sender, args) =>
            {
                var viewModel = new ErrorNodesViewModel(p);
                var window = new ErrorNodesWindow
                {
                    // Set the data context for the main grid in the window.
                    MainGrid = { DataContext = viewModel },

                    // Set the owner of the window to the Dynamo window.
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechErrorNodesMenuItem);
            #endregion

            #region Favourite Nodes
            designtechFavouriteNodesMenuItem = new MenuItem { Header = "Favourite Nodes" };
            designtechFavouriteNodesMenuItem.Click += (sender, args) =>
            {
                var viewModel = new FavouriteNodesViewModel(p);
                var window = new FavouriteNodesWindow
                {
                    // Set the data context for the main grid in the window.
                    MainGrid = { DataContext = viewModel },

                    // Set the owner of the window to the Dynamo window.
                    Owner = p.DynamoWindow
                };
                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;
                window.Show();
            };
            designtechMenuItem.Items.Add(designtechFavouriteNodesMenuItem);
            #endregion

            p.dynamoMenu.Items.Add(designtechMenuItem);
        }

        public void Shutdown()
        {
        }

        public string UniqueId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public string Name
        {
            get
            {
                return "designtech View Extension";
            }
        }

    }
}

