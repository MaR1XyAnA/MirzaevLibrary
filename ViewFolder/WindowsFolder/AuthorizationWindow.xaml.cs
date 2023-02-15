using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

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
                RegistrationButton.IsEnabled = true;
                NextButton.Background = new SolidColorBrush(Color.FromRgb(255, 227, 230));
                NextButton.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 227, 230));
            }
        }
    }
}

