﻿using System;
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
    /// Interaction logic for ZwrotyWindow.xaml
    /// </summary>
    public partial class ZwrotyWindow : Window
    {
        public ZwrotyWindow()
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
            ID_wypozyczenia_tb.Text = "";
            ID_pracownika_tb.Text = "";
            data_zwrotu_datapicker.SelectedDate = null;
            doplaty_tb.Text = "";


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
            catch (Exception ex) { }
        }
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

            }
        }
    }
}
