using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searcher_A
{
    public partial class help_page : Form
    {
        public help_page()
        {
            InitializeComponent();
        }

        public static string ReadTextResourceFromAssembly(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        private void help_page_Load(object sender, EventArgs e)
        {
           // chromiumWebBrowser1.Text = ReadTextResourceFromAssembly(Properties.Resources.help_0_html);
        }
    }
}
