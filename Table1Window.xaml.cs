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
    /// Interaction logic for Table1Window.xaml
    /// </summary>
    public partial class Table1Window : Window
    {
        Wypozyczalnia_Gier_komputerowychEntities1 dataEntities = new Wypozyczalnia_Gier_komputerowychEntities1();
        public Table1Window()
        {
            InitializeComponent();
        }

        private void Load_table(object sender, RoutedEventArgs e)
        {
            var query =
            from klienci in dataEntities.Kliencis
            select new {klienci.ID_klienta, klienci.Nazwisko, klienci.Imie, klienci.Adres, klienci.Kod_pocztowy, klienci.Data_urodzenia, klienci.Numer_DO };
            MyDataGrid.ItemsSource = query.ToList();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
