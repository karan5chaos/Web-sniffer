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
    public partial class Add_new_website : Form
    {
        public Add_new_website()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var addnew = File.ReadAllText(Properties.Settings.Default.save_path + "links/links.lnks");

                addnew += Environment.NewLine + textBox1.Text + "," + textBox2.Text + "," + comboBox1.Text;

                File.WriteAllText(track_change.link_path, addnew);
                this.Close();
            }
        }
    }
}
