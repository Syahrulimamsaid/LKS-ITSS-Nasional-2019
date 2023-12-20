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
    public partial class FrontOffice : Form
    {
        public FrontOffice()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reservation_serach_ f = new Reservation_serach_();
            f.Hide();
            MasterRoomType o = new MasterRoomType();
            o.Hide();
            MasterRoom r = new MasterRoom();
            r.Hide();
            CheckOut ca = new CheckOut();
            ca.Hide();
            RequestAdditionalItemS Y = new RequestAdditionalItemS();
            Y.Hide();
            MasterItem c = new MasterItem()
            {
                TopLevel = false,
                TopMost = true
            };
           c.FormBorderStyle= FormBorderStyle.None;
            panel3.Controls.Add(c);
            c.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MasterRoomType o = new MasterRoomType();
            o.Hide();
            MasterItem c = new MasterItem();
            c.Hide();
            CheckOut ca = new CheckOut();
            ca.Hide();
            RequestAdditionalItemS Y = new RequestAdditionalItemS();
            Y.Hide();
            MasterRoom r = new MasterRoom()
            {
                TopLevel = false, TopMost = true
            };
        r.FormBorderStyle= FormBorderStyle.None;
            panel3.Controls.Add(r);
            r.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Reservation_serach_ f = new Reservation_serach_();
            f.Hide();
            MasterRoom r = new MasterRoom();
            r.Hide();
            MasterItem c = new MasterItem();
            c.Hide();
            CheckOut ca = new CheckOut();
            ca.Hide();
            MasterRoomType o = new MasterRoomType()
            {
                TopLevel = false, TopMost= true
            };
            o.FormBorderStyle= FormBorderStyle.None;    
            panel3.Controls.Add(o);
                o.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MasterRoomType o = new MasterRoomType();
            o.Hide();
            MasterRoom r = new MasterRoom();
            r.Hide();
            RequestAdditionalItemS Y = new RequestAdditionalItemS();
            Y.Hide();
            MasterItem c = new MasterItem();
            c.Hide();
            CheckOut ca = new CheckOut();
            ca.Hide();
            Reservation_serach_ f = new Reservation_serach_()
            {
                TopLevel = false, TopMost = true
            };
            f.FormBorderStyle   = FormBorderStyle.None;
            panel3.Controls.Add(f);
            f.Show();
        }

        private void FrontOffice_Load(object sender, EventArgs e)
        {
            label1.Text = Login.username;
            timer1.Start();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Reservation_serach_ f = new Reservation_serach_();
            f.Hide();
            MasterRoomType o = new MasterRoomType();
            o.Hide();
            RequestAdditionalItemS Y = new RequestAdditionalItemS();
            Y.Hide();
            MasterRoom r = new MasterRoom();
            r.Hide();
            MasterItem c = new MasterItem();
            c.Hide();
            CheckOut ca = new CheckOut()
            {
                TopLevel = false,
                TopMost = true
            };
            ca.FormBorderStyle = FormBorderStyle.None;
            panel3.Controls.Add(ca);
            ca.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MasterRoomType o = new MasterRoomType();
            o.Hide();
            MasterRoom r = new MasterRoom();
            r.Hide();
            MasterItem c = new MasterItem();
            c.Hide();
            Reservation_serach_ f = new Reservation_serach_();
            f.Hide();
            CheckOut ca = new CheckOut();
            ca.Hide();
            RequestAdditionalItemS l = new RequestAdditionalItemS()
            {
                TopLevel = false, TopMost = true
            };
            l.FormBorderStyle   = FormBorderStyle.None;
            panel3.Controls.Add(l);
            l.Show();
        }
    }
}
