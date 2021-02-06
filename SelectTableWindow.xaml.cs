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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SelectTableWindow : Window
    {
        ///<summary>
        /// <c>SelectTableWindow</c> initialization, calls the <c>RenderTables</c> method.
        /// </summary>
        public SelectTableWindow()
        {
            InitializeComponent();
            RenderTables();
        }

        /// <summary>
        /// The <c>RenderTables</c> method
        /// Writes the Query result to the dropdown
        /// </summary>
        public void RenderTables()
        {
            List<string> tables = DatabaseService.ReadTables();
            foreach (string table in tables)
            {
                tableList.Items.Add(table);
            }
            
            tableList.SelectedIndex = 0;
        }

        private void selectTableButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = tableList.SelectedItem.ToString();
            if (selectedItem == "Klienci")
            {
                var currentWindow = Application.Current.Windows[1];
                currentWindow.Hide();

                Table1Window nextWindow = new Table1Window();
                nextWindow.ShowDialog();
                currentWindow.Show();
            }
            else
                MessageBox.Show("The view is not implemented yet");
        }

        private void cancelTableButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
