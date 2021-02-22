using System.Windows;
using System;
using System.Windows.Controls;

namespace ProjektSemestralny
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Window
        /// <summary>
        /// <c>Login</c> window initialization in the center of the screen.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loginMethod.SelectedIndex = 0;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Buttons
        /// <summary>
        /// The <c>connectButton_Click</c> method.
        /// Performing login to the database on 'Connect' button click.
        /// Uses DatabaseService.openConnection method and takes Login and Password parameters.
        /// Uses <c>OpenSelectTableWindow</c> method to open the SelectTableWindow window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        /// <summary>
        /// The <c>cancelButton_Click</c> method.
        /// Closing application on 'Cancel' button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Login Methods
        /// <summary>
        /// The <c>Login</c> method.
        /// Connects to SQL Server using Windows Authentication
        /// </summary>
        public void Login()
        {
            try
            {
                if (loginMethod.SelectedIndex == 0)
                {
                    WindowsAuthentication();
                }
                else if (loginMethod.SelectedIndex == 1)
                {
                    SqlServerAuthentication();
                }
                else { }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }
        /// <summary>
        /// The <c>WindowsAuthentication</c> method.
        /// Connects to SQL Server using Windows Authentication
        /// Uses DatabaseService.OpenConnectionWA method.
        /// </summary>
        private void WindowsAuthentication()
        {
            try
            {
                DatabaseService.OpenConnectionWA();
                SelectTableWindow sec = new SelectTableWindow();
                sec.Show();
                this.Hide();

            }
            catch (Exception)
            {
                logintb.Focus();
                logintb.Clear();
                passwordtb.Clear();
            }
        }
        /// <summary>
        /// The <c>SqlServerAuthentication</c> method.
        /// Connects to SQL Server using SQL Server Authentication.
        /// Uses DatabaseService.OpenConnectionSSA method and takes Login and Password parameters.
        /// </summary>
        private void SqlServerAuthentication()
        {
            try
            {
                var connectionSuccessful = DatabaseService.OpenConnectionSSA(logintb.Text, passwordtb.Password);
                SelectTableWindow sec = new SelectTableWindow();
                sec.Show();
                this.Hide();
            }
            catch (Exception)
            {
                logintb.Focus();
                logintb.Clear();
                passwordtb.Clear();
            }
        }
        #endregion

        #region MyDataGrid_SelectionChanged
        /// <summary>
        /// The <c>loginMethod_SelectionChanged</c> method.
        /// Checks the selected authentication. 
        /// If 'Windows Authentication' is selected, Login and Password text boxes are blocked. 
        /// If 'SQL Server Authentication' is selected, Login and Password text boxes are unlocked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loginMethod.SelectedIndex == 0)
            {
                logintb.IsEnabled = false;
                passwordtb.IsEnabled = false;
            }
            else if (loginMethod.SelectedIndex == 1)
            {
                logintb.IsEnabled = true;
                passwordtb.IsEnabled = true;
            }
            else { }
        }
        #endregion
    }
}

