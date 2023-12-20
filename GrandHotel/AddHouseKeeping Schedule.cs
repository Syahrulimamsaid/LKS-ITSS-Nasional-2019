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
    public partial class AddHouseKeeping_Schedule : Form
    {   Koneksi konn = new Koneksi();
        private SqlDataAdapter da;
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataReader rd;
        string IDEMPLY;
        int ID;
        string IDCLRM;

        public AddHouseKeeping_Schedule()
        {
            InitializeComponent();
        }

        private void AddHouseKeeping_Schedule_Load(object sender, EventArgs e)
        {
            date.CustomFormat = "yyyy-MM-dd";
            housekeeper();
            Room();
            // tampil();
        }
        public void tampil()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from CleaningRoom",conn);
            ds = new DataSet();
            da.Fill(ds, "CleaningRoom");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "CleaningRoom";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void housekeeper()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from Employee inner join Job on Employee.JobID=Job.ID where JobID=4",conn);
            DataTable dt = new DataTable();
            {
                da.Fill(dt);
                Housekeeper.DataSource = dt;
                Housekeeper.ValueMember = "ID";
                Housekeeper.DisplayMember = "Name";
            }
        }
        public void Room()
        {
            SqlConnection conn = konn.GetConn();
            da = new SqlDataAdapter("select * from ReservationCheckOut inner join ReservationRoom on ReservationCheckOut.ReservationRoomID=ReservationRoom.ID inner join Room on RoomID=Room.ID",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            RoomNumber.DataSource = dt;
            RoomNumber.ValueMember = "ID";
            RoomNumber.DisplayMember = "RoomNumber";
        }

        private void btnADd_Click(object sender, EventArgs e)
        {   kodeotomatis();
            SqlConnection conn = konn.GetConn();
            conn.Open();
            try
            {
                cmd = new SqlCommand("insert into CleaningRoom values ('" + ID + "','" + date.Text + "','" + IDEMPLY + "')",conn);
                cmd.ExecuteNonQuery();
                dataGridView1.Rows.Add(date.Text, Housekeeper.Text, RoomNumber.Text);
                MessageBox.Show("Yes");
                // tampil();
            }
        catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
            finally
            {
                conn.Close();
            }  
        }

        private void Housekeeper_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from Employee where Name='" + Housekeeper.Text + "';",conn);
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                IDEMPLY = rd.GetInt32(0).ToString();
            }
            conn.Close();
        }
        public void kodeotomatis()
        {
            int urutan;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from CleaningRoom where ID in(select max(ID) from CleaningRoom) order by ID desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                urutan = Convert.ToInt32(rd[0].ToString()) + 1;
            }
            else
            {
                urutan = Convert.ToInt32("1");
            }
            rd.Close();
            ID = urutan;
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Column4"].Index)
                {
                    if (dataGridView1.Rows.Count > 1)
                    {
                        string nama;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            nama = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            cmd = new SqlCommand("select Name, Employee.ID as ed, CleaningRoom.ID as clrd from Employee inner join CleaningRoom on Employee.ID=CleaningRoom.Employee.ID where Name='" + nama + "';", conn);
                            {
                                rd = cmd.ExecuteReader();
                                while (rd.Read())
                                {
                                    label2.Text = rd["clrd"].ToString();
                                    IDCLRM = rd["clrd"].ToString();
                                }
                                rd.Close();
                            }
                        }
                        cmd = new SqlCommand("delete CleaningRoom where ID='" + IDCLRM + "'", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil");
                    }
                }
            }
           catch (Exception f)
            {
                MessageBox.Show("Gagal");
                MessageBox.Show(f.ToString());
            }
            finally
            {
                conn.Close();
            }  
        }
    }
}
