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

namespace Searcher_A
{
    public partial class About_page : Form
    {
        public About_page()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.IO.Stream str = Properties.Resources.Cute_cat_meow_sound;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:karan@nextschool.org");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("www.linkedin.com/in/karanpiprani");
        }
    }
}
