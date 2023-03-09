using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class AuthorizationWindow : Window
    {
        int NumberIncorrectAttempts = 0; // Переменная для подсчёта количества неправильно введённого пароля.
        MainWindow mainWindow = new MainWindow();

        public AuthorizationWindow()
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "AU001 - Ошибка акторизации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                if (Properties.Settings.Default.CheckSlider_Settings == 100)
                {
                    LoginTextBox.Text = Properties.Settings.Default.LoginUser_Settings; // Вытягиваем в LoginTextBox логин пользователя
                    PasswordPasswordBox.Password = Properties.Settings.Default.PasswordUser_Settings; // Вытягиваем в PasswordPasswordBox пароль пользователя
                    RememberUserSlider.Value = Properties.Settings.Default.CheckSlider_Settings;
                }
            }
        }

        #region Color
        SolidColorBrush ValueBlue = new SolidColorBrush(Color.FromRgb(82, 158, 244));
        SolidColorBrush ValueWheat = new SolidColorBrush(Color.FromRgb(225, 227, 230));
        #endregion
        #region Действие
        private void AnimLoadingStart() // Запуск анимации загрузки
        {
            LoadingApplicationProgressBar.Visibility = Visibility.Visible;
            LoadingApplicationProgressBar.IsIndeterminate = true;
        }

        private void AnimLoadingEnd() // Остановка анимации загрузки
        {
            LoadingApplicationProgressBar.Visibility = Visibility.Collapsed;
            LoadingApplicationProgressBar.IsIndeterminate = false;
        }
        public void SaveSettings()
        {
            if (RememberUserSlider.Value == 100) // Проверяем, прокручен ли Slider
            {
                Properties.Settings.Default.CheckSlider_Settings = 100; // Сохраняем логин в приложении
                Properties.Settings.Default.LoginUser_Settings = LoginTextBox.Text; // Сохраняем логин в приложении
                Properties.Settings.Default.PasswordUser_Settings = PasswordPasswordBox.Password; // Сохраняем пароль в приложении
                Properties.Settings.Default.Save(); // Сохраняем данные в приложении
            }
            else
            {
                Properties.Settings.Default.CheckSlider_Settings = 0; // Сохраняем логин в приложении
                Properties.Settings.Default.LoginUser_Settings = null; // Сохраняем логин в приложении
                Properties.Settings.Default.PasswordUser_Settings = null; // Сохраняем пароль в приложении
                Properties.Settings.Default.Save(); // Сохраняем данные в приложении
            }
        }
        private async void EnterUser() // АсинхронныйМ метод для входа пользователя
        {
            try
            {
                AnimLoadingStart();
                string ReceiveLogin = LoginTextBox.Text; // Создаём переменную типа string и вставляем туда данные из LoginTextBox потому что блять эта хуйня по другому не хочеит работать блять!!!!!!!!!!!!!!!!!!!!!
                var GetUser = await AppConnectClass.DataBase.UserTable.FirstOrDefaultAsync(data => data.Login_User == ReceiveLogin); // Переменная которая ищет пользователя по Login (LINQ - запрос)

                if (GetUser != null) // Если пользователь существует
                {
                    if (PasswordPasswordBox.Password != GetUser.Password_User) // Если пароль не правильный
                    {
                        NumberIncorrectAttempts++; // +1 к переменной

                        MessageBox.Show(
                            "Не правильный логин или пароль",
                            "AU002 - Ошибка акторизации",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);

                        if (NumberIncorrectAttempts >= 5) // Если переменная = или > 5
                        {
                            ResetPasswordTextBlock.Visibility = Visibility.Visible;
                        }
                    }
                    else // Если пользователь существует и пароль введён правильно
                    {
                        SaveSettings();
                        UserClass.GetUserTable = GetUser; // В переменную в классе отпровляем "Ссылку" на пользователя
                        mainWindow.Show();
                        this.Close();
                    }
                }
                else // Если пользователя не существует 
                {
                    MessageBox.Show(
                        "Данного пользователя не существует",
                        "Ошибка акторизации",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "AU003 - Ошибка акторизации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally { AnimLoadingEnd(); }
        }
        #endregion
        #region Click
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            EnterUser();
        }

        private void IgnoreAutborizationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ForgetPasswordHyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgetWindow forgetWindow = new ForgetWindow();
            forgetWindow.Show();
            this.Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
        #endregion
        #region KeyDown
        private void PasswordPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterUser();
            }
        }
        #endregion
        #region PreviewMouseDown_PreviewMouseUp
        private void VisiblePasswordUserButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Collapsed;
            VisiblePasswordGrid.Visibility = Visibility.Visible;
            string PasswordShow = Convert.ToString(PasswordPasswordBox.Password);
            PasswordTextBox.Text = PasswordShow;
        }

        private void VisiblePasswordUserButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Visible;
            VisiblePasswordGrid.Visibility = Visibility.Collapsed;
            string PasswordHide = Convert.ToString(PasswordTextBox.Text);
            PasswordPasswordBox.Password = PasswordHide;
        }
        #endregion
        #region TextChanged_PasswordChanged_ValueChanged
        private void RememberUserSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (RememberUserSlider.Value == 100)
            {
                RememberUserNoTextBlock.Foreground = ValueWheat;
                RememberUserYesTextBlock.Foreground = ValueBlue;
            }
            else if (RememberUserSlider.Value == 0)
            {
                RememberUserNoTextBlock.Foreground = ValueBlue;
                RememberUserYesTextBlock.Foreground = ValueWheat;
            }
            else
            {
                RememberUserNoTextBlock.Foreground = ValueWheat;
                RememberUserYesTextBlock.Foreground = ValueWheat;
            }
        }
        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 0)
            {
                HintLoginTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintLoginTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void PasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPasswordBox.Password.Length > 0)
            {
                HintPasswordVisibilityTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintPasswordVisibilityTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordTextBox.Text.Length > 0)
            {
                HintPasswordCollapsedTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintPasswordCollapsedTextBlock.Visibility = Visibility.Visible;
            }
        }
        #endregion
        #region LayoutUpdated
        private void NextButton_LayoutUpdated(object sender, EventArgs e)
        {
            string LoginString, PassworPasswordString;
            LoginString = Convert.ToString(LoginTextBox.Text);
            PassworPasswordString = Convert.ToString(PasswordPasswordBox.Password);

            if (PassworPasswordString == "" || LoginString == "")
            {
                NextButton.IsEnabled = false;
            }
            else
            {
                NextButton.IsEnabled = true;
            }
        }
        #endregion
        #region Управление окном
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) // Для того, что бы окно перетаскивать 
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    this.DragMove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "AUBU001 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "AUBU002 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "AUBU003 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        #endregion
    }
}

