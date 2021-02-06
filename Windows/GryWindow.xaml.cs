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
    /// Interaction logic for GryWindow.xaml
    /// </summary>
    public partial class GryWindow : Window
    {
        public GryWindow()
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
                    "ID_gry, " +
                    "Nazwa, " +
                    "Kategoria, " +
                    "Kategoria_wiekowa, " +
                    "Data_wydania, " +
                    "Cena_dzien " + 
                "FROM Gry";

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
            ID_gry_tb.Text = "";
            Nazwa_tb.Text = "";
            Kategoria_tb.Text = "";
            Kategoria_wiekowa_tb.Text = "";
            Data_wydania_tb.Text = "";
            Cena_dzien_tb.Text = "";

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

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";

                    cmd.CommandText =
                         "INSERT INTO Gry (" +
                             "ID_gry, " +
                             "Nazwa, " +
                             "Kategoria, " +
                             "Kategoria_wiekowa, " +
                             "Data_wydania, " +
                             "Cena_dzien) " +

                         "VALUES(" +
                             $@"{Int32.Parse(ID_gry_tb.Text)}, " +
                             $@"'{Nazwa_tb.Text}', " +
                             $@"'{Kategoria_tb.Text}', " +
                             $@"'{Kategoria_wiekowa_tb.Text}', " +
                             $@"'{Data_wydania_tb.Text}', " +
                             $@"'{Cena_dzien_tb.Text}')";

                    break;
                case 1:
                    msg = "Row Updated Successfully!";

                    cmd.CommandText =
                        $@"UPDATE Gry Set " +
                            $@"Nazwa = '{Nazwa_tb.Text}', " +
                            $@"Kategoria = '{Kategoria_tb.Text}', " +
                            $@"Kategoria_wiekowa = '{Kategoria_wiekowa_tb.Text}', " +
                            $@"Data_wydania = '{Data_wydania_tb.Text}', " +
                            $@"Cena_dzien = '{Cena_dzien_tb.Text}' " +
                        $@"WHERE ID_gry = {Int32.Parse(ID_gry_tb.Text)};";

                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.CommandText =
                        "DELETE FROM Gry " +
                        "WHERE ID_gry = " +
                            $@"{Int32.Parse(ID_gry_tb.Text)}";
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

                ID_gry_tb.Text = dr["ID_gry"].ToString();
                Nazwa_tb.Text = dr["Nazwa"].ToString();
                Kategoria_tb.Text = dr["Kategoria"].ToString();
                Kategoria_wiekowa_tb.Text = dr["Kategoria_wiekowa"].ToString();
                Data_wydania_tb.Text = dr["Data_wydania"].ToString();
                Cena_dzien_tb.Text = dr["Cena_dzien"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }
    }
}
