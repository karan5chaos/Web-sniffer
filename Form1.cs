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

        private void button1_Click(object sender, EventArgs e)
        {

            string query = textBox1.Text;

            string wikiurl = ("https://en.wikipedia.org/wiki/" + query);
            string schurl = ("http://www.scholarpedia.org/w/index.php?search=" + query);
            string cturl = ("https://citizendium.org/wiki/index.php?search=" + query);
            string metaurl = ("https://metacademy.org/search?q=" + query);
            string briturl = ("https://www.britannica.com/search?query=" + query);
            string wikitionaryurl = ("https://en.wiktionary.org/wiki/" + query);
            string wikidataurl = ("https://www.wikidata.org/w/index.php?search=" + query + "&ns0=1&ns120=1");
            string infogalacticurl = ("https://infogalactic.com/info/"+ query);
            string encyclopediaurl = "https://www.encyclopedia.com/gsearch?q=" + query;


            chromiumWebBrowser1.LoadUrlAsync(wikiurl);
            Scholarpedia_wb.LoadUrlAsync(schurl);
            ctwb.LoadUrlAsync(cturl);
            metawb.LoadUrlAsync(metaurl);
            britwb.LoadUrlAsync(briturl);
            wiktionarywb.LoadUrlAsync(wikitionaryurl);
            wikidatawb.LoadUrlAsync(wikidataurl);
            infogalacticwb.LoadUrlAsync(infogalacticurl);
            enwb.LoadUrlAsync(encyclopediaurl);
            // Scholarpedia_wb.WaitForInitialLoadAsync();





            is_loaded = true;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Scholarpedia_wb_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {

            //  if(Scholarpedia_wb.)

            

        }

        bool is_loaded = false;

        private void Scholarpedia_wb_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //var txt = Scholarpedia_wb.GetMainFrame().GetTextAsync().Result;

            ////  Scholarpedia_wb.Find("There is currently no text in this page", true, false, false);


            ////MessageBox.Show();
            //if (txt.Contains("There is currently no text in this page"))
            //{

            //    Scholarpedia_wb.Load("http://www.scholarpedia.org/w/index.php?search=" + textBox1.Text);


            //}

            
            //string schurl = ("http://www.scholarpedia.org/article/" + textBox1.Text);

           

            

        }

        private void button2_Click(object sender, EventArgs e)
        {


            // chromiumWebBrowser1.WaitForInitialLoadAsync();
            //Scholarpedia_wb.ViewSource();
            //chromiumWebBrowser1.GetSourceAsync().ContinueWith(taskHtml =>
            //{
            //    var html = taskHtml.Result;
            //    File.WriteAllText("C:/EZ-5/page_html.html",html);

            //    chromiumWebBrowser1.Load("C:/EZ-5/"+textBox1.Text+"_page.html");

            //});

            ChromiumWebBrowser browser = null;
            string path = "";
            //switch (tabControl1.SelectedTab.Name)
            //{
            //    case "wikipediatab":
            //        {

            //            browser = chromiumWebBrowser1;
            //            path = "C:/EZ-5/" + textBox1.Text + "_wikipedia_page.pdf";


            //        }
            //        break;


            //    case "schpediatab":
            //        {

            //            browser = Scholarpedia_wb;
            //            path = "C:/EZ-5/" + textBox1.Text + "_scholarpedia_page.pdf";
            //        }
            //        break;

            //    case "citendiumtab":
            //        {

            //            browser = ctwb;
            //            path = "C:/EZ-5/" + textBox1.Text + "_ct_page.pdf";
            //        }
            //        break;

            //    case "metatab":
            //        {

            //            browser = metawb;
            //            path = "C:/EZ-5/" + textBox1.Text + "_metacademy_page.pdf";
            //        }
            //        break;

            //    case "brittab":
            //        {

            //            browser = britwb;
            //            path = "C:/EZ-5/" + textBox1.Text + "_brit_page.pdf";
            //        }
            //        break;

            //    case "wiktionarytab":
            //        {

            //            browser = wiktionarywb;
            //            path = "C:/EZ-5/" + textBox1.Text + "_wiktionary_page.pdf";
            //        }
            //        break;


            foreach (Control control in tabControl1.SelectedTab.Controls)
            {
                browser = (ChromiumWebBrowser)control;
                path = "C:/EZ-5/" + textBox1.Text + "_"+tabControl1.SelectedTab.Text+"_page.pdf";

            }






            PrintToPdfAsync(path,browser);
           // Cef.Shutdown();



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
    }
}
