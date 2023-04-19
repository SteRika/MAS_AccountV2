using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using System.Windows.Forms;

namespace MAS_Account.npgsql
{
    class Npgsql_Helper
    {
        //static void Mains(string[] args)
        //{
        //    // Connect to a PostgreSQL database
        //    NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres; " +
        //       "Password=pwd;Database=postgres;");
        //    conn.Open();

        //    // Define a query returning a single row result set
        //    NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM cities", conn);

        //    // Execute the query and obtain the value of the first column of the first row
        //    Int64 count = (Int64)command.ExecuteScalar();

        //    Console.Write("{0}\n", count);
        //    conn.Close();
        //}
        private static string _connectionString = "Server=localhost;Database=MAS_Inventory;" + "Port=5432;Username=postgres;Password=erika9698;";

        static Npgsql_Helper()
        {
            _connectionString = "Server=localhost;Database=MAS_Inventory;" + "Port=5432;Username=postgres;Password=erika9698;";
        }

        public static DataTable Exec(string query)
        {
            DataTable result = new DataTable();
            try
            {
                using (NpgsqlConnection psqlConnection = new NpgsqlConnection("Server=localhost;Database=MAS_Inventory;" + "Port=5432;Username=postgres;Password=erika9698;"))
                {
                    psqlConnection.Open();
                    using (NpgsqlCommand psqlCommand = new NpgsqlCommand(query, psqlConnection))
                    {
                        psqlCommand.CommandTimeout = 6000;
                        using (NpgsqlDataReader psqlDataReader = psqlCommand.ExecuteReader())
                        {
                            result.Load(psqlDataReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw ex;
            }
            return result;
        }

        public static int ExecTransaction(string query, int expectedResult)
        {
            int result = 0;
            try
            {
                using (NpgsqlConnection psqlConnection = new NpgsqlConnection("Server=snws01;Port=5432;Database=ESDG;User Id=postgres;Password=postgres;"))
                {
                    psqlConnection.Open();
                    using (NpgsqlCommand psqlCommand = new NpgsqlCommand(query, psqlConnection))
                    {
                        using (NpgsqlTransaction psqlTransaction = psqlConnection.BeginTransaction())
                        {
                            psqlCommand.Transaction = psqlTransaction;
                            try
                            {
                                result = psqlCommand.ExecuteNonQuery();

                                if (result == expectedResult || expectedResult == -1)
                                    psqlTransaction.Commit();
                            }
                            catch (SqlException ex)
                            {
                                //Log.Error("Exec Transaction Error", ex);
                                psqlTransaction.Rollback();
                            }
                        }
                    }
                    psqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
