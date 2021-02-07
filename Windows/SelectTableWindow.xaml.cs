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
        /// <summary>
        /// The <c>selectTableButton_Click</c> method.
        /// Opens table managment for selected table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectTableButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = tableList.SelectedItem.ToString();
            if (selectedItem == "Klienci")
            {
                var currentWindow = Application.Current.Windows[1];
                currentWindow.Hide();

                KlienciWindow nextWindow = new KlienciWindow();
                nextWindow.ShowDialog();
                currentWindow.Show();
            }
            if (selectedItem == "Gry")
            {
                var currentWindow = Application.Current.Windows[1];
                currentWindow.Hide();

                GryWindow nextWindow = new GryWindow();
                nextWindow.ShowDialog();
                currentWindow.Show();
            }
            if (selectedItem == "Pracownicy")
            {
                var currentWindow = Application.Current.Windows[1];
                currentWindow.Hide();

                Windows.PracownicyWindow nextWindow = new Windows.PracownicyWindow();
                nextWindow.ShowDialog();
                currentWindow.Show();
            }
            if (selectedItem == "Wypozyczenia")
            {
                var currentWindow = Application.Current.Windows[1];
                currentWindow.Hide();

                Windows.WypozyczeniaWindow nextWindow = new Windows.WypozyczeniaWindow();
                nextWindow.ShowDialog();
                currentWindow.Show();
            }
            if (selectedItem == "Zwroty")
            {
                var currentWindow = Application.Current.Windows[1];
                currentWindow.Hide();

                Windows.ZwrotyWindow nextWindow = new Windows.ZwrotyWindow();
                nextWindow.ShowDialog();
                currentWindow.Show();
            }
            else { }
                
        }

        private void cancelTableButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}