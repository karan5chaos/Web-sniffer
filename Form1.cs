using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        string wikiurl;
        string schurl;
        string cturl;
        string metaurl;
        string briturl;
        string wikitionaryurl;
        string wikidataurl;
        string infogalacticurl;
        string encyclopediaurl;
        string youurl;

        private void button1_Click(object sender, EventArgs e)
        {



            //wikiurl = ("https://en.wikipedia.org/wiki/" + query);
            //schurl = ("http://www.scholarpedia.org/w/index.php?search=" + query);
            //cturl = ("https://citizendium.org/wiki/index.php?search=" + query);
            //metaurl = ("https://metacademy.org/search?q=" + query);
            //briturl = ("https://www.britannica.com/search?query=" + query);
            //wikitionaryurl = ("https://en.wiktionary.org/wiki/" + query);
            //wikidataurl = ("https://www.wikidata.org/w/index.php?search=" + query + "&ns0=1&ns120=1");
            //infogalacticurl = ("https://infogalactic.com/info/"+ query);
            //encyclopediaurl = "https://www.encyclopedia.com/gsearch?q=" + query;
            //youurl = "https://www.youtube.com/results?search_query=" + query;

            tab_name = tabControl1.SelectedTab.Text;

            chromiumWebBrowser1.Load(chromiumWebBrowser1.Tag.ToString() + query);
            Scholarpedia_wb.LoadUrlAsync(Scholarpedia_wb.Tag.ToString() + query);
            ctwb.LoadUrlAsync(ctwb.Tag.ToString() + query);
            metawb.LoadUrlAsync(metawb.Tag.ToString() + query);
            britwb.LoadUrlAsync(britwb.Tag.ToString() + query);
            wiktionarywb.LoadUrlAsync(wiktionarywb.Tag.ToString() + query);
            wikidatawb.LoadUrlAsync(wikidatawb.Tag.ToString() + query + "&ns0=1&ns120=1");
            infogalacticwb.LoadUrlAsync(infogalacticwb.Tag.ToString() + query);
            enwb.LoadUrlAsync(enwb.Tag.ToString() + query);
            youwb.LoadUrlAsync(youwb.Tag.ToString() + query);


            is_loaded = true;


        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.Unload_on_idle;
            
        }

        private void Scholarpedia_wb_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {

        }

        bool is_loaded = false;

       

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

        private void wikipediatab_Leave(object sender, EventArgs e)
        {
            //chromiumWebBrowser1.Dispose();
            chromiumWebBrowser1.LoadUrlAsync("http://www.blankwebsite.com/");
        }

        private void wikipediatab_Enter(object sender, EventArgs e)
        {
            string wikiurl = ("https://en.wikipedia.org/wiki/" + query);
            chromiumWebBrowser1.LoadUrlAsync(wikiurl);
            //chromiumWebBrowser1.ResumeLayout();
        }

        private void ctwb_Leave(object sender, EventArgs e)
        {
            
        }


        //Leave events



        private void Common_tabLeave(object sender, EventArgs e)
        {
            
        }


        private void Common_tabEnter(object sender, EventArgs e)
        {

           
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
                    if (browser.Name == "wikidatawb")
                    {
                        browser.LoadUrlAsync(browser.Tag.ToString() + query + "&ns0=1&ns120=1");
                    }
                    else
                    {
                        browser.LoadUrlAsync(browser.Tag.ToString() + query);
                    }

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
    }
}
