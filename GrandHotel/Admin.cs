using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu p = new Menu();
            p.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            label1.Text = Login.username;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasterEmployee p = new MasterEmployee() 
            {TopLevel = false, TopMost = true };
            p.FormBorderStyle = FormBorderStyle.None;
            panel4.Controls.Add(p);
            p.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyy " + " ss-mm-hh");
        }
    }
}
