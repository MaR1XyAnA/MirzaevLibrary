using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using MirzaevLibrary.ViewFolder.PageFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class AuthorizationWindow : Window
    {
        MainWindow mainWindow = new MainWindow();

        public AuthorizationWindow()
        {
            try { InitializeComponent(); AppConnectClass.DataBase = new LibraryMirzayevaEntities(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "AU001 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public void EnterUser()
        {
            try
            {
                var GetUser = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.LoginUser == LoginTextBox.Text && data.PasswordUser == PasswordPasswordBox.Password);
                if (GetUser != null)
                {
                    UserClass.GetUserTable = GetUser;
                    FrameNavigationClass.BodyFNC.Navigate(new ProfilePage(UserClass.GetUserTable));
                    mainWindow.Show(); this.Close();
                }
                else { MessageBox.Show("Не правильный логин или пароль", "Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            catch (Exception ex) {  MessageBox.Show(ex.Message.ToString(), "AU002 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e) { EnterUser(); }

        private void IgnoreAutborizationButton_Click(object sender, RoutedEventArgs e) { MainWindow mainWindow = new MainWindow(); mainWindow.Show(); this.Close(); }

        private void PasswordPasswordBox_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { EnterUser(); } }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e) { RegistrationWindow registrationWindow = new RegistrationWindow(); registrationWindow.Show(); this.Close(); }

        private void VisiblePasswordUserButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Collapsed;  VisiblePasswordGrid.Visibility = Visibility.Visible;
            string PasswordString = Convert.ToString(PasswordPasswordBox.Password);
            PasswordTextBox.Text = PasswordString;
        }

        private void VisiblePasswordUserButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Visible; VisiblePasswordGrid.Visibility = Visibility.Collapsed;
            string PasswordString = Convert.ToString(PasswordTextBox.Text);
            PasswordPasswordBox.Password = PasswordString;
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 0) { HintLoginTextBlock.Visibility = Visibility.Collapsed; }
            else { HintLoginTextBlock.Visibility = Visibility.Visible; }
        }

        private void PasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPasswordBox.Password.Length > 0) { HintPasswordVisibilityTextBlock.Visibility = Visibility.Collapsed; }
            else { HintPasswordVisibilityTextBlock.Visibility = Visibility.Visible; }
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordTextBox.Text.Length > 0) { HintPasswordCollapsedTextBlock.Visibility = Visibility.Collapsed; }
            else { HintPasswordCollapsedTextBlock.Visibility = Visibility.Visible; }
        }

        #region Управление окном
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }} // Для того, что бы окно перетаскивать
        private void CloseButton_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void RollUpButton_Click(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }
        #endregion

        private void NextButton_LayoutUpdated(object sender, EventArgs e)
        {
            string LoginString, PassworPasswordString;
            LoginString = Convert.ToString(LoginTextBox.Text); PassworPasswordString = Convert.ToString(PasswordPasswordBox.Password);
            if (PassworPasswordString == "" || LoginString == "") { NextButton.IsEnabled = false; }
            else { NextButton.IsEnabled = true; }
        }
    }
}

