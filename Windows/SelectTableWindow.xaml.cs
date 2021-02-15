using System.Collections.Generic;
using System.Windows;



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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        #region Window_Closed
        private void Window_Closed(object sender, System.EventArgs e)
        {
            this.Close();
            MainWindow log = new MainWindow();
            log.ShowDialog();
        }
        #endregion

        #region RenderTablesMethod
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
#endregion

        #region Buttons
        /// <summary>
        /// The <c>selectTableButton_Click</c> method.
        /// Opens table management for selected table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectTableButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable();
        }

        private void cancelTableButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow log = new MainWindow();
            log.ShowDialog();
        }
        #endregion

        #region SelectedTable method
        /// <summary>
        /// The <c>SelectedTable</c> method.
        /// Opens table management for selected table
        /// </summary>
        public void SelectedTable()
        {
            string selectedItem = tableList.SelectedItem.ToString();
            if (selectedItem == "Klienci")
            {
                this.Hide();
                KlienciWindow kli = new KlienciWindow();
                kli.ShowDialog();
            }
            if (selectedItem == "Gry")
            {
                this.Hide();
                GryWindow nextWindow = new GryWindow();
                nextWindow.ShowDialog();
            }
            if (selectedItem == "Pracownicy")
            {
                this.Hide();
                Windows.PracownicyWindow nextWindow = new Windows.PracownicyWindow();
                nextWindow.ShowDialog();
            }
            if (selectedItem == "Wypozyczenia")
            {
                this.Hide();
                Windows.WypozyczeniaWindow nextWindow = new Windows.WypozyczeniaWindow();
                nextWindow.ShowDialog();
            }
            if (selectedItem == "Zwroty")
            {
                this.Hide();
                Windows.ZwrotyWindow nextWindow = new Windows.ZwrotyWindow();
                nextWindow.ShowDialog();
            }
            else { }
        }
        #endregion
    }
}