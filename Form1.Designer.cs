
namespace Searcher_A
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.wikipediatab = new System.Windows.Forms.TabPage();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.schpediatab = new System.Windows.Forms.TabPage();
            this.Scholarpedia_wb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.citendiumtab = new System.Windows.Forms.TabPage();
            this.ctwb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.metatab = new System.Windows.Forms.TabPage();
            this.metawb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.brittab = new System.Windows.Forms.TabPage();
            this.britwb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.wiktionarytab = new System.Windows.Forms.TabPage();
            this.wiktionarywb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.wikidatatab = new System.Windows.Forms.TabPage();
            this.wikidatawb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.infotab = new System.Windows.Forms.TabPage();
            this.infogalacticwb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.entab = new System.Windows.Forms.TabPage();
            this.enwb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.youtab = new System.Windows.Forms.TabPage();
            this.youwb = new CefSharp.WinForms.ChromiumWebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oflinePagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveForOfflineUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.wikipediatab.SuspendLayout();
            this.schpediatab.SuspendLayout();
            this.citendiumtab.SuspendLayout();
            this.metatab.SuspendLayout();
            this.brittab.SuspendLayout();
            this.wiktionarytab.SuspendLayout();
            this.wikidatatab.SuspendLayout();
            this.infotab.SuspendLayout();
            this.entab.SuspendLayout();
            this.youtab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Query:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(360, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.wikipediatab);
            this.tabControl1.Controls.Add(this.schpediatab);
            this.tabControl1.Controls.Add(this.citendiumtab);
            this.tabControl1.Controls.Add(this.metatab);
            this.tabControl1.Controls.Add(this.brittab);
            this.tabControl1.Controls.Add(this.wiktionarytab);
            this.tabControl1.Controls.Add(this.wikidatatab);
            this.tabControl1.Controls.Add(this.infotab);
            this.tabControl1.Controls.Add(this.entab);
            this.tabControl1.Controls.Add(this.youtab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 354);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.Deselected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Deselected);
            // 
            // wikipediatab
            // 
            this.wikipediatab.Controls.Add(this.chromiumWebBrowser1);
            this.wikipediatab.Location = new System.Drawing.Point(4, 22);
            this.wikipediatab.Name = "wikipediatab";
            this.wikipediatab.Padding = new System.Windows.Forms.Padding(3);
            this.wikipediatab.Size = new System.Drawing.Size(792, 328);
            this.wikipediatab.TabIndex = 0;
            this.wikipediatab.Tag = "";
            this.wikipediatab.Text = "Wikipedia";
            this.wikipediatab.UseVisualStyleBackColor = true;
            this.wikipediatab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.wikipediatab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(3, 3);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(786, 322);
            this.chromiumWebBrowser1.TabIndex = 0;
            this.chromiumWebBrowser1.Tag = "https://en.wikipedia.org/wiki/";
            this.chromiumWebBrowser1.FrameLoadEnd += new System.EventHandler<CefSharp.FrameLoadEndEventArgs>(this.Common_checkoffline);
            // 
            // schpediatab
            // 
            this.schpediatab.Controls.Add(this.Scholarpedia_wb);
            this.schpediatab.Location = new System.Drawing.Point(4, 22);
            this.schpediatab.Name = "schpediatab";
            this.schpediatab.Padding = new System.Windows.Forms.Padding(3);
            this.schpediatab.Size = new System.Drawing.Size(792, 328);
            this.schpediatab.TabIndex = 1;
            this.schpediatab.Tag = "http://www.scholarpedia.org/w/index.php?search=";
            this.schpediatab.Text = "Scholarpedia";
            this.schpediatab.UseVisualStyleBackColor = true;
            this.schpediatab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.schpediatab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // Scholarpedia_wb
            // 
            this.Scholarpedia_wb.ActivateBrowserOnCreation = false;
            this.Scholarpedia_wb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scholarpedia_wb.Location = new System.Drawing.Point(3, 3);
            this.Scholarpedia_wb.Name = "Scholarpedia_wb";
            this.Scholarpedia_wb.Size = new System.Drawing.Size(786, 322);
            this.Scholarpedia_wb.TabIndex = 0;
            this.Scholarpedia_wb.Tag = "http://www.scholarpedia.org/w/index.php?search=";
            // 
            // citendiumtab
            // 
            this.citendiumtab.Controls.Add(this.ctwb);
            this.citendiumtab.Location = new System.Drawing.Point(4, 22);
            this.citendiumtab.Name = "citendiumtab";
            this.citendiumtab.Padding = new System.Windows.Forms.Padding(3);
            this.citendiumtab.Size = new System.Drawing.Size(792, 328);
            this.citendiumtab.TabIndex = 2;
            this.citendiumtab.Text = "Citendium";
            this.citendiumtab.UseVisualStyleBackColor = true;
            this.citendiumtab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.citendiumtab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // ctwb
            // 
            this.ctwb.ActivateBrowserOnCreation = false;
            this.ctwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctwb.Location = new System.Drawing.Point(3, 3);
            this.ctwb.Name = "ctwb";
            this.ctwb.Size = new System.Drawing.Size(786, 322);
            this.ctwb.TabIndex = 0;
            this.ctwb.Tag = "https://citizendium.org/wiki/index.php?search=";
            this.ctwb.Leave += new System.EventHandler(this.ctwb_Leave);
            // 
            // metatab
            // 
            this.metatab.Controls.Add(this.metawb);
            this.metatab.Location = new System.Drawing.Point(4, 22);
            this.metatab.Name = "metatab";
            this.metatab.Padding = new System.Windows.Forms.Padding(3);
            this.metatab.Size = new System.Drawing.Size(792, 328);
            this.metatab.TabIndex = 3;
            this.metatab.Text = "Metacademy";
            this.metatab.UseVisualStyleBackColor = true;
            this.metatab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.metatab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // metawb
            // 
            this.metawb.ActivateBrowserOnCreation = false;
            this.metawb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metawb.Location = new System.Drawing.Point(3, 3);
            this.metawb.Name = "metawb";
            this.metawb.Size = new System.Drawing.Size(786, 322);
            this.metawb.TabIndex = 0;
            this.metawb.Tag = "https://metacademy.org/search?q=";
            // 
            // brittab
            // 
            this.brittab.Controls.Add(this.britwb);
            this.brittab.Location = new System.Drawing.Point(4, 22);
            this.brittab.Name = "brittab";
            this.brittab.Padding = new System.Windows.Forms.Padding(3);
            this.brittab.Size = new System.Drawing.Size(792, 328);
            this.brittab.TabIndex = 4;
            this.brittab.Text = "Britannica";
            this.brittab.UseVisualStyleBackColor = true;
            this.brittab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.brittab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // britwb
            // 
            this.britwb.ActivateBrowserOnCreation = false;
            this.britwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.britwb.Location = new System.Drawing.Point(3, 3);
            this.britwb.Name = "britwb";
            this.britwb.Size = new System.Drawing.Size(786, 322);
            this.britwb.TabIndex = 0;
            this.britwb.Tag = "https://www.britannica.com/search?query=";
            this.britwb.FrameLoadEnd += new System.EventHandler<CefSharp.FrameLoadEndEventArgs>(this.Common_checkoffline);
            // 
            // wiktionarytab
            // 
            this.wiktionarytab.Controls.Add(this.wiktionarywb);
            this.wiktionarytab.Location = new System.Drawing.Point(4, 22);
            this.wiktionarytab.Name = "wiktionarytab";
            this.wiktionarytab.Padding = new System.Windows.Forms.Padding(3);
            this.wiktionarytab.Size = new System.Drawing.Size(792, 328);
            this.wiktionarytab.TabIndex = 5;
            this.wiktionarytab.Text = "Wiktionary";
            this.wiktionarytab.UseVisualStyleBackColor = true;
            this.wiktionarytab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.wiktionarytab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // wiktionarywb
            // 
            this.wiktionarywb.ActivateBrowserOnCreation = false;
            this.wiktionarywb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wiktionarywb.Location = new System.Drawing.Point(3, 3);
            this.wiktionarywb.Name = "wiktionarywb";
            this.wiktionarywb.Size = new System.Drawing.Size(786, 322);
            this.wiktionarywb.TabIndex = 0;
            this.wiktionarywb.Tag = "https://en.wiktionary.org/wiki/";
            // 
            // wikidatatab
            // 
            this.wikidatatab.Controls.Add(this.wikidatawb);
            this.wikidatatab.Location = new System.Drawing.Point(4, 22);
            this.wikidatatab.Name = "wikidatatab";
            this.wikidatatab.Padding = new System.Windows.Forms.Padding(3);
            this.wikidatatab.Size = new System.Drawing.Size(792, 328);
            this.wikidatatab.TabIndex = 6;
            this.wikidatatab.Text = "Wikidata";
            this.wikidatatab.UseVisualStyleBackColor = true;
            this.wikidatatab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.wikidatatab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // wikidatawb
            // 
            this.wikidatawb.ActivateBrowserOnCreation = false;
            this.wikidatawb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wikidatawb.Location = new System.Drawing.Point(3, 3);
            this.wikidatawb.Name = "wikidatawb";
            this.wikidatawb.Size = new System.Drawing.Size(786, 322);
            this.wikidatawb.TabIndex = 0;
            this.wikidatawb.Tag = "https://www.wikidata.org/w/index.php?search=";
            // 
            // infotab
            // 
            this.infotab.Controls.Add(this.infogalacticwb);
            this.infotab.Location = new System.Drawing.Point(4, 22);
            this.infotab.Name = "infotab";
            this.infotab.Padding = new System.Windows.Forms.Padding(3);
            this.infotab.Size = new System.Drawing.Size(792, 328);
            this.infotab.TabIndex = 7;
            this.infotab.Text = "Infogalactic";
            this.infotab.UseVisualStyleBackColor = true;
            this.infotab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.infotab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // infogalacticwb
            // 
            this.infogalacticwb.ActivateBrowserOnCreation = false;
            this.infogalacticwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infogalacticwb.Location = new System.Drawing.Point(3, 3);
            this.infogalacticwb.Name = "infogalacticwb";
            this.infogalacticwb.Size = new System.Drawing.Size(786, 322);
            this.infogalacticwb.TabIndex = 0;
            this.infogalacticwb.Tag = "https://infogalactic.com/info/";
            // 
            // entab
            // 
            this.entab.Controls.Add(this.enwb);
            this.entab.Location = new System.Drawing.Point(4, 22);
            this.entab.Name = "entab";
            this.entab.Padding = new System.Windows.Forms.Padding(3);
            this.entab.Size = new System.Drawing.Size(792, 328);
            this.entab.TabIndex = 8;
            this.entab.Text = "Encyclopedia";
            this.entab.UseVisualStyleBackColor = true;
            this.entab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.entab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // enwb
            // 
            this.enwb.ActivateBrowserOnCreation = false;
            this.enwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enwb.Location = new System.Drawing.Point(3, 3);
            this.enwb.Name = "enwb";
            this.enwb.Size = new System.Drawing.Size(786, 322);
            this.enwb.TabIndex = 0;
            this.enwb.Tag = "https://www.encyclopedia.com/gsearch?q=";
            // 
            // youtab
            // 
            this.youtab.Controls.Add(this.youwb);
            this.youtab.Location = new System.Drawing.Point(4, 22);
            this.youtab.Name = "youtab";
            this.youtab.Padding = new System.Windows.Forms.Padding(3);
            this.youtab.Size = new System.Drawing.Size(792, 328);
            this.youtab.TabIndex = 9;
            this.youtab.Text = "YouTube";
            this.youtab.UseVisualStyleBackColor = true;
            this.youtab.Enter += new System.EventHandler(this.Common_tabEnter);
            this.youtab.Leave += new System.EventHandler(this.Common_tabLeave);
            // 
            // youwb
            // 
            this.youwb.ActivateBrowserOnCreation = false;
            this.youwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.youwb.Location = new System.Drawing.Point(3, 3);
            this.youwb.Name = "youwb";
            this.youwb.Size = new System.Drawing.Size(786, 322);
            this.youwb.TabIndex = 0;
            this.youwb.Tag = "https://www.youtube.com/results?search_query=";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 404);
            this.splitContainer1.SplitterDistance = 46;
            this.splitContainer1.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(551, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Unload page on idle";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(10, 17);
            this.status.Text = "-";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oflinePagesToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.saveForOfflineUseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oflinePagesToolStripMenuItem
            // 
            this.oflinePagesToolStripMenuItem.Name = "oflinePagesToolStripMenuItem";
            this.oflinePagesToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.oflinePagesToolStripMenuItem.Text = "Ofline pages";
            this.oflinePagesToolStripMenuItem.Click += new System.EventHandler(this.oflinePagesToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // saveForOfflineUseToolStripMenuItem
            // 
            this.saveForOfflineUseToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveForOfflineUseToolStripMenuItem.Image = global::Searcher_A.Properties.Resources.download;
            this.saveForOfflineUseToolStripMenuItem.Name = "saveForOfflineUseToolStripMenuItem";
            this.saveForOfflineUseToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.saveForOfflineUseToolStripMenuItem.Text = "Save for offline use";
            this.saveForOfflineUseToolStripMenuItem.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 404);
            this.panel1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.wikipediatab.ResumeLayout(false);
            this.schpediatab.ResumeLayout(false);
            this.citendiumtab.ResumeLayout(false);
            this.metatab.ResumeLayout(false);
            this.brittab.ResumeLayout(false);
            this.wiktionarytab.ResumeLayout(false);
            this.wikidatatab.ResumeLayout(false);
            this.infotab.ResumeLayout(false);
            this.entab.ResumeLayout(false);
            this.youtab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage schpediatab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage wikipediatab;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private CefSharp.WinForms.ChromiumWebBrowser Scholarpedia_wb;
        private System.Windows.Forms.TabPage citendiumtab;
        private CefSharp.WinForms.ChromiumWebBrowser ctwb;
        private System.Windows.Forms.TabPage metatab;
        private CefSharp.WinForms.ChromiumWebBrowser metawb;
        private System.Windows.Forms.TabPage brittab;
        private CefSharp.WinForms.ChromiumWebBrowser britwb;
        private System.Windows.Forms.TabPage wiktionarytab;
        private CefSharp.WinForms.ChromiumWebBrowser wiktionarywb;
        private System.Windows.Forms.TabPage wikidatatab;
        private CefSharp.WinForms.ChromiumWebBrowser wikidatawb;
        private System.Windows.Forms.TabPage infotab;
        private CefSharp.WinForms.ChromiumWebBrowser infogalacticwb;
        private System.Windows.Forms.TabPage entab;
        private CefSharp.WinForms.ChromiumWebBrowser enwb;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oflinePagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage youtab;
        private CefSharp.WinForms.ChromiumWebBrowser youwb;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem saveForOfflineUseToolStripMenuItem;
    }
}

