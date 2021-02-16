﻿using System.Windows;
using System;


namespace ProjektSemestralny
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// <c>MainWindow</c> (Login) window initialization
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        #region Window_Closed
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
            try
            {
                DatabaseService.OpenConnection(logintb.Text, passwordtb.Password);
                
               // this.Hide();
                SelectTableWindow sec = new SelectTableWindow();
                sec.Show();
                this.Hide();
                //sec.ShowDialog();
            }

            catch (Exception)
            {
                logintb.Focus();
                logintb.Clear();
                passwordtb.Clear();
            }
            finally 
            {

            }
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
    }
}
