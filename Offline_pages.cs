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
    public partial class Offline_pages : Form
    {
        public Offline_pages()
        {
            InitializeComponent();
        }


        string path = @"C:\EZ-5\";
        private void Offline_pages_Load(object sender, EventArgs e)
        {

            
            foreach (string file in Directory.GetFiles(path,"*.pdf"))
            {
                dataGridView1.Rows.Add(Path.GetFileNameWithoutExtension(file));
            
            }


            //chromiumWebBrowser1.LoadUrlAsync();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                chromiumWebBrowser1.LoadUrlAsync(path + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".pdf");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (string file in Directory.GetFiles(path,"*.pdf"))
            {
                if (file.Contains(textBox1.Text))
                {
                    dataGridView1.Rows.Add(Path.GetFileNameWithoutExtension(file));
                }
            
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
