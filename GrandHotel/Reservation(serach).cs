using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class Reservation_serach_ : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int ID;
        string idrt;
        int IDRR;
        int IDReservation;
        string BerapaMalam;
        public Reservation_serach_()
        {
            InitializeComponent();
        }
        private void Reservation_serach__Load(object sender, EventArgs e)
        {
            koderoom();
            ReservationRoomID();
            btnAdd.Enabled = false;
            roomtypr();
            ff();
            label16.Text = Convert.ToString(IDRR);
        } 
    

        public void tampilrequestItem()
        {
            SqlConnection conn = konn.GetConn();
                {
                cmd = new SqlCommand("select * from ReservationRequestItem", conn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "ReservationRequestItem");
                dataGridView3.DataSource = ds;
                dataGridView3.DataMember = "ReservationRequestItem";
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CariCustomer l = new CariCustomer();
            l.Hide();
            AddCustomer c = new AddCustomer()
            {
                TopLevel = false,
                TopMost = true
            };
            c.FormBorderStyle = FormBorderStyle.None;
            panelAdd.Controls.Add(c);
            c.Show();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AddCustomer l = new AddCustomer();
            l.Hide();
            CariCustomer c = new CariCustomer()
            {
                TopLevel = false,
                TopMost = true
            };
            c.FormBorderStyle = FormBorderStyle.None;
            panelAdd.Controls.Add(c);
            c.Show();

        }
        public void roomtypr()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select ID, Name from RoomType", conn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                txtroomtype.DataSource = dt;
                txtroomtype.ValueMember = "ID";
                txtroomtype.DisplayMember = "Name";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            SqlConnection conn = konn.GetConn();
            {
                cmd = new SqlCommand("select * from Room where RoomTypeID like '%" + idrt + "%'", conn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item[3].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[4].ToString();
                }
            }
        }

        public void ff()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select ID, Name, RequestPrice from Item", conn);
            DataTable dt = new DataTable();
            {
                da.Fill(dt);
                txtitem.DataSource = dt;
                txtitem.ValueMember = "ID";
                txtitem.DisplayMember = "Name";
            }
        }
       

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            SqlConnection conn = konn.GetConn();

            {
                long hitung;
                string urutan;

                conn.Open();
                cmd = new SqlCommand("select * from ReservationRequestItem where ID in(select max(ID) from Item) order by ID desc", conn);
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["ID"].ToString().Length - 1, 1)) + 1;
                    String urutannya = "00000" + hitung;
                    urutan = "0" + urutannya.Substring(urutannya.Length - 1, 1);
                }
                else
                {
                    urutan = "1";
                }
                rd.Close();
                ID = Convert.ToInt32(urutan);
                {
                    cmd = new SqlCommand("insert into ReservationRequestItem values ('" + urutan + "','" + IDReservation + "','" + ItemID.Text + "','" + txtqty.Text + "','" + totalitem.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    tampilrequestItem(); 

                }
                conn.Close();
                btnAdd.Enabled = false;

            }
        }
      

        private void txtitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select * from Item where Name='" + txtitem.Text + "';", conn);
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string id = rd.GetInt32(0).ToString();
                    string price = rd.GetInt32(2).ToString();
                    txtrp.Text = price;
                    ItemID.Text = id;
                }

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void koderoom()
        {
            long hitung;
            string urutan;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from ReservationRoom where ID in(select max(ID) from ReservationRoom) order by ID desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["ID"].ToString().Length - 1, 1)) + 1;
                String Urutannya = "00000" + hitung;
                urutan = "0" + Urutannya.Substring(Urutannya.Length - 1,1 ) ;
            }
            else
            {
                urutan = "1";
            }
            rd.Close();
            conn.Close();
            IDRR = Convert.ToInt32(urutan);
            }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                {
                    cmd = new SqlCommand("insert into ReservationRoom values ('" + IDRR + "','" + ID  + "','" + datestart.Text + "','" + BerapaMalam + "','" + labelPrice.Text + datestart + "','" + "')", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil");
                }
            }
            catch (Exception d)
            {
                MessageBox.Show("Gagal");
                MessageBox.Show(d.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void ReservationRoomID()
        {
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                long hitung;
                string urutan;
                cmd = new SqlCommand("select * from ReservationRoom where ID in(select max(ID) from ReservationRoom) order by ID desc", conn);
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    hitung = Convert.ToInt32(rd[0].ToString().Substring(rd["ID"].ToString().Length - 1, 1)) + 1;
                    String urutannya = "00000" + hitung;
                    urutan = "0" + urutannya.Substring(urutannya.Length - 1, 1);
                }
                else
                {
                    urutan = "1";
                }
                rd.Close();
                conn.Close();
                IDReservation = Convert.ToInt32(urutan);
            }
        }

        private void txtroomid_TextChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            int total, harga, jumlah;
            harga = Convert.ToInt32(txtrp.Text);
            jumlah = Convert.ToInt16(txtqty.Text);
            total = harga * jumlah;
            totalitem.Text = Convert.ToString(total);
        }

        private void txtqty_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }

        private void btnkanan_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[1].Value = dataGridView1.Rows[n].Cells[1].Value;
                    dataGridView2.Rows[n].Cells[2].Value = dataGridView1.Rows[n].Cells[2].Value;
                    dataGridView2.Rows[n].Cells[3].Value = dataGridView1.Rows[n].Cells[3].Value;
                    dataGridView1.Rows.RemoveAt(n);
                }
            }  
        }

        private void txtroomtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select * from RoomType where Name='" + txtroomtype.Text + "';", conn);
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string p = rd[0].ToString();
                    idrt = p;
                };
            }
        catch (Exception k)
            {
                MessageBox.Show(k.Message);
            }
            finally
            {
                conn.Close();
            }    
        }
        private void btnkiri_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView2.RowCount - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridView2.Rows[i];
                if (Convert.ToBoolean(row.Cells["Select1"].Value))
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[1].Value = dataGridView2.Rows[n].Cells[1].Value;
                    dataGridView1.Rows[n].Cells[2].Value = dataGridView2.Rows[n].Cells[2].Value;
                    dataGridView1.Rows[n].Cells[3].Value = dataGridView2.Rows[n].Cells[3].Value;
                    dataGridView2.Rows.RemoveAt(n);
                }
            }
        }


        private void FinishDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime awal = Convert.ToDateTime(datestart.Text);
            DateTime akhir = Convert.ToDateTime(FinishDate.Text);
            TimeSpan ts = new TimeSpan();
            ts = akhir.Subtract(awal);
            BerapaMalam = ts.Days + "";
            label13.Text = ts.Days + " Malam";
        }

     
    }
}
