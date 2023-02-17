using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class AuthorizationWindow : Window
    {
        public static UserClass userClass;
        public AuthorizationWindow()
        {
            InitializeComponent();
            try
            {
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "E001", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void EnterUser()
        {
            try
            {
                var GetUser = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.LoginUser == LoginTextBox.Text && data.PasswordUser == PasswordPasswordBox.Password);
                if (GetUser != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();

                    userClass.PersonalNumberUser = GetUser.PersonalNumberUser;
                    userClass.SurnameUser = GetUser.SurnameUser;
                    userClass.NameUser = GetUser.NameUser;
                    userClass.MiddlenameUser = GetUser.MiddlenameUser;
                    userClass.pnTicketUser = GetUser.pnTicketUser;
                    userClass.AddressUser = GetUser.AddressUser;
                    userClass.PhoneUser = GetUser.PhoneUser;
                    userClass.LoginUser = GetUser.LoginUser;
                    userClass.PasswordUser = GetUser.PasswordUser;
                    userClass.pnRoleUser = GetUser.pnRoleUser;
                    userClass.pnImageUser = GetUser.pnImageUser;
                }
                else
                {
                    MessageBox.Show("Не правильный логин или пароль", "Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void VisiblePasswordUserButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Collapsed;
            VisiblePasswordGrid.Visibility = Visibility.Visible;
            string PasswordString = Convert.ToString(PasswordPasswordBox.Password);
            PasswordTextBox.Text = PasswordString;
        }

        private void VisiblePasswordUserButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedPasswordGrid.Visibility = Visibility.Visible;
            VisiblePasswordGrid.Visibility = Visibility.Collapsed;
            string PasswordString = Convert.ToString(PasswordTextBox.Text);
            PasswordPasswordBox.Password = PasswordString;
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

        #region Управление окном
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) // Для того, что бы окно перетаскивать
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion

        private void NextButton_LayoutUpdated(object sender, EventArgs e)
        {
            string LoginString, PassworPasswordString;
            LoginString = Convert.ToString(LoginTextBox.Text);
            PassworPasswordString = Convert.ToString(PasswordPasswordBox.Password);
            if (PassworPasswordString == "" || LoginString == "")
            {
                NextButton.IsEnabled = false;
                NextButton.Background = new SolidColorBrush(Color.FromRgb(153, 154, 156));
                NextButton.BorderBrush = new SolidColorBrush(Color.FromRgb(153, 154, 156));
            }
            else
            {
                NextButton.IsEnabled = true;
                NextButton.Background = new SolidColorBrush(Color.FromRgb(255, 227, 230));
                NextButton.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 227, 230));
            }
        }
    }
}

