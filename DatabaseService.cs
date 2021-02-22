using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace ProjektSemestralny
{
    /// <summary>
    ///  The <c>DatabaseService</c> class.
    ///  
    /// </summary>
    public class DatabaseService
    {
        #region Variables Declaration
        /// <summary>
        /// The Windows Authentication connectionString declaration.
        /// </summary>
        public const String connectionStringWA =
            "Data Source=localhost;" +
            "Integrated Security=True;" +
            "Trusted_Connection=Yes;" +
            "TrustServerCertificate=False;" +
            "Initial Catalog=Wypozyczalnia_Gier_komputerowych;";

        /// <summary>
        /// The SQL Server Authentication connectionString declaration.
        /// </summary>
        public const String connectionStringSSA = 
            "Data Source=localhost;" +
            "Integrated Security = False;" +
            "Initial Catalog=Wypozyczalnia_Gier_komputerowych;";

        public static SqlConnection con;
        #endregion

        #region OpenConnection Methods
        /// <summary>
        /// The <c>OpenConnection</c> Method .
        /// Connects to the database.
        /// Takes username parameter and password parameter to use it in connection to database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static bool OpenConnectionSSA(string username, string password)
        {
            con = new SqlConnection(AddCredentials(username, password));
            try
            {
                con.Open();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid username or password. Please try again", "Error");
                return false;
            }
        }
        

        public static bool OpenConnectionWA()
        {
            con = new SqlConnection(connectionStringWA);
            try
            {
                con.Open();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid username or password. Please try again", "Error");
                return false;
            }
        }
        #endregion

        #region AddCredentials Method
        /// <summary>
        /// The <c>AddCredentials</c> method.
        /// Takes and saves username parameter and password parameter to ConnectionString
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string AddCredentials(string username, string password)
        {
            return connectionStringSSA + "User ID=" + username + ";Password=" + password;
        }
        #endregion

        #region ReadTables Method
        /// <summary>
        /// Reads database table list
        /// </summary>
        /// <returns>Table List</returns>
        public static List<String> ReadTables()
        {
            List<String> result = new List<String>(); 
            
            using (SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM Wypozyczalnia_Gier_komputerowych.INFORMATION_SCHEMA.TABLES;", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        result.Add((string)reader[0]);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
