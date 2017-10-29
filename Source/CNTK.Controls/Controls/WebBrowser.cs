using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CNTK.Controls
{
    public class WebBrowser : System.Windows.Forms.WebBrowser
    {
        #region Events

        /// <summary>
        /// Raised when the browser application quits.
        /// </summary>
        public event EventHandler Quit;

        #endregion

        #region Quit event
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowsMessages.WM_PARENTNOTIFY)
            {
                var wp = m.WParam.ToInt32();

                var X = wp & 0xFFFF;
                if (X == (int)WindowsMessages.WM_DESTROY) OnQuit();
            }

            base.WndProc(ref m);
        }

        /// <summary>   
        /// A list of all the available window messages.   
        /// </summary>   
        enum WindowsMessages
        {
            WM_DESTROY = 0x2,
            WM_PARENTNOTIFY = 0x210,
        }

        /// <summary>   
        /// Raises the <see cref="Quit"/> event.   
        /// </summary>   
        protected void OnQuit()
        {
            Quit?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Cookie
        /// <summary>
        /// Set the cookie.
        /// </summary>
        public void SetCookie(string url, string cookie)
        {
            foreach (string c in cookie.Split(';'))
            {
                string[] item = c.Split('=');
                if (item.Length == 2)
                {
                    InternetSetCookie(url, null, new Cookie(HttpUtility.UrlEncode(item[0]).Replace("+", ""), HttpUtility.UrlEncode(item[1]), "; expires = Session GMT", "/").ToString());
                }
            }
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        protected static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);
        #endregion
    }
}
