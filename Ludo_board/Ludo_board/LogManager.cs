using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_board
{
    public class LogManager
    {
        public DatabaseConnection? Connection;

        public LogManager(DatabaseConnection connection)
        {
            Connection = connection;
        }

        public void InsertIntoMovementLog(string Text)
        {
            try
            {
                string query2 = $"INSERT INTO ActionLog (PawnMovements) VALUES ('{Text}')";
                SqlCommand cmd = new SqlCommand(query2, Connection?.Conn);
                var number = cmd.ExecuteNonQuery();
                    Console.WriteLine("Rows affected : " + number);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        public List<string> GetAllRecords()
        {
            string query1 = "SELECT * FROM ActionLog";
            List<string> PawnMovements = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand(query1, Connection?.Conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PawnMovements.Add(reader["PawnMovements"].ToString());
                    }
                    Console.WriteLine("GetAllRecords command executed");
                    reader.Close();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error: " + e.Message);
            }
            return PawnMovements;
        }

        public void CleanLog()
        {
            try
            {
                string query1 = "DELETE FROM ActionLog";
                SqlCommand cmd = new SqlCommand(query1, Connection?.Conn);
                var number = cmd.ExecuteNonQuery();
                Console.WriteLine("Rows affected : " + number);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
