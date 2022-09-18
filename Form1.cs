
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using Newtonsoft.Json;

namespace EVEOver
{
    public partial class Form1 : Form
    {
        public static Form1? instance;
        Point location = Point.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;
            this.TopMost = true;
            instance = this;
            textBox1.Text = Properties.Settings.Default.localX.ToString();
            textBox2.Text = Properties.Settings.Default.localY.ToString();
        }

        public void addTextToSystemsLoaded(string _s)
        {
            systemsLoaded.Text = _s;
        }

        public void setProgress(string _s)
        {
            progress.Text = _s;
        }

        public void setLowestTruesec(string _s)
        {
            lowestTruesec.Text = "Lowest Truesec in Region: " + _s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils.initDatabase();
        }

        async void lookupNameKills()
        {
            string charName = nameInput.Text;

            MessageBox.Show(Utils.getSystemTruesec(1.0f,charName).ToString());
        }
    }
}