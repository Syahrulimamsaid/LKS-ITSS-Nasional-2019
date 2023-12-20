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
    public partial class HouseKeeper : Form
    {
        public HouseKeeper()
        {
            InitializeComponent();
        }

        private void HouseKeeper_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = Login.username;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu p = new Menu();
            p.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyy " + " ss-mm-hh");
        }
    }
}
