using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class AuthorizationWindow : Window
    {
        int NumberIncorrectAttempts = 0; // Переменная для подсчёта количества неправильно введённого пароля.
        MainWindow mainWindow = new MainWindow();

        public AuthorizationWindow()
        {
            try { InitializeComponent(); AppConnectClass.DataBase = new LibraryMirzayevaEntities(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "AU001 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        #region Действие
        private void AnimLoadingStart() { LoadingApplicationProgressBar.Visibility = Visibility.Visible; LoadingApplicationProgressBar.IsIndeterminate = true; } // Запуск анимации загрузки
        private void AnimLoadingEnd() { LoadingApplicationProgressBar.Visibility = Visibility.Collapsed; LoadingApplicationProgressBar.IsIndeterminate = false; } // Остановка анимации загрузки
        private async void EnterUser() // АсинхронныйМ метод для входа пользователя
        {
            try
            {
                AnimLoadingStart();
                string GrtLogin = LoginTextBox.Text; // Создаём переменную типа string и вставляем туда данные из LoginTextBox потому что блять эта хуйня по другому не хочеит работать блять!!!!!!!!!!!!!!!!!!!!!
                var GetUser = await AppConnectClass.DataBase.UserTable.FirstOrDefaultAsync(data => data.LoginUser == GrtLogin); // Переменная которая ищет пользователя по Login (LINQ - запрос)
                
                if (GetUser != null) // Если пользователь существует
                {
                    if (PasswordPasswordBox.Password != GetUser.PasswordUser) // Если пароль не правильный
                    {
                        NumberIncorrectAttempts++; // +1 к переменной
                        MessageBox.Show("Не правильный логин или пароль", "AU002 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                        if (NumberIncorrectAttempts >= 5) { ResetPasswordTextBlock.Visibility = Visibility.Visible; } // Если переменная = или > 5
                    }
                    else // Если пользователь существует и пароль введён правильно
                    {
                        UserClass.GetUserTable = GetUser; // В переменную в классе отпровляем "Ссылку" на пользователя
                        mainWindow.Show(); this.Close();
                    }
                }
                else { MessageBox.Show("Данного пользователя не существует", "Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }  // Если пользователя не существует 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "AU003 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally  { AnimLoadingEnd(); }
        }
        #endregion

        #region Click
        private void NextButton_Click(object sender, RoutedEventArgs e) { EnterUser(); }

        private void IgnoreAutborizationButton_Click(object sender, RoutedEventArgs e) { MainWindow mainWindow = new MainWindow(); mainWindow.Show(); this.Close(); }

        private void ForgetPasswordHyperlink_Click(object sender, RoutedEventArgs e) { ForgetWindow forgetWindow = new ForgetWindow(); forgetWindow.Show(); this.Close(); }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e) { RegistrationWindow registrationWindow = new RegistrationWindow(); registrationWindow.Show(); this.Close(); }
        #endregion

        #region KeyDown
        private void PasswordPasswordBox_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { EnterUser(); } }
        #endregion

        #region PreviewMouseDown_PreviewMouseUp
        private void VisiblePasswordUserButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Collapsed; VisiblePasswordGrid.Visibility = Visibility.Visible;
            string PasswordString = Convert.ToString(PasswordPasswordBox.Password);
            PasswordTextBox.Text = PasswordString;
        }

        private void VisiblePasswordUserButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Visible; VisiblePasswordGrid.Visibility = Visibility.Collapsed;
            string PasswordString = Convert.ToString(PasswordTextBox.Text);
            PasswordPasswordBox.Password = PasswordString;
        }
        #endregion

        #region TextChanged_PasswordChanged
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
        #endregion

        #region LayoutUpdated
        private void NextButton_LayoutUpdated(object sender, EventArgs e)
        {
            string LoginString, PassworPasswordString;
            LoginString = Convert.ToString(LoginTextBox.Text); PassworPasswordString = Convert.ToString(PasswordPasswordBox.Password);
            if (PassworPasswordString == "" || LoginString == "") { NextButton.IsEnabled = false; }
            else { NextButton.IsEnabled = true; }
        }
        #endregion

        #region Управление окном
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) // Для того, что бы окно перетаскивать 
        {
            try { if (e.ChangedButton == MouseButton.Left) { this.DragMove(); } }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "AUBU001 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try { Application.Current.Shutdown(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "AUBU002 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            try { WindowState = WindowState.Minimized; }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "AUBU003 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion
    }
}

