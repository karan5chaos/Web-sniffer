using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searcher_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            var settings = new CefSettings();
            // settings.Locale = "zh-CN";
            settings.CefCommandLineArgs.Add("disable-gpu", "1");

            Cef.Initialize(settings);

            InitializeComponent();
        }

        string query = "";
        string tab_name = "";
        string prim_path = "C:/EZ-5/";
        string setting_path = @"C:\Users\Karan\source\repos\Searcher_A\TextFile1.txt";

        private void button1_Click(object sender, EventArgs e)
        {





            tab_name = tabControl1.SelectedTab.Text;


            foreach (TabPage page in tabControl1.TabPages)
            {
                foreach (Control control in page.Controls)
                {
                    ChromiumWebBrowser browser = (ChromiumWebBrowser)control;

                    if (browser.Tag.ToString().Contains("*query*"))
                    {
                        string corrected_link = browser.Tag.ToString().Replace("*query*", query);
                        browser.Load(corrected_link);
                    }
                }
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
           // fileSystemWatcher1.Path = setting_path;

            checkBox1.Checked = Properties.Settings.Default.Unload_on_idle;
            load_tabs();
        }


        public static void UnloadTabpage(TabControl tabcontrol)
        {
            while (tabcontrol.Controls.Count > 0) tabcontrol.Controls[0].Dispose();
        }

        void load_tabs()
        {


            UnloadTabpage(tabControl1);
           
            foreach (string line in File.ReadAllLines(setting_path))
            {
                var split = line.Split(',');

                if (split[2]=="true")
                {
                    TabPage page = new TabPage();
                    page.Name = split[0] + "tab";
                    //page.Tag = split[1];
                    page.Text = split[0];


                    ChromiumWebBrowser browser = new ChromiumWebBrowser();
                    browser.Name = split[0] + "wb";
                    browser.Tag = Tag = split[1];

                    browser.Dock = DockStyle.Fill;

                    page.Controls.Add(browser);
                    tabControl1.TabPages.Add(page);

                    browser.FrameLoadEnd += Common_checkoffline;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ChromiumWebBrowser browser = null;
            string path = "";
          
            foreach (Control control in tabControl1.SelectedTab.Controls)
            {
                browser = (ChromiumWebBrowser)control;
                path = prim_path + textBox1.Text + "_"+tabControl1.SelectedTab.Text+"_page.pdf";

            }

            PrintToPdfAsync(path,browser);
            status.Text = "Page downloaded for offline use..";

        }


        public static async Task PrintToPdfAsync(string path, ChromiumWebBrowser browser)
        {
            await browser.PrintToPdfAsync(path);
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null,null);
            
            }
        }

        private void oflinePagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Offline_pages().Show();
        }



        private void Common_checkoffline(object sender, FrameLoadEndEventArgs e)
        {
            string name =  prim_path + query + "_" + tab_name + "_page.pdf";

            if (File.Exists(name))
            {
                saveForOfflineUseToolStripMenuItem.Text = "Offline page available!";

            }
            else
            {
                saveForOfflineUseToolStripMenuItem.Text = "Save for offline use";

            }
        }

      


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Unload_on_idle = Convert.ToBoolean(checkBox1.CheckState);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            query = textBox1.Text;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

            tab_name = tabControl1.SelectedTab.Text;
            if (Properties.Settings.Default.Unload_on_idle)
            {
                TabControl tabControl = sender as TabControl;


                foreach (ChromiumWebBrowser browser in tabControl.SelectedTab.Controls)
                {

                        browser.LoadUrlAsync(browser.Tag.ToString().Replace("*query*",query));
                }

            }
        }

        private void tabControl1_Deselected(object sender, TabControlEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (Properties.Settings.Default.Unload_on_idle)
            {
                foreach (ChromiumWebBrowser browser in tabControl.SelectedTab.Controls)
                {
                    browser.LoadUrlAsync("http://www.blankwebsite.com/");
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings_page().Show();
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
           
        }
    }
}
