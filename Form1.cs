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
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            Cef.Initialize(settings);

            InitializeComponent();
        }

        string query = "";
        string tab_name = "";
        string prim_path = Properties.Settings.Default.save_path;
        string setting_path = track_change.link_path;


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

        void create_folder()
        {
            if (!Directory.Exists(Properties.Settings.Default.save_path))
            {
                Directory.CreateDirectory(Properties.Settings.Default.save_path);
            }

            if (!Directory.Exists(Properties.Settings.Default.save_path + "links"))
            {
                Directory.CreateDirectory(Properties.Settings.Default.save_path + "/links");

                if (!File.Exists(track_change.link_path))
                {
                    File.Create(track_change.link_path);
                }
            }

            if (!Directory.Exists(Properties.Settings.Default.save_path + "pages"))
                {
                    Directory.CreateDirectory(Properties.Settings.Default.save_path + "/pages");
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            create_folder();

            if (Properties.Settings.Default.q_hide == false)
            {
                new Quote_of_the_day().ShowDialog();
            
            }

            load_tabs();

            timer1.Start();
        }


        public static void UnloadTabpage(TabControl tabcontrol)
        {
            while (tabcontrol.Controls.Count > 0) tabcontrol.Controls[0].Dispose();
        }

        void load_tabs()
        {
            tabControl1.Hide();
            UnloadTabpage(tabControl1);
            
           
            foreach (string line in File.ReadAllLines(setting_path))
            {
                var split = line.Split(',');

                if (split[2]=="true")
                {
                    TabPage page = new TabPage();
                    page.Name = split[0] + "tab";
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
            tabControl1.Show();
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
                button4.Text = "Offline page available!";

            }
            else
            {
               // button4.Text = "Save for offline use";

            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            query = textBox1.Text;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (track_change.changed == false && query!="")
            {
                tab_name = tabControl1.SelectedTab.Text;
                if (Properties.Settings.Default.Unload_on_idle)
                {
                    TabControl tabControl = sender as TabControl;
                    get_browser(tabControl).LoadUrlAsync(get_browser(tabControl).Tag.ToString().Replace("*query*", query));
                }
            }
        }

        private void tabControl1_Deselected(object sender, TabControlEventArgs e)
        {
            
            TabControl tabControl = sender as TabControl;

            try
            {
                if (Properties.Settings.Default.Unload_on_idle && track_change.changed==false && query!="")
                {
                    get_browser(tabControl1).LoadUrlAsync("http://www.blankwebsite.com/");
                }
            }

            catch
            { }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings_page().ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (track_change.changed == true)
            {
                load_tabs();
                track_change.changed = false;
            
            }
        }

        public ChromiumWebBrowser get_browser(TabControl tabcontrol)
        {
            ChromiumWebBrowser browser = new ChromiumWebBrowser();
            foreach (Control control in tabcontrol.SelectedTab.Controls)
            {
                browser = (ChromiumWebBrowser)control;
            }

            return browser;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            get_browser(tabControl1).Back();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            get_browser(tabControl1).Forward();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            get_browser(tabControl1).LoadUrlAsync("http://www.blankwebsite.com/");
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string path = track_change.pages_path + textBox1.Text + "_" + tabControl1.SelectedTab.Text + "_page.pdf";
            PrintToPdfAsync(path, get_browser(tabControl1));

            status.Text = "Page downloaded for offline use..";
        }

        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Quote_of_the_day().ShowDialog();
        }
    }
}
