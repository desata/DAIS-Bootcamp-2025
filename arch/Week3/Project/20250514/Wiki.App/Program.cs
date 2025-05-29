using Microsoft.Data.SqlClient;
using DT = System.Data;

namespace Wiki.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(
                    "Server=.;Database=WikiDatabase;Trusted_Connection=True;User Id=sa;Password=P@ssw0rd123;TrustServerCertificate=True;Connection Timeout=30;")
                )
            {
                connection.Open();
                Console.WriteLine("Connected successfully.");

                Program.SelectRows(connection);

                Console.WriteLine("Press any key to finish...");
                Console.ReadKey(true);

            }


        }

        static public void SelectRows(SqlConnection connection)
        {
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
SELECT  
	 
		FullName 
	FROM  Employees e where e.EmployeeId = 13; ";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("{0}",
                        reader.GetString(0));
                }
            }
        }
    }

    /**** Actual output:  
    Connected successfully.  
    Press any key to finish...  
    ****/
}
