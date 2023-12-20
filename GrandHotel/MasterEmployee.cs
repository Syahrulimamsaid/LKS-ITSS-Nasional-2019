using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class MasterEmployee : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
        int ID;
        string IDJOB;
        public MasterEmployee()
        {
            InitializeComponent();
        }

        public void tampil()
        {
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("select * from Employee", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Employee");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Employee";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void kosong()
        {
            txtuser.Text = "";
            txtpas.Text = "";
            Txtconfirm.Text = "";
            txtname.Text = "";
            txtemail.Text = "";
            date.Text = "";
            txtJob.Text = "";
            txtaddress.Text = "";
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
            txtuser.Enabled = false;
            txtpas.Enabled = false;
            Txtconfirm.Enabled = false;
            txtname.Enabled = false;
            txtemail.Enabled = false;
            date.Enabled = false;
            txtJob.Enabled = false;
            txtaddress.Enabled = false;
        }

        public void Open()
        {
            txtuser.Enabled = true;
            txtpas.Enabled = true;
            Txtconfirm.Enabled = true;
            txtname.Enabled = true;
            txtemail.Enabled = true;
            date.Enabled = true;
            txtJob.Enabled = true;
            txtaddress.Enabled = true;
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
            nokey();
            Open();
            h();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtuser.Text.Trim() == "" ||
            txtpas.Text.Trim() == "" ||
            Txtconfirm.Text.Trim() == "" ||
            txtname.Text.Trim() == "" ||
            txtemail.Text.Trim() == "" ||
            date.Text.Trim() == "" ||
            txtJob.Text.Trim() == "" ||
            txtaddress.Text.Trim() == "")
            {
                MessageBox.Show("Silahkan Isi Data!!!");
            }
            else if (txtpas.Text != Txtconfirm.Text)
            {
                MessageBox.Show("Password Tidak Sesuai !!!");
            }
            else
            {
                SqlConnection conn = konn.GetConn();
                try
                {
                    long hitung;
                    string urutan;

                    conn.Open();
                    cmd = new SqlCommand("select * from Employee where ID in(select max(ID) from Employee) order by ID desc", conn);
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
                        cmd = new SqlCommand("insert into Employee values ('" + urutan + "','" + txtuser.Text + "','" + txtpas.Text + "','" + txtname.Text + "','" + txtemail.Text + "','" + txtaddress.Text + "','" + date.Text + "','" + IDJOB + "')", conn);
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
                    cmd = new SqlCommand("update Employee set Username='" + txtuser.Text + "', Password='" + txtpas.Text + "', Name='" + txtname.Text + "', Email='" + txtemail.Text + "', Address='" + txtaddress + "',DateOfBirth='" + date.Text + "',JobID='" + txtJob.Text + "' where ID=@ID", conn);
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
                    cmd = new SqlCommand("delete Employee where ID=@ID", conn);
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
                txtuser.Text = row.Cells[1].Value.ToString();
                txtpas.Text = row.Cells[2].Value.ToString();
                txtname.Text = row.Cells[3].Value.ToString();
                txtemail.Text = row.Cells[4].Value.ToString();
                date.Text = row.Cells[6].Value.ToString();
                txtJob.Text = row.Cells[7].Value.ToString();
                txtaddress.Text = row.Cells[5].Value.ToString();
                buka();
                key();Open();
                btnInsert.Enabled = false;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.ToString());
            }
        }

        private void btncencel_Click_1(object sender, EventArgs e)
        {
            kosong();
            key();
            Lock();
            kunci();
        }
    public void h()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select ID, Name from Job", conn);
            DataTable dt = new DataTable();
            {
                da.Fill(dt);
                txtJob.DataSource = dt;
                txtJob.ValueMember= "ID";
                txtJob.DisplayMember= "Name";
            }
        }

        private void MasterEmployee_Load_1(object sender, EventArgs e)
        {
            date.CustomFormat = "yyyy-MM-dd";
            tampil();
            kosong();
            Lock();
            kunci();
            key();
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Txtconfirm_Leave(object sender, EventArgs e)
        {
            if (txtpas.Text == Txtconfirm.Text)
            {
                Txtconfirm.ForeColor = Color.Blue;
                errorProvider1.Clear();
            }
            else
            {
                Txtconfirm.ForeColor = Color.Red;
                errorProvider1.SetError(this.Txtconfirm, "Password Tidak Sesuai !!!");
            }
        }

        private void txtJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from Job where Name='" + txtJob.Text + "';",conn);
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                IDJOB = rd.GetInt32(0).ToString();
            }
            conn.Close();
        }
    }
}