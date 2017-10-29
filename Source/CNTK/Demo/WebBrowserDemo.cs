using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNTK.Demo
{
    public partial class WebBrowserDemo : Form
    {
        public WebBrowserDemo()
        {
            InitializeComponent();

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(@"http://www.baidu.com");
            webBrowser1.NewWindow += WebBrowser1_NewWindow;
            webBrowser1.Quit += WebBrowser1_Quit;
        }

        private void WebBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            var wb = (WebBrowser)sender;
            var url = wb.Document.ActiveElement.GetAttribute("href");
            wb.Navigate(url);

            e.Cancel = true;
        }

        private void WebBrowser1_Quit(object sender, EventArgs e)
        {
            MessageBox.Show("quit!");
        }
    }
}
