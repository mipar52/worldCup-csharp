using CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupForms.UIUtils
{
    public class LoadingPanelUtils
    {
        public static LoadingPanel ShowLoadingPanel(Form form, string message = "Loading...")
        {
            var loadingPanel = new LoadingPanel
            {
                Width = 300,
                Height = 80
            };

            // 2. Center it using ClientSize
            loadingPanel.Location = new Point(
                (form.ClientSize.Width - loadingPanel.Width) / 2,
                (form.ClientSize.Height - loadingPanel.Height) / 2
            );

            // 3. Add and show it
            loadingPanel.SetMessage(message);
            form.Controls.Add(loadingPanel);
            loadingPanel.BringToFront();
            loadingPanel.Visible = true;
            loadingPanel.Refresh();
            return loadingPanel;
        }
        public static void HideLoadingPanel(Form form)
        {
            var loadingPanel = form.Controls.OfType<Panel>().FirstOrDefault(p => p.BackColor == Color.FromArgb(128, 0, 0, 0));
            if (loadingPanel != null)
            {
                form.Controls.Remove(loadingPanel);
            }
        }
    }
}
