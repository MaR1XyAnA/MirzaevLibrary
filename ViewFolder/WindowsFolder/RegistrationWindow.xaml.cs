using System;
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
