using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

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
        /// The <c>connectButton_Click</c> method.
        /// Performing login to the database on 'Connect' button click.
        /// Uses DatabaseService.openConnection method and takes Login and Password parameters.
        /// Uses <c>OpenSelectTableWindow</c> method to open the SelectTableWindow window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseService.OpenConnection(logintb.Text, passwordtb.Password);
            OpenSelectTableWindow();
        }

        /// <summary>
        /// The <c>cancelButton_Click</c> method.
        /// Closing application on 'Cancel' button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// The <c>OpenSelectTableWindow</c> method.
        /// Opens next window.
        /// </summary>
        public void OpenSelectTableWindow()
        {
            var currentWindow = Application.Current.Windows [0];
            currentWindow.Hide();

            SelectTableWindow nextWindow = new SelectTableWindow();
            nextWindow.ShowDialog();
            currentWindow.Show();
        }
    }
}

