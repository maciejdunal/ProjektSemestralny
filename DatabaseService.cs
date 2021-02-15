using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace ProjektSemestralny
{
    class DatabaseService
    {   
        private const String CONN_STRING = "Data Source=localhost;Initial Catalog = Wypozyczalnia_Gier_komputerowych;";
        public static SqlConnection con;

        #region OpenConnectionMethod
        /// <summary>
        /// The <c>OpenConnection</c> Method .
        /// Connects to the database.
        /// Takes username parameter and password parameter to use it in connection to database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void OpenConnection(string username, string password)
        {

            con = new SqlConnection(AddCredentials(username, password));

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid username or password. Please try again", "Error");
            }
        }
        #endregion

        #region AddCredentialsMethod
        /// <summary>
        /// The <c>AddCredentials</c> method.
        /// Takes and saves username parameter and password parameter to ConnectionString
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string AddCredentials(string username, string password)
        {
            return CONN_STRING + "User ID=" + username + ";Password=" + password;
        }
        #endregion

        #region ReadTablesMethod
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
