using MirzaevLibrary.ViewFolder.WindowsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IgnoreAutborizationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {

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
            if (LoginTextBox.Text.Length >0)
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
    }
}
