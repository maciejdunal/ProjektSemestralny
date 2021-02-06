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
            Query();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Query();
        }
        private void Window_Closed(object sender, EventArgs e)
        {

        }

        public void Query()
        {
            //var query =
            //    from klienci in dataEntities.Kliencis
            //    select new { klienci.ID_klienta, klienci.Nazwisko, klienci.Imie, klienci.Adres, klienci.Kod_pocztowy, klienci.Data_urodzenia, klienci.Numer_DO };
            //MyDataGrid.ItemsSource = query.ToList();
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText = "SELECT ID_klienta, Nazwisko, Imie, Adres, Kod_pocztowy, Data_urodzenia, Numer_DO FROM Klienci";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            MyDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql =
                "INSERT INTO Klienci(ID_klienta, Nazwisko, Imie, Adres, Kod_Pocztowy, Data_urodzenia, Numer_DO) " +
                "VALUES(:ID_klienta :Nazwisko, Imie, :Adres, :Kod_Pocztowy, :Data_urodzenia, :Numer_DO)";
            this.AUD(sql, 0);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;

        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql =
                "UPDATE Klienci SET Nazwisko = :Nazwisko" +
                    "Imie = :Imie," +
                    "Adres = :Adres," +
                    "Kod_pocztowy= :Kod_pocztowy" +
                    "Data_urodzenia= :Kod_pocztowy," +
                    "Numer_DO = :Numer_DO" +
                "WHERE ID_klienta = :ID_klienta";
            this.AUD(sql, 1);
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql =
                "DELETE FROM Klienci " +
                "WHERE ID_Klienta = :ID_Klienta";
            this.AUD(sql, 2);
            this.resetAll();
        }
        private void resetAll()
        {
            ID_Klienta_tb.Text = "";
            Nazwisko_tb.Text = "";
            Imie_tb.Text = "";
            Adres_tb.Text = "";
            Kod_pocztowy_tb.Text = "";
            Data_urodzenia_datapicker.SelectedDate = null;
            Numer_DO_tb.Text = "";

            add_btn.IsEnabled = true;
            update_btn.IsEnabled = false;
            delete_btn.IsEnabled = false;
        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            this.resetAll();
        }

        private void AUD(String statement, int state)
        {
            String msg = "";
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText = statement;
            cmd.CommandType = CommandType.Text;


            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";
                    cmd.Parameters.Add("ID_klienta", SqlDbType.Int, 6).Value = Int32.Parse(ID_Klienta_tb.Text);
                    cmd.Parameters.Add("Nazwisko", SqlDbType.VarChar, 50).Value = Nazwisko_tb.Text;
                    cmd.Parameters.Add("Imie", SqlDbType.VarChar, 50).Value = Imie_tb.Text;
                    cmd.Parameters.Add("Adres", SqlDbType.VarChar, 50).Value = Adres_tb.Text;
                    cmd.Parameters.Add("Kod_pocztowy", SqlDbType.VarChar, 50).Value = Kod_pocztowy_tb.Text;
                    cmd.Parameters.Add("Data_urodzenia", SqlDbType.Date, 7).Value = Data_urodzenia_datapicker.SelectedDate;
                    cmd.Parameters.Add("Numer_DO", SqlDbType.VarChar, 50).Value = Numer_DO_tb.Text;

                    break;
                case 1:
                    msg = "Row Updated Successfully!";

                    cmd.Parameters.Add("Nazwisko", SqlDbType.VarChar, 50).Value = Nazwisko_tb.Text;
                    cmd.Parameters.Add("Imie", SqlDbType.VarChar, 50).Value = Imie_tb.Text;
                    cmd.Parameters.Add("Adres", SqlDbType.VarChar, 50).Value = Adres_tb.Text;
                    cmd.Parameters.Add("Kod_pocztowy", SqlDbType.VarChar, 50).Value = Kod_pocztowy_tb.Text;
                    cmd.Parameters.Add("Data_urodzenia", SqlDbType.Date, 7).Value = Data_urodzenia_datapicker.SelectedDate;
                    cmd.Parameters.Add("Numer_DO", SqlDbType.VarChar, 50).Value = Numer_DO_tb.Text;

                    cmd.Parameters.Add("ID_klienta", SqlDbType.Int, 6).Value = Guid.Parse(ID_Klienta_tb.Text);

                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    //cmd.Parameters.Add("ID_klienta", SqlDbType.Int, 6).Value = Guid.Parse(ID_Klienta_tb.Text);
                    cmd.Parameters.Add("Imie", SqlDbType.VarChar, 50).Value = Imie_tb.Text;
                    break;
            }
            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    this.Query();
                }
            }
            catch (Exception exp) { }
        }
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                ID_Klienta_tb.Text = dr["ID_klienta"].ToString();
                Nazwisko_tb.Text = dr["Nazwisko"].ToString();
                Imie_tb.Text = dr["Imie"].ToString();
                Adres_tb.Text = dr["Adres"].ToString();
                Kod_pocztowy_tb.Text = dr["Kod_pocztowy"].ToString();
                Data_urodzenia_datapicker.SelectedDate = DateTime.Parse(dr["Data_urodzenia"].ToString());
                Numer_DO_tb.Text = dr["Numer_DO"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }
    }
}
