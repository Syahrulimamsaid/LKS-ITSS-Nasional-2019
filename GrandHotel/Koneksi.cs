using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GrandHotel
{
    class Koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source= LAPTOP-DURV5T29\SQLSAMS;Initial Catalog=PC_01;Integrated Security=True";
            return conn;
        }
    }
}