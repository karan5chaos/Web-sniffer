using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;

namespace Searcher_A
{


    public partial class Settings_page : Form
    {
        public Settings_page()
        {
            
            InitializeComponent();
            this.dataGridView1.DoubleBuffered(true);
        }



        string setting_path = track_change.link_path;
      
        private void Settings_page_Load(object sender, EventArgs e)
        {

            checkBox1.Checked = Properties.Settings.Default.Unload_on_idle;
            checkBox2.Checked = Properties.Settings.Default.q_hide;
            textBox1.Text = Properties.Settings.Default.d_save_folder_name;
            checkBox4.Checked = Properties.Settings.Default.auto_backup;

            textBox2.Text = track_change.link_path;
            textBox3.Text = track_change.pages_path;
            textBox4.Text = Properties.Settings.Default.save_path;
            textBox5.Text = Properties.Settings.Default.lins_path;

            load_links();
          
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

           

        }


        bool ischanged = false;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0 && e.ColumnIndex == 0)
                {
                    var line = File.ReadLines(setting_path).ElementAtOrDefault(dataGridView1.SelectedRows[0].Index);


                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0];


                    if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[0].Value) == true)
                    {

                        string[] arrLine = File.ReadAllLines(setting_path);
                        arrLine[dataGridView1.SelectedRows[0].Index] = arrLine[dataGridView1.SelectedRows[0].Index].Replace("true", "false");
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
            catch (Exception exc)
            {
                MessageBox.Show("Error while changing state..\n" + exc.Message);

            }
        }

        private void Settings_page_FormClosing(object sender, FormClosingEventArgs e)
        {
            track_change.changed = ischanged;
            Properties.Settings.Default.d_save_folder_name = textBox1.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Unload_on_idle = Convert.ToBoolean(checkBox1.CheckState);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        void load_links()
        {
            dataGridView1.Rows.Clear();
            backgroundWorker1.RunWorkerAsync();
        
        
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (string line in File.ReadAllLines(setting_path))
                {
                    var split = line.Split(',');
                    backgroundWorker1.ReportProgress(0, split);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while loading links..\n" + exc.Message);

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //split = null;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            var split = e.UserState as string[];
            dataGridView1.Rows.Add(Convert.ToBoolean(split[2]), split[0], split[1]);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.q_hide = Convert.ToBoolean(checkBox2.CheckState);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dr = new Add_new_website().ShowDialog();

            if (dr == DialogResult.OK)
            {
                load_links();
            
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox1.Enabled = true;

            }
            else
            {
                textBox1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var index = dataGridView1.SelectedRows[0].Index;

                string[] arrLine = File.ReadAllLines(setting_path);
                arrLine[index] = "";
                //var rem = arrLine[index].Where(l => l.Trim().Length > 0);

                //arrLine[index] = arrLine[index].Remove(index);
                File.WriteAllLines(setting_path, arrLine);

                var lines = File.ReadAllLines(setting_path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines(setting_path, lines);

                load_links();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while deleting link..\n" + exc.Message);

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(textBox2.Text);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox5.Checked;

            if (checkBox5.Checked)
            {
                Transition.run(checkBox5, "ForeColor", Color.Black  , new TransitionType_Flash(999999, 99999));

            }
            else
            {
                Transition.run(checkBox5, "ForeColor", Color.DarkRed, new TransitionType_Deceleration(1000));
                checkBox5.ForeColor = Color.DarkRed;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.auto_backup = checkBox4.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
