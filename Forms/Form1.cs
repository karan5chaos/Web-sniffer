using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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
            }

            if (!File.Exists(track_change.link_path))
            {
                //File.Create(track_change.link_path);
                File.WriteAllBytes(track_change.link_path, Properties.Resources.links);
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
                Quote_of_the_day quote = new Quote_of_the_day();

                quote.WindowState = FormWindowState.Minimized;
                quote.Show();
                
                //quote.Visible = false;
                

                
                //new Quote_of_the_day().Hide();


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

            try
            {
                foreach (string line in File.ReadAllLines(setting_path))
                {
                    var split = line.Split(',');

                    if (split[2] == "true")
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
            catch(Exception exc)
            {
                MessageBox.Show("Error loading tabs..\n" +exc.Message);
            
            }
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
            try
            {
                if (track_change.changed == false && query != "")
                {
                    tab_name = tabControl1.SelectedTab.Text;
                    if (Properties.Settings.Default.Unload_on_idle)
                    {
                        TabControl tabControl = sender as TabControl;
                        get_browser(tabControl).LoadUrlAsync(get_browser(tabControl).Tag.ToString().Replace("*query*", query));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while selecting tabs tabs..\n" + exc.Message);

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

            catch (Exception exc)
            {
                MessageBox.Show("Error while unloading tabs..\n" + exc.Message);

            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings_page().ShowDialog();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
           
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
            try
            {
                string path = track_change.pages_path + textBox1.Text + "_" + tabControl1.SelectedTab.Text + "_page.pdf";
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                PrintToPdfAsync(path, get_browser(tabControl1));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                status.Text = "Page downloaded for offline use..";
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while saving page ..\n" + exc.Message);

            }
        }

        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Quote_of_the_day().ShowDialog();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            //tabControl1.Visible = false;
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

           
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            var connectedd = (bool) e.UserState;

            if (connectedd)
            {
                connectionToolStripMenuItem.Image = Properties.Resources.bullet_green;
                connectionToolStripMenuItem.Text = "Connected";
            }
            else if(connected==false)
            {
                connectionToolStripMenuItem.Image = Properties.Resources.bullet_red;
                connectionToolStripMenuItem.Text = "Not Connected";
            }

        }

        bool connected;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    connected = true;
                    backgroundWorker1.ReportProgress(0, connected);
                }
            }
            catch
            {
                connected = false;
                backgroundWorker1.ReportProgress(0, connected);
            }

           
        }

        private void importLinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void exportLinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

        }

        private void exportAllOfflinePagesTozipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();


        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            File.Delete(track_change.link_path);
            File.Move(openFileDialog1.FileName,track_change.link_path);
            MessageBox.Show("Links imported successfully.", "Import completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //File.Replace(track_change.link_path, openFileDialog1.FileName,openFileDialog1.FileName+".bak");
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            File.Copy(track_change.link_path, saveFileDialog1.FileName);
            MessageBox.Show("Links exported successfully.", "Export completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            pdf_exportworker.RunWorkerAsync();
        }

        private void pdf_exportworker_DoWork(object sender, DoWorkEventArgs e)
        {
            ZipFile.CreateFromDirectory(track_change.pages_path, saveFileDialog2.FileName);
        }

        private void pdf_exportworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Pages exported to zip file.","Export completed",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new help_page().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About_page().ShowDialog();
        }
    }
}
