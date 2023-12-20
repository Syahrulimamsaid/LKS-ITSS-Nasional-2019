using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class MasterItem : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int ID;
        public MasterItem()
        {
            InitializeComponent();
        }

        private void MasterItem_Load(object sender, EventArgs e)
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
                cmd = new SqlCommand("select * from Item", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Item");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Item";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void kosong()
        {
            txtname.Text = "";
            txtrp.Text = "";
            Txtcp.Text = "";

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
            txtname.Enabled = false;
            txtrp.Enabled = false;
            Txtcp.Enabled = false;
        }

        public void Open()
        {
            txtname.Enabled = true;
            txtrp.Enabled = true;
            Txtcp.Enabled = true;
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

        

        private void btnsave_Click(object sender, EventArgs e)
        {

            if (txtname.Text.Trim() == "" || txtrp.Text.Trim() == "" || Txtcp.Text.Trim() == "")
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
                    cmd = new SqlCommand("select * from Item where ID in(select max(ID) from Item) order by ID desc", conn);
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
                        cmd = new SqlCommand("insert into Item values ('" + urutan + "','" + txtname.Text + "','" + txtrp.Text + "','" + Txtcp.Text + "')", conn);
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
                    cmd = new SqlCommand("update Item set Name='" + txtname.Text + "', RequestPrice='" + txtrp.Text + "', CompensationFee='" + Txtcp.Text + "' where ID=@ID", conn);
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
                    cmd = new SqlCommand("delete Item where ID=@ID", conn);
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

        private void btncencel_Click_1(object sender, EventArgs e)
        {
            kosong();
            kunci();
            Lock();
            key();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            ID = Convert.ToInt32(row.Cells[0].Value.ToString());
            txtname.Text = row.Cells[1].Value.ToString();
            txtrp.Text = row.Cells[2].Value.ToString();
            Txtcp.Text = row.Cells[3].Value.ToString();
            buka();
            nokey();
            Open();
        }

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
                Open();
                nokey();
 
        }
    }
}
