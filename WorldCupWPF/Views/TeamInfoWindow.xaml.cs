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
using System.Windows.Shapes;

namespace WorldCupWPF.Views
{
    /// <summary>
    /// Interaction logic for TeamInfoWindow.xaml
    /// </summary>
    public partial class TeamInfoWindow : Window
    {
        public TeamInfoWindow()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
