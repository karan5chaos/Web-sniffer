using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searcher_A
{
    public partial class Quote_of_the_day : Form
    {
        public Quote_of_the_day()
        {
            InitializeComponent();
        }


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

        private void Quote_of_the_day_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.q_hide;

            string url = "https://free-quotes-api.herokuapp.com/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

          //  StreamReader reader = new StreamReader(resStream);

            using (StreamReader Reader = new StreamReader(resStream))
            {
                var myObject = JsonConvert.DeserializeObject<Quotes>(Reader.ReadToEnd());

                richTextBox1.Text = myObject.quote + Environment.NewLine + Environment.NewLine + "- " + myObject.author;
            }


            //string text = reader.ReadToEnd();

            


        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.q_hide = Convert.ToBoolean(checkBox1.CheckState);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
