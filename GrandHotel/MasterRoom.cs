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
using System.Xml.Linq;

namespace GrandHotel
{
    public partial class MasterRoom : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int ID;
        string ip;
        public MasterRoom()
        {
            InitializeComponent();
        }

        private void MasterRoom_Load(object sender, EventArgs e)
        {
            tampil();
            kosong();
            Lock();
            kunci();
            key();
        }

        public void tampil()
        {
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("select * from Room", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Room");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Room";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void kosong()
        {
            txtnumber.Text = "";
            txtnamatype.Text = "";
            Txtfloor.Text = "";
            txtdes.Text = "";
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
            Lock();
            key();

        }
        public void Lock()
        {
            txtnumber.Enabled = false;
            txtnamatype.Enabled = false;
            Txtfloor.Enabled = false;
            txtdes.Enabled = false;
        }

        public void Open()
        {
            txtnumber.Enabled = true;
            txtnamatype.Enabled = true;
            Txtfloor.Enabled = true;
            txtdes.Enabled = true;
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Open();
            nokey(); kk();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
           
            if (txtnumber.Text.Trim() == "" || txtnamatype.Text.Trim() == "" || Txtfloor.Text.Trim() == "" )
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
                    cmd = new SqlCommand("select * from Room where ID in(select max(ID) from Room) order by ID desc", conn);
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
                        cmd = new SqlCommand("insert into Room values ('" + urutan + "','" + ip + "','" + txtnumber.Text + "','" + Txtfloor.Text + "','" + txtdes.Text + "')", conn);
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
            if (MessageBox.Show("Yakin Akan Ubah : " + ID + " ??", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = konn.GetConn();
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("update Room set RoomTypeID='" + ip + "', RoomNumber='" + txtnumber.Text + "', RoomFloor='" + Txtfloor.Text + "', Description='" + txtdes.Text + "' where ID=@ID", conn);
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
        }
    

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin Akan Hapus : " + ID + " ??", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = konn.GetConn();
                {
                    conn.Open();
                    cmd = new SqlCommand("delete Room where ID=@ID", conn);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                txtnamatype.Text = row.Cells[1].Value.ToString();
                txtnumber.Text = row.Cells[2].Value.ToString();
                Txtfloor.Text = row.Cells[3].Value.ToString();
                txtdes.Text = row.Cells[4].Value.ToString();
                buka();
                Open();
                btnsave.Enabled = false;

            }
            catch (Exception d)
            {
                MessageBox.Show(d.ToString());
            }
        }
    
    public void kk()
        {
            SqlConnection conn = konn.GetConn();
            {
               
                da = new SqlDataAdapter("select ID, Name from RoomType", conn);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    txtnamatype.DataSource = dt;
                    txtnamatype.ValueMember = "ID";
                    txtnamatype.DisplayMember = "Name";
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.ToString());
                }  
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select * from RoomType where Name='" + txtnamatype.Text + "';" , conn);
            try
            {
                    conn.Open();
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ip = rd.GetInt32(0).ToString();
                    };
            }
            catch (Exception G)
                {
                    MessageBox.Show(G.Message);
                }
            finally
                {
                    conn.Close();
                }    
            
        }

        private void btncencel_Click_1(object sender, EventArgs e)
        {
            kosong();
            kunci();
            Lock();
            key();
        }
    }
}
