using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class MasterRoomType : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int ID;
        public MasterRoomType()
        {
            InitializeComponent();
        }

        private void MasterRoom_Load(object sender, EventArgs e)
        {
            Lock();
            kunci();
            key();
            tampil();
        }

        public void tampil()
        {
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("select * from RoomType", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "RoomType");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "RoomType";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void kosong()
        {
            txtName.Text = "";
            txtCapacity.Text = "";
            TxtPrice.Text = "";
        }

        public void kunci()
        {
            btnUpdate.Enabled = false;
            btndelete.Enabled = false;
            btnInsert.Enabled = true;
        }
        public void buka()
        {
            btnUpdate.Enabled = true;
            btndelete.Enabled = true;
            btnInsert.Enabled = false;
        }

        private void btncencel_Click(object sender, EventArgs e)
        {
            kosong();
            kunci();
            key();
            Lock();

        }
        public void Lock()
        {
            txtName.Enabled = false;
            txtCapacity.Enabled = false;
            TxtPrice.Enabled = false;
        }

        public void Open()
        {
            txtName.Enabled = true;
            txtCapacity.Enabled = true;
            TxtPrice.Enabled = true;
        }

        public void key()
        {
            btnsave.Enabled = false;
            btnInsert.Enabled = true;

        }
        public void nokey()
        {
            btnsave.Enabled = true;
            btnInsert.Enabled = false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                txtName.Text = row.Cells[1].Value.ToString();
                txtCapacity.Text = row.Cells[2].Value.ToString();
                TxtPrice.Text = row.Cells[3].Value.ToString();
                buka();
                Open();
                btnsave.Enabled = false;

            }
            catch (Exception d)
            {
                MessageBox.Show(d.ToString());
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            Open();
            nokey();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            
            btnInsert.Enabled = false;
            if (txtName.Text.Trim() == "" || txtCapacity.Text.Trim() == "" || TxtPrice.Text.Trim() == "")
            {
                MessageBox.Show("Silahkan Isi Data!!!");
            }
            else
            {
                SqlConnection conn = konn.GetConn();
                try
                {
                    long hitung;
                    string urutan;

                    conn.Open();
                    cmd = new SqlCommand("select * from RoomType where ID in(select max(ID) from RoomType) order by ID desc", conn);
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
                    {
                        cmd = new SqlCommand("insert into RoomType values ('" + urutan + "','" + txtName.Text + "','" + txtCapacity.Text + "','" + TxtPrice.Text + "')", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dimasukkan!!!");
                        tampil();
                        kosong();
                        Lock();
                        key();
                    }
                }
                catch (Exception d)
                {
                    MessageBox.Show("Data Gagal Dimasukkan!!!");
                    MessageBox.Show(d.ToString());

                }
                finally
                {
                    conn.Close();
                }
            }
        }





        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("update RoomType set Name='" + txtName.Text + "', Capacity='" + txtCapacity.Text + "', RoomPrice='" + TxtPrice.Text + "' where ID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diubah!!!");
                tampil();
                kosong();
                Lock();
                key();
                kunci();
                btnsave.Enabled = false;

            }
            catch (Exception d)
            {
                MessageBox.Show("Data Gagal Diubah!!!");
                MessageBox.Show(d.ToString());

            }
            finally
            {
                conn.Close();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("delete RoomType where ID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Dihapus!!!");
                tampil();
                kosong();
                Lock();
                kunci();
                key();
                conn.Close();
            }
        }
    }
}



