using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searcher_A
{



    public partial class Offline_pages : Form
    {
        public Offline_pages()
        {
            InitializeComponent();
            this.dataGridView1.DoubleBuffered(true);
        }

        private void Offline_pages_Load(object sender, EventArgs e)
        {
            
            

            //if(track_change.page_saved == false)
            fileSystemWatcher1.Path = track_change.pages_path;

            load_files();
            fr = new Form1();
            timer1.Start();
        }

        public void load_files()
        {
            try
            {
                dataGridView1.Rows.Clear();

                foreach (string file in Directory.EnumerateFiles(track_change.pages_path, "*.pdf"))
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(dataGridView1);
                    dr.SetValues(Path.GetFileNameWithoutExtension(file));
                    dr.Tag = file;
                    dataGridView1.Rows.Add(dr);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while listing pages..\n" + exc.Message);

            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var datagrid = sender as DataGridView;
                if (dataGridView1.Rows.Count > 0 && datagrid.CurrentRow.Tag != null)
                {
                    chromiumWebBrowser1.LoadUrlAsync(datagrid.CurrentRow.Tag.ToString());

                    string rtffile = Properties.Settings.Default.pages_path + "/" + dataGridView1.CurrentRow.Cells[0].Value + ".rtf";

                    //if (File.Exists(rtffile))
                    //{
                    //    richTextBoxPrintCtrl1.Rtf = File.ReadAllText(rtffile);
                    //}
                   
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while loading page..\n" + exc.Message);

            }
        }

        //Test method
        //public void manipulatePdf(string outputFile, string highLightFile)
        //{
        //    PdfReader reader = new PdfReader(outputFile);

        //    using (FileStream fs = new FileStream(highLightFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        //    {
        //        using (PdfStamper stamper = new PdfStamper(reader, fs))
        //        {
        //            //Create a rectangle for the highlight. NOTE: Technically this isn't used but it helps with the quadpoint calculation
        //            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(60.6755f, 749.172f, 94.0195f, 735.3f);
        //            //Create an array of quad points based on that rectangle. NOTE: The order below doesn't appear to match the actual spec but is what Acrobat produces
        //            float[] quad = { rect.Left, rect.Bottom, rect.Right, rect.Bottom, rect.Left, rect.Top, rect.Right, rect.Top };

        //            //Create our hightlight
        //            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);

        //            //Set the color
        //            highlight.Color = iTextSharp.text.BaseColor.YELLOW;

        //            //Add the annotation
        //            stamper.AddAnnotation(highlight, 1);
        //        }
        //    }
        //}
        //Test Method

    //bool search = false;
    private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
                dataGridView1.Rows.Clear();
                foreach (string file in Directory.GetFiles(track_change.pages_path, "*.pdf"))
                {
                    if (file.Contains(textBox1.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.CreateCells(dataGridView1);
                        dr.SetValues(Path.GetFileNameWithoutExtension(file));
                        dr.Tag = file;
                        dataGridView1.Rows.Add(dr);
                    }

                    dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while searching for files..\n" + exc.Message);

            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {


            if (textBox1.Text == "")
            {
                dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
                load_files();
                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            }
            else
            {
                button1_Click(null, null);

            }


        }

        private void deletePageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Delete selected page?", "Delete item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    File.Delete(dataGridView1.CurrentRow.Tag.ToString());
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error while deleting a file..\n" + exc.Message);

                }
                load_files();
            }

        }

        private void openPageExternallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(dataGridView1.CurrentRow.Tag.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            toolStripProgressBar1.Visible = true;
            toolStripStatusLabel1.Text = "Uploading files..";
            backgroundWorker1.RunWorkerAsync();


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                track_change.Check_folder(Properties.Settings.Default.d_save_folder_name);
                foreach (string file in Directory.EnumerateFiles(track_change.pages_path, "*.pdf"))
                {
                    using (Stream sr = File.OpenRead(file))
                    {
                        track_change.UploadFile(sr, Path.GetFileName(file), "application/pdf", Properties.Settings.Default.d_folder_name, "");
                        backgroundWorker1.ReportProgress(0, track_change.message);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error occurred..\n" + exc.Message);

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            button2.Enabled = true;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Text = "Files uploaded successfully..";
        }

        Form1 fr;
        private void timer1_Tick(object sender, EventArgs e)
        {

            
            
            if (track_change.g_drive_process)
            {
                button2.Enabled = false;

            }
            else
            {
                button2.Enabled = true;
            }

            //if (track_change.page_saved==true)
            //{
            //    load_files();
            //    track_change.page_saved = false;
            //}
            

            // toolStripStatusLabel1.Text = track_change.message;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var split = e.UserState as string;
            toolStripStatusLabel1.Text = split;
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
           
            
        }

        private void Offline_pages_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Offline_pages_FormClosing(object sender, FormClosingEventArgs e)
        {
            track_change.page_saved = false;
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (track_change.page_saved == false || this.Visible==true)
            {
                load_files();
            }
                
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //manipulatePdf(dataGridView1.CurrentRow.Tag.ToString(), dataGridView1.CurrentRow.Tag.ToString());
        }


        //public class convert { 



        public class Quotes
        {
            public string author
            {
                get;
                set;
            }
            public string quote
            {
                get;
                set;
            }
        }

        //}
        private void button3_Click_1(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // richTextBoxPrintCtrl1.SelectionBackColor = Color.Yellow;
        }

        private void strikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // richTextBoxPrintCtrl1.SelectionBullet = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string rtffile = Properties.Settings.Default.pages_path + "/" + dataGridView1.CurrentRow.Cells[0].Value + ".rtf";
            //File.WriteAllText(rtffile, richTextBoxPrintCtrl1.Rtf);
        }
    }


}
