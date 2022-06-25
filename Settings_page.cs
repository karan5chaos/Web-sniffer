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
    public partial class Settings_page : Form
    {
        public Settings_page()
        {
            InitializeComponent();
        }



        string setting_path = track_change.link_path;

        private void Settings_page_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.Unload_on_idle;
            foreach (string line in File.ReadAllLines(setting_path))
            {

                var split = line.Split(',');

                dataGridView1.Rows.Add(Convert.ToBoolean(split[2]),split[0],split[1]);                
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

           

        }


        bool ischanged = false;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.ColumnIndex==0)
            {
                var line = File.ReadLines(setting_path).ElementAtOrDefault(dataGridView1.SelectedRows[0].Index);


                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0];


                if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[0].Value) == true)
                {

                    string[] arrLine = File.ReadAllLines(setting_path);
                    arrLine[dataGridView1.SelectedRows[0].Index] = arrLine[dataGridView1.SelectedRows[0].Index].Replace("true","false");
                    File.WriteAllLines(setting_path, arrLine);

                    ch1.Value = false;
                    
                }
                else if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[0].Value) == false)
                {
                    string[] arrLine = File.ReadAllLines(setting_path);
                    arrLine[dataGridView1.SelectedRows[0].Index] = arrLine[dataGridView1.SelectedRows[0].Index].Replace("false", "true");
                    File.WriteAllLines(setting_path, arrLine);

                    ch1.Value = true;

                }

                ischanged = true;


               
            }
        }

        private void Settings_page_FormClosing(object sender, FormClosingEventArgs e)
        {
            track_change.changed = ischanged;
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Add_new_website().ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Unload_on_idle = Convert.ToBoolean(checkBox1.CheckState);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
