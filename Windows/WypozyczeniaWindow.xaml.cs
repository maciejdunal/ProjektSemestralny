using System;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Data.SqlClient;


namespace ProjektSemestralny.Windows
{
    /// <summary>
    /// Interaction logic for WypozyczeniaWindow.xaml
    /// </summary>
    public partial class WypozyczeniaWindow : Window
    {
        #region Window
        /// <summary>
        /// <c>WypozyczeniaWindow</c> window initialization in the center of the screen.
        /// </summary>
        public WypozyczeniaWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            SelectTableWindow sel = new SelectTableWindow();
            sel.ShowDialog();
        }
        #endregion

        #region UpdateDataGridMethod
        /// <summary>
        /// The <c>UpdateDataGrid</c> method.
        /// Executes a query that returns a given table.
        /// </summary>
        public void UpdateDataGrid()
        {
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText =
                "SELECT " +
                    "ID_wypozyczenia, " +
                    "ID_Gry, " +
                    "ID_pracownika, " +
                    "ID_klienta, " +
                    "Data_wypozyczenia " +
                "FROM Wypozyczenia";

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
            try
            {
                this.Operations("", 0);
                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
                resetAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + "Please provide correct data.", "Error");
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            this.Operations("", 1);
            resetAll();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Operations("", 2);
            this.resetAll();
        }
        private void resetAll()
        {
            ID_wypozyczenia_tb.Text = "";
            ID_gry_tb.Text = "";
            ID_pracownika_tb.Text = "";
            ID_klienta_tb.Text = "";
            Data_wypozyczenia_datapicker.SelectedDate = null;

            add_btn.IsEnabled = true;
            update_btn.IsEnabled = false;
            delete_btn.IsEnabled = false;
            ID_wypozyczenia_tb.IsEnabled = true;
        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            this.resetAll();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SelectTableWindow sel = new SelectTableWindow();
            sel.ShowDialog();
        }
        #endregion

        #region Operations(Add/Update/Delete)
        /// <summary>
        /// The <c>Operations</c> method.
        /// It allows to Add/Update/Delete a row.
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="state"></param>
        private void Operations(String statement, int state)
        {
            String msg = "";
            SqlCommand cmd = DatabaseService.con.CreateCommand();
            cmd.CommandText = statement;
            cmd.CommandType = CommandType.Text;

            var date = $@"{Data_wypozyczenia_datapicker.SelectedDate.Value.Month}/{Data_wypozyczenia_datapicker.SelectedDate.Value.Day}/{Data_wypozyczenia_datapicker.SelectedDate.Value.Year}";

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";

                    cmd.CommandText =
                         "INSERT INTO Wypozyczenia (" +
                             "ID_wypozyczenia, " +
                             "ID_Gry, " +
                             "ID_pracownika, " +
                             "ID_klienta, " +
                             "Data_wypozyczenia) " +

                         "VALUES(" +
                             $@"{Int32.Parse(ID_wypozyczenia_tb.Text)}, " +
                             $@"{Int32.Parse(ID_gry_tb.Text)}, " +
                             $@"{Int32.Parse(ID_pracownika_tb.Text)}, " +
                             $@"{Int32.Parse(ID_klienta_tb.Text)}, " +
                             $@"'{date}')";


                    break;
                case 1:
                    msg = "Row Updated Successfully!";

                    cmd.CommandText =
                        $@"UPDATE Wypozyczenia Set " +
                            $@"ID_Gry = {Int32.Parse(ID_gry_tb.Text)}, " +
                            $@"ID_pracownika = {Int32.Parse(ID_pracownika_tb.Text)}, " +
                            $@"ID_klienta = {Int32.Parse(ID_klienta_tb.Text)}, " +
                            $@"Data_wypozyczenia = '{date}' " +
                        $@"WHERE ID_wypozyczenia = {Int32.Parse(ID_wypozyczenia_tb.Text)};";

                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.CommandText =
                        "DELETE FROM Wypozyczenia " +
                        "WHERE ID_wypozyczenia = " +
                            $@"{Int32.Parse(ID_wypozyczenia_tb.Text)}";
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }
        #endregion

        #region MyDataGrid_SelectionChanged 
        /// <summary>
        /// The <c>MyDataGrid_SelectionChanged</c> method.
        /// Retrieves the values from the selected row into the appropriate text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                _ = DateTime.TryParse(dr["Data_wypozyczenia"].ToString(), out var dateTime);

                ID_wypozyczenia_tb.Text = dr["ID_wypozyczenia"].ToString();
                ID_gry_tb.Text = dr["ID_Gry"].ToString();
                ID_pracownika_tb.Text = dr["ID_pracownika"].ToString();
                ID_klienta_tb.Text = dr["ID_klienta"].ToString();
                Data_wypozyczenia_datapicker.SelectedDate = dateTime;

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
                ID_wypozyczenia_tb.IsEnabled = false;
            }
        }
        #endregion
    }
}
