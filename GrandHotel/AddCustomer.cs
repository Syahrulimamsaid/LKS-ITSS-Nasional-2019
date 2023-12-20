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
    public partial class AddCustomer : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int ID;
        
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {
            tampil();
            kosong();
            Lock();
            key();
            kunci();
        }
        public void tampil()
        {
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("select * from Customer", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Customer");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Customer";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void kosong()
        {
            txtname.Text = "";
            txtNik.Text = "";
            txtemail.Text = "";
            txtgender.Text = "";
            txtph.Text = "";
            txtage.Text = "";
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
            txtname.Enabled= false;
            txtNik.Enabled = false;
            txtemail.Enabled = false;
            txtgender.Enabled = false;
            txtph.Enabled = false;
            txtage.Enabled = false;
        }

        public void Open()
        {
            txtname.Enabled = true;
            txtNik.Enabled = true;
            txtemail.Enabled = true;
            txtgender.Enabled = true;
            txtph.Enabled = true;
            txtage.Enabled = true;
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
            nokey();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {

            if (txtname.Text.Trim() == "" || txtgender.Text.Trim() == "" || txtph.Text.Trim() == "")
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
                    cmd = new SqlCommand("select * from Customer where ID in(select max(ID) from Customer) order by ID desc", conn);
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
                        cmd = new SqlCommand("insert into Customer values ('" + urutan + "','" + txtname.Text + "','" + txtNik.Text + "','" + txtemail.Text + "','" + txtgender.Text + "','" +txtph.Text +"','"+ txtage.Text+ "')", conn);
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
                    cmd = new SqlCommand("update Customer set Name='" + txtname.Text + "', NIK='" + txtNik.Text + "', Email='" + txtemail.Text + "', Gender='" + txtgender.Text + "',PhoneNumber ='"+txtph.Text+"', Age='"+txtage.Text +"' where ID=@ID", conn);
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
                    cmd = new SqlCommand("delete Customer where ID=@ID", conn);
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
                txtname.Text = row.Cells[1].Value.ToString();
                txtNik.Text = row.Cells[2].Value.ToString();
                txtemail.Text = row.Cells[3].Value.ToString();
                txtgender.Text = row.Cells[4].Value.ToString();
                txtph.Text = row.Cells[5].Value.ToString();
                txtage.Text = row.Cells[6].Value.ToString();
                buka();
                Open();
                btnsave.Enabled = false;
            }
            catch (Exception d)
            {
                MessageBox.Show(d.ToString());
            }
        }

        private void btncencel_Click_1(object sender, EventArgs e)
        {
            kosong();
            Lock();
            kunci();
            key();
        }
    }
}
