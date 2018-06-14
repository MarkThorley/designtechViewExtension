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
            

            //listBox.Items.Add("Test");
            //listBox.Items.Add("Mark");
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // listBox.SelectedItem.
        }
    }
}
