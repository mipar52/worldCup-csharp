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

namespace WorldCupWPF.Controls
{
    /// <summary>
    /// Interaction logic for LoadingSpinnerControl.xaml
    /// </summary>
    public partial class LoadingSpinnerControl : UserControl
    {
        public LoadingSpinnerControl()
        {
            InitializeComponent();
        }
        public string Message
        {
            get => txtMessage.Text;
            set => txtMessage.Text = value;
        }
    }
}
