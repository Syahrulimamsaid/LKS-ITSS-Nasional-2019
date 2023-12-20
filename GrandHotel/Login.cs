using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel
{
   
    public partial class Login : Form
    {
        public static string username;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;
        private SqlDataReader rd;
        Koneksi konn = new Koneksi();
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
           
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("select * from Employee where Username='" + textBox1.Text + "'and Password='" + textBox2.Text + "'", conn);
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    Menu call = new  Menu();
                    call.Show();
                    this.Hide();
                    MessageBox.Show("Selamat Datang : " + textBox1.Text + " !!!");
                    conn.Close();
                }
                else if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("Silahkan Isi Username dan Password");
                }
                else
                {
                    MessageBox.Show("Username/Password Salah!!!");
                }
            } 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) 
            {
                textBox2.UseSystemPasswordChar = false; 
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
         public void kunci()
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kunci();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
           
            if (MessageBox.Show("Apakah Yakin Keluar??", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question)  == DialogResult.Yes)
            {
                MessageBox.Show("~~~~~~~~~~~~~~ SAMPAI JUMPA ~~~~~~~~~~~~~~~");
                Application.Exit();
            }
            else
            {
                
            }
        }
        
        
    }
}
