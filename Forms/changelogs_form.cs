using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searcher_A.Forms
{
    public partial class changelogs_form : Form
    {
        public changelogs_form()
        {
            InitializeComponent();
        }

        private void changelogs_form_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Properties.Resources.changelog;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
