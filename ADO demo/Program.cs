using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ADO_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Northwind ADO.Net Console Demo");

            // 1. Connection
            SqlConnection NorthwindConnection = new();
            NorthwindConnection.ConnectionString = @"Data Source=(Local);Initial Catalog=Northwind;Integrated Security=True";

            // 2. Command
            SqlCommand NorthwindCommand = new SqlCommand();
            NorthwindCommand.CommandType = CommandType.StoredProcedure;
            NorthwindCommand.Connection = NorthwindConnection;
            NorthwindCommand.CommandText = "CustOrderHist";

            //@CustomerID nchar(5)
            SqlParameter CustomerID = new SqlParameter();
            CustomerID.ParameterName = "@CustomerID";
            CustomerID.SqlDbType = SqlDbType.NChar;
            CustomerID.Value = "EASTC"; //Hardcoded
            CustomerID.Direction = ParameterDirection.Input; //Default value

            NorthwindCommand.Parameters.Add((CustomerID));

            // Open the Connection
            NorthwindConnection.Open();

            SqlDataReader NorthwindDataReader;
            NorthwindDataReader = NorthwindCommand.ExecuteReader(); // or ExecuteNonQuery().

            for (int col = 0; col < NorthwindDataReader.FieldCount; col++)
            {
                Console.Write(NorthwindDataReader.GetName(col).ToString());
            }
            Console.WriteLine("\n--------------------\n");

            if(NorthwindDataReader.HasRows) // Make sure the ResultSet isn't empty.
            {
                while (NorthwindDataReader.Read())
                {
                    Console.Write(NorthwindDataReader["ProductName"] + "\t");
                    Console.WriteLine(NorthwindDataReader["Total"]);
                }
            }
            

            NorthwindConnection.Close();


        }
    }
}
