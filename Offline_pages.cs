using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void Offline_pages_Load(object sender, EventArgs e)
        {
            chromiumWebBrowser1.LoadUrlAsync(@"C:\EZ-5\cat_Wikipedia_page.pdf");
        }
    }
}
