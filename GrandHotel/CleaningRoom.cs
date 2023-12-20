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
    public partial class CleaningRoom : Form
    {
        public static string datagridview1;
        Koneksi konn = new Koneksi();
        private SqlDataAdapter da;
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataReader rd;
        int ID;
        int idRoom; string datestart; string dateend; string note; string statuscleaning; string RoomID;
        string IDCR;
        public CleaningRoom()
        {
            InitializeComponent();
        }

        private void CleaningRoom_Load(object sender, EventArgs e)
        {
            tampilRoom();
            hh();
        }

        public void hh()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from CleaningRoom",conn);
            {
                rd = cmd.ExecuteReader();
                rd.Read();
                if(rd.HasRows)
                {
                    label4.Text = rd.GetInt32(0).ToString();
                    IDCR = rd.GetInt32(0).ToString();
                }
            }
            conn.Close();
        }
        public void tampilRoom()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd =  new SqlCommand("select * from ReservationCheckOut inner join ReservationRoom on ReservationCheckOut.ReservationRoomID=ReservationRoom.ID inner join Room on ReservationRoom.RoomID=Room.ID ",conn);
            {
                rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        dataGridViewp.Rows[dataGridViewp.Rows.Add()].Cells[0].Value = rd["RoomNumber"].ToString();
                    }
                    rd.Close();
                }
                conn.Close();
            }
        }
        private void dataGridViewp_Leave(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            try
            {
                for (int i = 0; i < dataGridViewp.Rows.Count; i++)
                {
                    idRoom = Convert.ToInt32(dataGridViewp.Rows[i].Cells[0].Value.ToString());
                    datestart = dataGridViewp.Rows[i].Cells[1].Value.ToString();
                    dateend = dataGridViewp.Rows[i].Cells[2].Value.ToString();
                    note = dataGridViewp.Rows[i].Cells[3].Value.ToString();
                    statuscleaning = dataGridViewp.Rows[i].Cells[4].Value.ToString();
                }
                cmd = new SqlCommand("select * from Room where RoomNumber='" + idRoom + "';", conn);
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RoomID = rd.GetInt32(0).ToString();
                    }
                    rd.Close();
                }
                cmd = new SqlCommand("select * from CleaningRoom'");
                {
                    rd = cmd.ExecuteReader();
                    while (rd.Read()) 
                    {
                        label4.Text = rd.GetInt32(0).ToString();
                        IDCR = rd.GetInt32(0).ToString();
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
    public void kodeotomatis()
        {
            int urutan;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from CleaningRoomDetail where ID in(select max(ID) from CleaningRoomDetail) order by ID desc", conn);
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
    }
}
