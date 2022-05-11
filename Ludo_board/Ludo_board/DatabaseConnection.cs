using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_board
{
    public class DatabaseConnection
    {
        string ConnString { get; set; }
        public SqlConnection Conn { get; set; }

        public DatabaseConnection()
        {
            ConnString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}LudoDataBase.mdf;Integrated Security=True";
            Conn = new SqlConnection(ConnString);
            try
            {
                Conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
        }

    }
}
