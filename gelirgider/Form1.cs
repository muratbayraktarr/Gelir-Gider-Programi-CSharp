using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gelirgider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ev e1 = new ev();
            e1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            okul o1 = new okul();
            o1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ciftlik c1 = new ciftlik();
            c1.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
