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

namespace ProjektSemestralny.Windows
{
    /// <summary>
    /// Interaction logic for PracownicyWindow.xaml
    /// </summary>
    public partial class PracownicyWindow : Window
    {
        public PracownicyWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateDataGrid();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }

        public void UpdateDataGrid()
        {
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText =
                "SELECT " +
                    "ID_pracownika, " +
                    "Nazwisko, " +
                    "Imie, " +
                    "Data_urodzenia, " +
                    "Adres, " +
                    "Stanowisko " +
                "FROM Pracownicy";

            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            MyDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Operations("", 0);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Operations("", 1);
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Operations("", 2);
            this.resetAll();
        }
        private void resetAll()
        {
            ID_pracownika_tb.Text = "";
            Imie_tb.Text = "";
            Nazwisko_tb.Text = "";
            Data_urodzenia_datapicker.SelectedDate = null;
            Adres_tb.Text = "";
            Stanowisko_tb.Text = "";

            add_btn.IsEnabled = true;
            update_btn.IsEnabled = false;
            delete_btn.IsEnabled = false;
        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            this.resetAll();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// The <c>Operations</c> method.
        /// It allows  to Add, Update and Delete operations
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="state"></param>
        private void Operations(String statement, int state)
        {
            String msg = "";
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText = statement;
            cmd.CommandType = CommandType.Text;

            var date = $@"{Data_urodzenia_datapicker.SelectedDate.Value.Month}/{Data_urodzenia_datapicker.SelectedDate.Value.Day}/{Data_urodzenia_datapicker.SelectedDate.Value.Year}";

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";

                    cmd.CommandText =
                         "INSERT INTO Pracownicy (" +
                             "ID_pracownika, " +
                             "Imie, " +
                             "Nazwisko, " +
                             "Data_urodzenia, " +
                             "Adres, " +
                             "Stanowisko) " +

                         "VALUES(" +
                             $@"{Int32.Parse(ID_pracownika_tb.Text)}, " +
                             $@"'{Imie_tb.Text}', " +
                             $@"'{Nazwisko_tb.Text}', " +
                             $@"'{date}', " +
                             $@"'{Adres_tb.Text}', " +
                             $@"'{Stanowisko_tb.Text}')";

                    break;
                case 1:
                    msg = "Row Updated Successfully!";

                    cmd.CommandText =
                        $@"UPDATE Pracownicy Set " +
                            $@"Imie = '{Imie_tb.Text}', " +
                            $@"Nazwisko = '{Nazwisko_tb.Text}', " +
                            $@"Data_urodzenia = '{date}', " +
                            $@"Adres = '{Adres_tb.Text}', " +
                            $@"Stanowisko = '{Stanowisko_tb.Text}' " +
                        $@"WHERE ID_pracownika = {Int32.Parse(ID_pracownika_tb.Text)};";

                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.CommandText =
                        "DELETE FROM Klienci " +
                        "WHERE ID_klienta = " +
                            $@"{Int32.Parse(ID_pracownika_tb.Text)}";
                    break;
            }
            try
            {
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show(msg);
                    this.UpdateDataGrid();
                }
            }
            catch (Exception ex) { }
        }
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                _ = DateTime.TryParse(dr["Data_urodzenia"].ToString(), out var dateTime);

                ID_pracownika_tb.Text = dr["ID_pracownika"].ToString();
                Imie_tb.Text = dr["Imie"].ToString();
                Nazwisko_tb.Text = dr["Nazwisko"].ToString();
                Data_urodzenia_datapicker.SelectedDate = dateTime;
                Adres_tb.Text = dr["Adres"].ToString();
                Stanowisko_tb.Text = dr["Stanowisko"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }
    }
}
