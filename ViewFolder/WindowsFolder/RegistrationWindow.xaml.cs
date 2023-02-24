using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            try { InitializeComponent(); AppConnectClass.DataBase = new LibraryMirzayevaEntities(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void SurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SurnameTextBox.Text.Length > 0) { HintSurnameTextBlock.Visibility = Visibility.Collapsed; }
            else { HintSurnameTextBlock.Visibility = Visibility.Visible; }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text.Length > 0) { HintNameTextBlock.Visibility = Visibility.Collapsed; }
            else { HintNameTextBlock.Visibility = Visibility.Visible; }
        }

        private void MiddlenameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MiddlenameTextBox.Text.Length > 0) { HintMiddlenameTextBlock.Visibility = Visibility.Collapsed;}
            else { HintMiddlenameTextBlock.Visibility = Visibility.Visible; }
        }

        public void  EnterUser()
        {
            if (AppConnectClass.DataBase.UserTable.Count(DataUser => DataUser.LoginUser == EmailTextBox.Text || DataUser.PhoneUser == PhoneTextBox.Text) > 0)
            {
                MessageBox.Show("Данный номер телефона или login уже используется", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var AddUser = new UserTable()
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
                    AppConnectClass.DataBase.UserTable.Add(AddUser); AppConnectClass.DataBase.SaveChanges();
                    MessageBox.Show("Регистрация пройдена успешно. Авторизируйтесь в приложении для работы в нём", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AuthorizationWindow authorizationWindow = new AuthorizationWindow(); authorizationWindow.Show(); this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error); }
            }

        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmailTextBox.Text.Length > 0) { HintEmailTextBlock.Visibility = Visibility.Collapsed; }
            else { HintEmailTextBlock.Visibility = Visibility.Visible; }
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PhoneTextBox.Text.Length > 0) { HintPhoneTextBlock.Visibility = Visibility.Collapsed; }
            else { HintPhoneTextBlock.Visibility = Visibility.Visible; }
        }

        private void NewPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewPasswordTextBox.Text.Length > 0) { HintNewPasswordTextBlock.Visibility = Visibility.Collapsed; }
            else { HintNewPasswordTextBlock.Visibility = Visibility.Visible; }
        }

        private void PasswordPaswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPaswordBox.Password.Length > 0) { HintPasswordTextBlock.Visibility = Visibility.Collapsed; }
            else { HintPasswordTextBlock.Visibility = Visibility.Visible; }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string MessageNull = "";
                if (string.IsNullOrWhiteSpace(SurnameTextBox.Text)) MessageNull += "Поле с ФАМИЛИЕЙ не должно быть пустым \n";
                if (string.IsNullOrWhiteSpace(NameTextBox.Text)) MessageNull += "Поле с ИМЯНЕМ не должно быть пустым \n";
                if (string.IsNullOrWhiteSpace(EmailTextBox.Text)) MessageNull += "Поле с ЭЛЕКТРОННОЙ ПОЧТОЙ не должно быть пустым \n";
                if (string.IsNullOrWhiteSpace(PhoneTextBox.Text)) MessageNull += "Поле с НОМЕРОМ ТЕЛЕФОНА не должно быть пустым \n";
                if (string.IsNullOrWhiteSpace(NewPasswordTextBox.Text)) MessageNull += "Поле с ПАРОЛЕМ не должно быть пустым \n";
                if (string.IsNullOrWhiteSpace(PasswordPaswordBox.Password)) MessageNull += "Поле с ПОВТОРНОМ ПАРОЛЕМ не должно быть пустым.";

                if (MessageNull != "") { MessageBox.Show(MessageNull, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error); MessageNull = null; }
                else
                {
                    string MessageLack = "";
                    if (SurnameTextBox.Text.Length <= 5) MessageLack += "Фамилия не может быть меньше 5 символов\n";
                    if (NameTextBox.Text.Length <= 1) MessageLack += "Имя не может быть меньше 1 символов\n";
                    if (PhoneTextBox.Text.Length < 11) MessageLack += "Номер телефона должен быть из 11 символов\n";
                    if (NewPasswordTextBox.Text.Length <= 3) MessageLack += "Пароль не может быть меньше 3 символов, придумайте более длинный пароль";

                    if (MessageLack != "") { MessageBox.Show(MessageLack, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error); MessageLack = null; }
                    else { EnterUser(); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e) { AuthorizationWindow authorizationWindow = new AuthorizationWindow(); authorizationWindow.Show(); this.Close(); }

        private void PasswordPaswordBox_LayoutUpdated(object sender, EventArgs e)
        {
            string PasswordText, PasswordPasword;
            PasswordText = Convert.ToString(NewPasswordTextBox.Text); PasswordPasword = Convert.ToString(PasswordPaswordBox.Password);
            if (PasswordText == "")
            {
                PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));
                RegistrationButton.IsEnabled = false;
            }
            else
            {
                if (PasswordPasword != PasswordText) { RegistrationButton.IsEnabled = false; }
                if (PasswordPasword == PasswordText) { RegistrationButton.IsEnabled = true; }
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) { Regex regex = new Regex("[^0-9]"); e.Handled = regex.IsMatch(e.Text); }
        #region Управление окном
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }} // Для того, что бы окно перетаскивать 

        private void CloseButton_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }

        private void RollUpButton_Click(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }
        #endregion
    }
}
