using Auth0.ManagementApi.Models;
using CefSharp;
using CefSharp.WinForms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Searcher_A.Forms;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;
using static Google.Apis.Drive.v3.DriveService;

namespace Searcher_A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
          

            InitializeComponent();
        }

        string query = "";
        string tab_name = "";
       // string prim_path = track_change.pages_path;
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

                        //textBox2.Text = browser.ti
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

            if (!Directory.Exists(Properties.Settings.Default.lins_path))
            {
                Directory.CreateDirectory(Properties.Settings.Default.lins_path);
            }

            if (!File.Exists(track_change.link_path))
            {
                //File.Create(track_change.link_path);
                File.WriteAllBytes(track_change.link_path, Properties.Resources.links);
            }

            if (!Directory.Exists(Properties.Settings.Default.pages_path))
            {
                Directory.CreateDirectory(Properties.Settings.Default.pages_path);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            track_change.page_saved = false;

            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            Cef.Initialize(settings);

            create_folder();

            if (Properties.Settings.Default.q_hide == false)
            {
                Quote_of_the_day quote = new Quote_of_the_day();

                quote.WindowState = FormWindowState.Minimized;
                quote.Show();
            }


            load_tabs();
            timer1.Start();

            if (Properties.Settings.Default.auto_backup)
            {
                track_change.g_drive_process = true;
                drive_backgroundworker.RunWorkerAsync();
            
            }

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

                        browser.TitleChanged += Browser_TitleChanged;
                       // browser.FrameLoadEnd += Common_checkoffline;
                        //browser.ContextMenuStrip = brow_context;

                    }
                }
                tabControl1.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error loading tabs..\n" + exc.Message);

            }
        }

        string title = "";
        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            title = e.Title.ToString();
        }

        public static async Task PrintToPdfAsync(string path, ChromiumWebBrowser browser)
        {
            await browser.PrintToPdfAsync(path);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void oflinePagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Offline_pages().Show();
        }

        string offtxt = "Save for offline use";

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
                       // get_browser(tabControl1).ini
                        TabControl tabControl = sender as TabControl;
                        get_browser(tabControl).Load(get_browser(tabControl).Tag.ToString().Replace("*query*", query));
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
                if (Properties.Settings.Default.Unload_on_idle && track_change.changed == false && query != "")
                {

                    string html = " <html><body></body></html>";
                    //get_browser(tabControl1).Dispose();
                    get_browser(tabControl1).LoadHtml(html);
                    GC.Collect();
                    
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

            textBox2.Text = title;

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

            if (track_change.changed == true)
            {
                load_tabs();
                track_change.changed = false;

            }

            string name = track_change.pages_path + RemoveInvalidFilePathCharacters(textBox2.Text, "-") + ".pdf";

            if (File.Exists(name))
            {
                button6.Enabled = true;
                button5.Enabled = false;
            }
            else
            {
                button6.Enabled = false;
                button5.Enabled = true;
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
            string html = " <html><body></body></html>";
            //get_browser(tabControl1).Dispose();
            get_browser(tabControl1).LoadHtml(html);
            GC.Collect();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string path = track_change.pages_path + RemoveInvalidFilePathCharacters(textBox2.Text,"-") + ".pdf";

               
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                PrintToPdfAsync(path, get_browser(tabControl1));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                status.Text = "Page downloaded for offline use..";
                track_change.page_saved = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while saving page ..\n" + exc.Message);

            }
        }

        public static string RemoveInvalidFilePathCharacters(string filename, string replaceChar)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(filename, replaceChar);
        }

        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Quote_of_the_day().ShowDialog();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {



        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            var connectedd = Convert.ToBoolean(e.UserState);
            if (connectedd)
            {
                connectionToolStripMenuItem.Image = Properties.Resources.bullet_green;
                connectionToolStripMenuItem.Text = "Connected";
            }
            else if (connectedd == false)
            {
                connectionToolStripMenuItem.Image = Properties.Resources.bullet_red;
                connectionToolStripMenuItem.Text = "Not Connected";
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool connected;
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
            File.Move(openFileDialog1.FileName, track_change.link_path);
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
            MessageBox.Show("Pages exported to zip file.", "Export completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new help_page().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About_page().ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Offline_pages pages = new Offline_pages();
                
            string link = track_change.pages_path + RemoveInvalidFilePathCharacters(textBox2.Text, "-") + ".pdf";
            //link = RemoveInvalidFilePathCharacters(link, "-");

            pages.Show();
                //pages.dataGridView1.SelectedRows.Clear();
                foreach (DataGridViewRow row in pages.dataGridView1.Rows)
                {
                    if (row.Tag.ToString() == link)
                    {
                        row.Selected = true;
                        pages.chromiumWebBrowser1.Load(row.Tag.ToString());
                    }

                }
            
        }

        private void drive_backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                track_change.Check_folder(Properties.Settings.Default.d_save_folder_name);
                foreach (string file in Directory.EnumerateFiles(track_change.pages_path, "*.pdf"))
                {
                    using (Stream sr = File.OpenRead(file))
                    {
                        track_change.UploadFile(sr, Path.GetFileName(file), "application/pdf", Properties.Settings.Default.d_folder_name, "");
                        drive_backgroundworker.ReportProgress(0, track_change.message);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error occurred..\n" + exc.Message);

            }
        }

        private void drive_backgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            status.Text = "Auto-backup completed..";
            track_change.g_drive_process = false;
        }

        private void drive_backgroundworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var message = e.UserState as string;
            status.Text = message;
        }

        private void changelogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new changelogs_form().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            get_browser(tabControl1).Reload();
        }
    }
}
