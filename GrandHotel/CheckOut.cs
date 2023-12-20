using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class CheckOut : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int harga;
        int idroom;
        int IDiteml;
        int IDITEM;
        int IDROOM;
        int IDRR;
        int IDNMITM;
        int totalharga;
        int iditem;
        int IDITMSTATUS;
        int ID;
        public CheckOut()
        {
            InitializeComponent();
        }

        private void CheckOut_Load(object sender, EventArgs e)
        {
            label6.Text = "0";
            room();
            statusitem();
        }
 
    public void room()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from ReservationRoom inner join Room on ReservationRoom.RoomID=Room.ID", conn);
            DataTable dt = new DataTable();
            {
                da.Fill(dt);
                txtrommnumber.DataSource = dt;
                txtrommnumber.ValueMember = "ID";
                txtrommnumber.DisplayMember = Convert.ToString("RoomNumber");
            }
        }

        private void txtrommnumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            try
            {
                cmd = new SqlCommand("select * from Room where RoomNumber='" + txtrommnumber.Text + "';", conn);
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        IDROOM = Convert.ToInt32(rd.GetInt32(0).ToString());
                      
                    }
                    rd.Close();
                }
                cmd = new SqlCommand("select * from ReservationRoom where RoomID='" + IDROOM + "';",conn);
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        IDRR = Convert.ToInt32(rd.GetInt32(0).ToString());
                       
                    }
                    rd.Close();
                }
                cmd = new SqlCommand("select * from ReservationRequestItem inner join Item on ReservationRequestItem.ItemID=Item.ID where ReservationRoomID='" + IDRR + "';", conn);
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read()) 
                    {
                      
                    }
                    rd.Close();
                }
                da = new SqlDataAdapter("select * from ReservationRequestItem inner join Item on ReservationRequestItem.ItemID=Item.ID where ReservationRoomID='" + IDRR + "'", conn);
                DataTable dt = new DataTable();
                {
                    da.Fill(dt);
                    txtnamatype.DataSource = dt;
                    txtnamatype.ValueMember = "ID";
                    txtnamatype.DisplayMember = "Name";
                }
                
                //cmd = new SqlCommand("select * from ReservationRequestItem INNER JOIN Item ON ReservationRequestItem.ItemID=Item.ID where ReservationRoomID like '%" + id + "%'", conn);
                //{
                //    rd = cmd.ExecuteReader();
                //    if (rd.HasRows)
                //    {
                //        while (rd.Read())
                //        {
                //            int n = dataGridView1.Rows.Add();
                //            dataGridView1.Rows[n].Cells[0].Value = rd["ID"].ToString();
                //            dataGridView1.Rows[n].Cells[1].Value = rd["Name"].ToString();
                //            dataGridView1.Rows[n].Cells[2].Value = rd["QTY"].ToString();
                //            dataGridView1.Rows[n].Cells[3].Value = rd["TotalPrice"].ToString();
                //        }
                //    }
                //    rd.Close();
                //}
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

        private void txtnamatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            try
            {
                cmd = new SqlCommand("select * from Item where Name='" + txtnamatype.Text + "';", conn);
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        harga = Convert.ToInt32(rd.GetInt32(2).ToString());
                        IDNMITM = Convert.ToInt32(rd[0].ToString());
                    }
                    rd.Close();
                }
                cmd = new SqlCommand("select * from ReservationRequestItem where ItemID='" + IDNMITM + "';",conn);
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        txtqty.Text = Convert.ToString(rd[3].ToString());
                    }
                    rd.Close();
                }
            }
            catch (Exception d)
            {
                MessageBox.Show(d.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void statusitem()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from ItemStatus", conn);
            DataTable dt = new DataTable();
            {
                da.Fill(dt);
                txtitemstatus.DataSource = dt;
                txtitemstatus.ValueMember = "ID";
                txtitemstatus.DisplayMember = "Name";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            kodeotomatisrrr();
            SqlConnection conn = konn.GetConn();
            conn.Open();
            try
            {
                totalharga = Convert.ToInt32(txtqty.Text) * harga;
                dataGridView1.Rows.Add(IDROOM, txtnamatype.Text, txtqty.Text, totalharga);
                label6.Text = "0";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    label6.Text = Convert.ToString(int.Parse(label6.Text) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
                }
                cmd = new SqlCommand("insert into ReservationCheckOut values ('" + ID + "','" + IDROOM + "','" + IDNMITM + "','" + IDITMSTATUS + "','" + txtqty.Text + "','" + totalharga + "')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil");
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
        public void kodeotomatisrrr()
        {
            long hitung;
            string urutan;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from ReservationRequestItem where ID in(select max(ID) from ReservationRequestItem) order by ID desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt32(rd[0].ToString()) + 1;
                urutan = "00000" + hitung;
            }
        else
            {
                urutan = "00001";
            }
            rd.Close();
            conn.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            label6.Text = "0";
            dataGridView1.Rows.Clear();
        }
        public void kodechechout()
        {
            long hitung;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from ReservationCheckOut where ID in(select max(ID) from ReservationCheckOut) order by ID desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt32(rd[0].ToString()) + 1;
            }
            else
            {
                hitung = Convert.ToInt32("1");
            }
            rd.Close();
            ID = Convert.ToInt32(hitung);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Column4"].Index)
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    SqlConnection conn = konn.GetConn();
                    conn.Open();
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    cmd = new SqlCommand("delete from ReservationCheckOut where ID=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    label6.Text = "0";
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        label6.Text = Convert.ToString(int.Parse(label6.Text) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
                    }
                    conn.Close();
                }
            }
        }

        private void txtitemstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from ItemStatus where Name='" + txtitemstatus.Text + "';", conn);
            {
                rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    IDITMSTATUS = Convert.ToInt32(rd.GetInt32(0).ToString());
                }
                rd.Close();
            }
            conn.Close();
        }
    }
}

