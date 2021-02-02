using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace ProjektSemestralny
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Login window initialization
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method performing login to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            downloadData();
        }

        /// <summary>
        /// Method connecting to the database
        /// </summary>
        public void downloadData()
        {
            string myConnection =
                "SERVER=" + serverNametb.Text + ";" +
                "DATABASE=" + databaseNametb.Text + ";" +
                "UID=" + logintb.Text + ";" +
                "PASSWORD=" + passwordtb.Password + ";";

            SqlConnection connection = new SqlConnection(myConnection);

                try
                {
                    connection.Open();
                    var Nextwindow = new Window1();
                    Nextwindow.ShowDialog();
                }

            ///<exception cref="SqlException">Thrown when fail logging.</exception>
            catch (SqlException ex)
                {
                    MessageBox.Show("Błąd logowania do bazy danych", "Błąd");
                }
                connection.Close();
        }
     }
}

