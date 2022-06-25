using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        }


        //string path = @"C:\EZ-5\";
        private void Offline_pages_Load(object sender, EventArgs e)
        {



            load_files();

            //chromiumWebBrowser1.LoadUrlAsync();
        }

        void load_files()
        {
            //dataGridView1.ClearSelection();
            dataGridView1.Rows.Clear();

            foreach (string file in Directory.GetFiles(track_change.pages_path, "*.pdf"))
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dataGridView1);
                dr.SetValues(Path.GetFileNameWithoutExtension(file));
                dr.Tag = file;
                dataGridView1.Rows.Add(dr);

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            var datagrid = sender as DataGridView;
            if (dataGridView1.Rows.Count > 0 && datagrid.CurrentRow.Tag!=null)
            {

                

                chromiumWebBrowser1.LoadUrlAsync(datagrid.CurrentRow.Tag.ToString());
                
            }
            
        }

        //bool search = false;
        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            dataGridView1.Rows.Clear();
            foreach (string file in Directory.GetFiles(track_change.pages_path,"*.pdf"))
            {
                if (file.Contains(textBox1.Text))
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
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
            if(MessageBox.Show("Delete selected page?", "Delete item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(dataGridView1.CurrentRow.Tag.ToString());
                load_files();
            }
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void openPageExternallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(dataGridView1.CurrentRow.Tag.ToString());
            
        }
    }
}
