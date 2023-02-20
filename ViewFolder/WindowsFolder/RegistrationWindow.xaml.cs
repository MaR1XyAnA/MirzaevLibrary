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
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            AppConnectClass.DataBase = new LibraryMirzayevaEntities();
        }

        private void SurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SurnameTextBox.Text.Length > 0)
            {
                HintSurnameTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintSurnameTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text.Length > 0)
            {
                HintNameTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintNameTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void MiddlenameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MiddlenameTextBox.Text.Length > 0)
            {
                HintMiddlenameTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintMiddlenameTextBlock.Visibility = Visibility.Visible;
            }
        }

        public void EnterUser()
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
                message += "Поле с ФАМИЛИЕЙ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                message += "Поле с ИМЯНЕМ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                message += "Поле с ЭЛЕКТРОННОЙ ПОЧТОЙ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                message += "Поле с НОМЕРОМ ТЕЛЕФОНА не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                message += "Поле с ПАРОЛЕМ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                message += "Поле с ПОВТОРНОМ ПАРОЛЕМ не должно быть пустым.";
            if (message != "")
            {
                MessageBox.Show(message, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (AppConnectClass.DataBase.UserTable.Count(DataUser => DataUser.LoginUser == EmailTextBox.Text || DataUser.PhoneUser == PhoneTextBox.Text) > 0)
                {
                    MessageBox.Show("Данный номер телефона или login уже используется", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var UserTable = new UserTable
                    {
                        SurnameUser = SurnameTextBox.Text,
                        NameUser = NameTextBox.Text,
                        MiddlenameUser = MiddlenameTextBox.Text,
                        PhoneUser = PhoneTextBox.Text,
                        LoginUser = EmailTextBox.Text,
                        PasswordUser = PhoneTextBox.Text,
                        pnRoleUser = 1,
                        pnImageUser = 1
                    };
                    try
                    {
                        AppConnectClass.DataBase.UserTable.Add(UserTable);
                        MessageBox.Show("Регистрация пройдена успешно. Авторизируйтесь в приложении для работы в нём", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmailTextBox.Text.Length > 0)
            {
                HintEmailTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintEmailTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PhoneTextBox.Text.Length > 0)
            {
                HintPhoneTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintPhoneTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void NewPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewPasswordTextBox.Text.Length > 0)
            {
                HintNewPasswordTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintNewPasswordTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void PasswordPaswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPaswordBox.Password.Length > 0)
            {
                HintPasswordTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                HintPasswordTextBlock.Visibility = Visibility.Visible;
            }
            
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }

        private void PasswordPaswordBox_LayoutUpdated(object sender, EventArgs e)
        {
            string PasswordText, PasswordPasword;
            PasswordText = Convert.ToString(NewPasswordTextBox.Text);
            PasswordPasword = Convert.ToString(PasswordPaswordBox.Password);
            if (PasswordText == "")
            {
                PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));

                RegistrationButton.IsEnabled = false;
                RegistrationButton.Background = new SolidColorBrush(Color.FromRgb(153, 154, 156));
                RegistrationButton.BorderBrush = new SolidColorBrush(Color.FromRgb(153, 154, 156));
            }
            else
            {
                if (PasswordPasword != PasswordText)
                {
                    PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(217, 34, 0));

                    RegistrationButton.IsEnabled = false;
                    RegistrationButton.Background = new SolidColorBrush(Color.FromRgb(153, 154, 156));
                    RegistrationButton.BorderBrush = new SolidColorBrush(Color.FromRgb(153, 154, 156));
                }
                if (PasswordPasword == PasswordText)
                {
                    PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(133, 178, 43));

                    RegistrationButton.IsEnabled = true;
                    RegistrationButton.Background = new SolidColorBrush(Color.FromRgb(255, 227, 230));
                    RegistrationButton.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 227, 230));
                }
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
    }
}
