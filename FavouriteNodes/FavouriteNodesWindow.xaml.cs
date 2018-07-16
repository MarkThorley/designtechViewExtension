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
using System.Diagnostics;

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
            
        }

    }
}
