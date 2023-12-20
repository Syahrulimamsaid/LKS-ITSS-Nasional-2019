using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
    public partial class CariCustomer : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader rd;
        private DataSet ds;
        Koneksi konn = new Koneksi();
       
        public CariCustomer()
        {
            InitializeComponent();
        }
        public void tampilcustomer()
        {
            SqlConnection conn = konn.GetConn();
            {
                cmd = new SqlCommand("select * from Customer", conn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "Customer");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Customer";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        public static string pillihcustomer;
        string ID;

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tampilcustomer();
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select * from Customer where ID like '%" + txtsearch.Text + "%' or Name like '%" + txtsearch.Text + "%' or Email like '%" + txtsearch.Text + "%' or Gender like '%" + txtsearch.Text + "%' or PhoneNumber like '%" + txtsearch.Text + "%' or Age like '%" + txtsearch.Text + "%'", conn);

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "Customer");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Customer";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void CariCustomer_Load(object sender, EventArgs e)
        {
            tampilcustomer();
            t();
           
        }
        public void t()
        {
            DataGridViewCheckBoxColumn cc = new DataGridViewCheckBoxColumn();
            cc.HeaderText = "Chose";
            cc.DataPropertyName = "btnCheck";
            cc.Name = "Check";
            dataGridView1.Columns.Add(cc); 
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
               // textBox1.Text = row.Cells[0].Value.ToString();
                ID = Convert.ToString(row.Cells[1].Value.ToString());
                pillihcustomer = ID.ToString();
            }
            catch (Exception F)
            {
                MessageBox.Show(F.ToString());
            }
        }
    }
}
