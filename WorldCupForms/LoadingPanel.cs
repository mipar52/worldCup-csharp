using System.Windows.Forms;

namespace CustomControls
{
    public partial class LoadingPanel : UserControl
    {
        public LoadingPanel()
        {
            InitializeComponent();
        }

        public void SetMessage(string message)
        {
            lblMessage.Text = message;
        }
    }
}
