using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin k = new Admin();
            k.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontOffice k = new FrontOffice();
            k.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            HouseKeeper p = new HouseKeeper();
            p.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label1.Text = Login.username;
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(label1.Text + " Yakin Keluar???", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login p = new Login();
                p.Show();
            }
            else
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyy " + " ss-mm-hh");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
