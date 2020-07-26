using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace ChromiumBasedBrowser
{
    public partial class Browser : Form
    {
        private ChromiumWebBrowser browser;

        public Browser()
        {
            InitializeComponent();
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("https://youtube.com");
            browser.Dock = DockStyle.Fill;
            this.Controls.Add(browser);
        }
    }
}
