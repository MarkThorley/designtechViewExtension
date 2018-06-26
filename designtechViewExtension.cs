using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using Dynamo.ViewModels;

namespace designtechViewExtension
{
    public class designtechViewExtension : IViewExtension
    {
        private MenuItem designtechMenuItem;
        private MenuItem designtechAboutMenuItem;
        private MenuItem designtechGraphDiagnosticsMenuItem;
        private MenuItem designtechLiveWatchMenuItem;

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
                //var viewModel = new GraphDiagnosticsViewModel(p);
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

            #region Graph Diagnostics
            designtechGraphDiagnosticsMenuItem = new MenuItem { Header = "Graph Diagnostics" };
            designtechGraphDiagnosticsMenuItem.Click += (sender, args) =>
            {
                var viewModel = new GraphDiagnosticsViewModel(p);
                var window = new GraphDiagnosticsWindow
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
            designtechMenuItem.Items.Add(designtechGraphDiagnosticsMenuItem);
            #endregion

            #region Live Watch
            designtechLiveWatchMenuItem = new MenuItem { Header = "Live Watch" };
            designtechLiveWatchMenuItem.Click += (sender, args) =>
            {
                var viewModel = new LiveWatchViewModel(p);
                var window = new LiveWatchWindow
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
            designtechMenuItem.Items.Add(designtechLiveWatchMenuItem);
            #endregion

            //Add items to menu list
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

