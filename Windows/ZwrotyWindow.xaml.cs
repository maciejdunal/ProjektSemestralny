using System;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Data.SqlClient;


namespace ProjektSemestralny.Windows
{
    /// <summary>
    /// Interaction logic for ZwrotyWindow.xaml
    /// </summary>
    public partial class ZwrotyWindow : Window
    {
        #region Window
        /// <summary>
        /// <c>ZwrotyWindow</c> window initialization in the center of the screen.
        /// </summary>
        public ZwrotyWindow()
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
                    "ID_pracownika, " +
                    "data_zwrotu, " +
                    "doplaty " +
                "FROM Zwroty";

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
            ID_pracownika_tb.Text = "";
            data_zwrotu_datapicker.SelectedDate = null;
            doplaty_tb.Text = "";


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

        #region Operations(Add/Update/Delete/Reset)
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


            var date = $@"{data_zwrotu_datapicker.SelectedDate.Value.Month}/{data_zwrotu_datapicker.SelectedDate.Value.Day}/{data_zwrotu_datapicker.SelectedDate.Value.Year}";

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";

                    cmd.CommandText =
                         "INSERT INTO Zwroty (" +
                             "ID_wypozyczenia, " +
                             "ID_pracownika, " +
                             "data_zwrotu, " +
                             "doplaty) " +

                         "VALUES(" +
                             $@"{Int32.Parse(ID_wypozyczenia_tb.Text)}, " +
                             $@"{Int32.Parse(ID_pracownika_tb.Text)}, " +
                             $@"'{date}', " +
                             $@"{Int32.Parse(doplaty_tb.Text)})";


                    break;
                case 1:
                    msg = "Row Updated Successfully!";

                    cmd.CommandText =
                        $@"UPDATE Zwroty Set " +
                            $@"ID_pracownika = {Int32.Parse(ID_pracownika_tb.Text)}, " +
                            $@"data_zwrotu = '{date}', " +
                            $@"doplaty = {Int32.Parse(doplaty_tb.Text)} " +
                        $@"WHERE ID_wypozyczenia = {Int32.Parse(ID_wypozyczenia_tb.Text)};";

                    break;
                case 2:
                    msg = "Row Deleted Successfully!";

                    cmd.CommandText =
                        "DELETE FROM Zwroty " +
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
                _ = DateTime.TryParse(dr["data_zwrotu"].ToString(), out var dateTime);

                ID_wypozyczenia_tb.Text = dr["ID_wypozyczenia"].ToString();
                ID_pracownika_tb.Text = dr["ID_pracownika"].ToString();
                data_zwrotu_datapicker.SelectedDate = dateTime;
                doplaty_tb.Text = dr["doplaty"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
                ID_wypozyczenia_tb.IsEnabled = false;
            }
        }
        #endregion
    }
}
