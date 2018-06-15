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
        private MenuItem designtechGraphDiagnosticsMenuItem;

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

            designtechGraphDiagnosticsMenuItem = new MenuItem { Header = "Graph Diagnostics" };
            designtechGraphDiagnosticsMenuItem.Click += (sender, args) =>
            {
                var viewModel = new WindowViewModel(p);
                var window = new designtechWindow
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

