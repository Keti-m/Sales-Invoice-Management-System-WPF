using System;
using System.Data;
using System.Data.SqlClient;

namespace SaleInvoice.DatabaseProject
{
    class DBAccess
    {
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader DbReader;
        private static SqlDataAdapter adapter = new SqlDataAdapter();

        private static string strConnString =
            "Data Source=keti\\sqlexpress;Initial Catalog=Project;Integrated Security=True;";

        public void createConn()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeConn()
        {
            connection.Close();
        }

        public int executeQuery(SqlCommand dbCommand)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    createConn();

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public void readDatathroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Vetëm për SELECT (lexon rresht për rresht)
        public SqlDataReader readDatathroughReader(string query)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                DbReader = command.ExecuteReader();
                return DbReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
