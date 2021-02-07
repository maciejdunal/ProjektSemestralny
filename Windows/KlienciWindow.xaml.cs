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
    /// Interaction logic for KlienciWindow.xaml
    /// </summary>
    public partial class KlienciWindow : Window
    {
        /// <summary>
        /// <c>KlienciWindow</c> window initialization
        /// </summary>
        public KlienciWindow()
        {
            InitializeComponent();
        }

        #region Window_Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateDataGrid();
        }
        #endregion

        #region Window_Closed
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region UpdateDataGridMethod
        /// <summary>
        /// The <c>UpdateDataGrid</c> method.
        /// Executes a query that returns a given table
        /// </summary>
        public void UpdateDataGrid()
        {
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText =
                "SELECT " +
                    "ID_klienta, " +
                    "Nazwisko, " +
                    "Imie, " +
                    "Adres, " +
                    "Kod_pocztowy, " +
                    "Data_urodzenia, " +
                    "Numer_DO " + 
                "FROM Klienci";

            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            MyDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }
        #endregion

        #region Buttons
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

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Operations
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
                         "INSERT INTO Klienci (" +
                             "ID_klienta, " +
                             "Nazwisko, " +
                             "Imie, " +
                             "Adres, " +
                             "Kod_Pocztowy, " +
                             "Data_urodzenia, " +
                             "Numer_DO) " +

                         "VALUES(" +
                             $@"{Int32.Parse(ID_Klienta_tb.Text)}, " +
                             $@"'{Nazwisko_tb.Text}', " +
                             $@"'{Imie_tb.Text}', " +
                             $@"'{Adres_tb.Text}', " +
                             $@"'{Kod_pocztowy_tb.Text}', " +
                             $@"'{date}', " +
                             $@"'{Numer_DO_tb.Text}')";

                    break;
                case 1:
                    msg = "Row Updated Successfully!";

                    cmd.CommandText =
                        $@"UPDATE Klienci Set " +
                            $@"Nazwisko = '{Nazwisko_tb.Text}', " +
                            $@"Imie = '{Imie_tb.Text}', " +
                            $@"Adres = '{Adres_tb.Text}', " +
                            $@"Kod_Pocztowy = '{Kod_pocztowy_tb.Text}', " +
                            $@"Data_urodzenia = '{date}', " +
                            $@"Numer_DO = '{Numer_DO_tb.Text}' " +
                        $@"WHERE ID_klienta = {Int32.Parse(ID_Klienta_tb.Text)};";

                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.CommandText =
                        "DELETE FROM Klienci " +
                        "WHERE ID_klienta = " +
                            $@"{Int32.Parse(ID_Klienta_tb.Text)}";
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
            catch (Exception) { }
        }
        #endregion

        #region MyDataGrid_SelectionChanged
        /// <summary>
        /// The <c>MyDataGrid_SelectionChanged</c> method.
        /// Retrieves the values from the selected row into the appropriate text fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                _ = DateTime.TryParse(dr["Data_urodzenia"].ToString(), out var dateTime);

                ID_Klienta_tb.Text = dr["ID_klienta"].ToString();
                Nazwisko_tb.Text = dr["Nazwisko"].ToString();
                Imie_tb.Text = dr["Imie"].ToString();
                Adres_tb.Text = dr["Adres"].ToString();
                Kod_pocztowy_tb.Text = dr["Kod_pocztowy"].ToString();
                Data_urodzenia_datapicker.SelectedDate = dateTime;
                Numer_DO_tb.Text = dr["Numer_DO"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }
        #endregion
    }
}