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
    public partial class RequestAdditionalItemS : Form
    {
        Koneksi konn = new Koneksi();
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private SqlCommand cmd;
        private DataSet ds;
        string total;
        string items;
        int qty = 0;
        int price = 0;
        int idk;
        int harga;
        int KODE;
        int idreser;
        string roomnumber;
        string iditem;
        public RequestAdditionalItemS()
        {
            InitializeComponent();
        }

        private void RequestAdditionalItemS_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            room();
            kode();
            item();
        }
        public void room()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from ReservationRoom INNER JOIN Room ON ReservationRoom.RoomID=Room.ID ", conn);
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
            dataGridView1.Rows.Clear();
            SqlConnection conn = konn.GetConn();
            {
                try
                {
                    SqlCommand cmd1 = new SqlCommand("select * from Room where RoomNumber='" + txtrommnumber.Text + "';", conn);
                    conn.Open();
                    {
                        rd = cmd1.ExecuteReader();
                        while (rd.Read())
                        {
                            string Room = rd.GetInt32(0).ToString();
                            roomnumber = Room;
                        }
                        rd.Close();
                    }
                    cmd = new SqlCommand("select * from ReservationRoom where RoomID='" + roomnumber + "';",conn);
                    {
                        rd  = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            string p = rd.GetInt32(0).ToString();
                            idreser = Convert.ToInt32(p);
                        }
                        rd.Close();
                    }
                    cmd = new SqlCommand("SELECT ReservationRequestItem.ID as rrID, ReservationRoom.ID, ReservationRequestItem.ReservationRoomID, ItemID, Item.ID, ReservationRoomID, Name, Qty, TotalPrice " + "FROM ReservationRoom " + "INNER JOIN ReservationRequestItem ON ReservationRoom.ID = ReservationRequestItem.ReservationRoomID " + "INNER JOIN Item ON ItemID=Item.ID " + "WHERE ReservationRoomID LIKE '%" + roomnumber + "%'", conn);
                    {
                        rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                int n = dataGridView1.Rows.Add();
                                dataGridView1.Rows[n].Cells[0].Value = rd["rrID"].ToString();
                                dataGridView1.Rows[n].Cells[1].Value = rd["Name"].ToString();
                                dataGridView1.Rows[n].Cells[2].Value = rd["Qty"].ToString();
                                dataGridView1.Rows[n].Cells[3].Value = rd["TotalPrice"].ToString();
                            }
                            rd.Close();
                        }
                    }
                    label6.Text = "0";
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        label6.Text = Convert.ToString(int.Parse(label6.Text) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
                    }
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void item()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from Item", conn);
            DataTable dt = new DataTable();
            {
                da.Fill(dt);
                txtnamatype.DataSource = dt;
                txtnamatype.ValueMember = "ID";
                txtnamatype.DisplayMember = "Name";
            }
        }

        private void txtnamatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select * from Item where Name='" + txtnamatype.Text + "';", conn);
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string d = rd.GetInt32(0).ToString();
                    string p = rd.GetInt32(2).ToString();
                    iditem = d;
                    total = p;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            kode();
            SqlConnection conn = konn.GetConn();
                conn.Open();
                harga = Convert.ToInt32(TxtQty.Text) * Convert.ToInt32(total);
                cmd = new SqlCommand("insert into ReservationRequestItem values ('" + idk + "','" + idreser + "','" + iditem + "','" + TxtQty.Text + "','" + harga + "')", conn);
                cmd.ExecuteNonQuery();
                dataGridView1.Rows.Add(idk, txtnamatype.Text, TxtQty.Text, harga);
                btnAdd.Enabled = false;
                TxtQty.Text = "";
                label6.Text = "0";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                label6.Text = Convert.ToString(int.Parse(label6.Text) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
            }
            conn.Close();
        }           

        private void TxtQty_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            cmd = new SqlCommand("delete ReservationRequestItem where ID=@ID", conn);
            cmd.Parameters.AddWithValue("ID", id);
            cmd.ExecuteNonQuery();
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            label6.Text = "0";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                label6.Text = Convert.ToString(int.Parse(label6.Text) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
            }
            conn.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            label6.Text = "0";
        }

        public void kode()
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
                hitung = Convert.ToInt32(rd[0].ToString())+ 1;
                String urutannya = "00000" + hitung;
                urutan = urutannya;
            }
            else
            {
                urutan = "00001";
            }
            idk = Convert.ToInt32(urutan);
            rd.Close();
            conn.Close();
            
        }

    }
}