﻿using System;
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
            InitialiseForm();
        }

        private void InitialiseForm()
        {
            BrowserTabs.Height = ClientRectangle.Height - 25;
        }

        private void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
            BrowserTabs.TabPages[0].Dispose();
            //BrowserTabs.TabPages[0].Dispose();
            AddBrowserTab();
        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            Navigate(toolStripAddressBar.Text);
        }

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            var selectedBrowser = (ChromiumWebBrowser)sender;
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripAddressBar.Text = e.Address;
            }));
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            var selectedBrowser = (ChromiumWebBrowser)sender;
            this.Invoke(new MethodInvoker(() =>
            {
                selectedBrowser.Parent.Text = e.Title;
            }));
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }

        private void toolStripAddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(toolStripAddressBar.Text);
            }
        }

        private void Navigate(string address)
        {
            try
            {
                var selectedBrowser = (ChromiumWebBrowser)BrowserTabs.SelectedTab.Controls[0];
                selectedBrowser.Load(address);
            }
            catch
            {

            }
        }

        private void toolStripButtonAddTab_Click(object sender, EventArgs e)
        {
            AddBrowserTab();
            //select the latest browser tab
            BrowserTabs.SelectedTab = BrowserTabs.TabPages[BrowserTabs.TabPages.Count - 2];
        }

        private void AddBrowserTab()
        {
            //adding a tab
            var newTabPage = new TabPage();
            newTabPage.Text = "New Tab";
            //BrowserTabs.TabPages.Add(newTabPage);
            BrowserTabs.TabPages.Insert(BrowserTabs.TabPages.Count - 1, newTabPage);


            //adding browser
            browser = new ChromiumWebBrowser("https://datorium.eu");
            browser.Dock = DockStyle.Fill;
            browser.AddressChanged += Browser_AddressChanged;
            browser.TitleChanged += Browser_TitleChanged;
            browser.TitleChanged += Browser_TitleChanged;
            newTabPage.Controls.Add(browser);
        }


        private void BrowserTabs_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Tab Index Changed");
            if (BrowserTabs.SelectedTab == BrowserTabs.TabPages[BrowserTabs.TabPages.Count - 1])
            {
                AddBrowserTab();
                //select the latest browser tab
                BrowserTabs.SelectedTab = BrowserTabs.TabPages[BrowserTabs.TabPages.Count - 2];
            }
        }
    }
}
