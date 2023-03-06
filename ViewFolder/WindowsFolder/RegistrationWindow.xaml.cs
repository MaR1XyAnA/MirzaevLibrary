using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class RegistrationWindow : Window
    {
        string CodeString = null; // Создаём переменную для дальнейшей записи в неё и для получения из неё рандомного кода для регистрации пользователя
        
        public RegistrationWindow()
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
                    "RE001 - Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error); 
            }
        }

        private int RandomTextSender() // Метод, который генерирует рандомное число для подтверждения регистрации
        {
            Random random = new Random();
            return random.Next(1000000);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) // метод который позволит вводить только цифры от 0 до 9
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region TextChanged_PasswordChanged
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
        #endregion
        #region LayoutUpdated
        private void PasswordPaswordBox_LayoutUpdated(object sender, EventArgs e) // Постоянная проверка PasswordBox на соответствии контента
        {
            try
            {
                string PasswordText, PasswordPasword;
                PasswordText = Convert.ToString(NewPasswordTextBox.Text); 
                PasswordPasword = Convert.ToString(PasswordPaswordBox.Password);

                if (PasswordText == "")
                {
                    PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));
                }
                else if (PasswordPasword != PasswordText)
                {
                    PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58));
                }
                else
                {
                    PasswordPaswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20));
                    RegistrationButton.IsEnabled = true;
                }

                RegistrationButton.IsEnabled = !(PasswordText == "" || PasswordPasword != PasswordText);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "RE006 - Ошибка регистрации",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
        #endregion
        #region Click
        private void ConfirmButton_Click(object sender, RoutedEventArgs e) // Метод для проверки кода и конец регистрации пользователя
        {
            try
            {
                if (ConfirmTextBox.Text != CodeString)
                { 
                    MessageBox.Show("Не верный код");
                }
                else 
                {
                    EnterUser(); 
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "RE007 - Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show();
                this.Close(); 
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString(), 
                    "RE005 - Ошибка регистрации", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error); 
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e) // Метод для проверки на "пустышки"
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

                if (MessageNull != "") 
                {
                    MessageBox.Show(
                        MessageNull, 
                        "Ошибка регистрации",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error); 
                    MessageNull = null; 
                }
                else
                {
                    string MessageLack = "";
                    if (SurnameTextBox.Text.Length <= 5) MessageLack += "Фамилия не может быть меньше 5 символов\n";
                    if (NameTextBox.Text.Length <= 1) MessageLack += "Имя не может быть меньше 1 символов\n";
                    if (PhoneTextBox.Text.Length < 11) MessageLack += "Номер телефона должен быть из 11 символов\n";
                    if (NewPasswordTextBox.Text.Length <= 3) MessageLack += "Пароль не может быть меньше 3 символов, придумайте более длинный пароль";

                    if (MessageLack != "") 
                    { 
                        MessageBox.Show(
                            MessageLack, 
                            "Ошибка регистрации", 
                            MessageBoxButton.OK,
                            MessageBoxImage.Error); 
                        MessageLack = null;
                    }
                    else
                    {
                        if (AppConnectClass.DataBase.UserTable.Count(DataUser => DataUser.Login_User == EmailTextBox.Text || DataUser.Phone_User == PhoneTextBox.Text) > 0)
                        {
                            MessageBox.Show(
                                "Данный номер телефона или login уже используется",
                                "RE003 - Ошибка регистрации",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                        else
                        {
                            InformationUserStackPanel.Visibility = Visibility.Collapsed;
                            ConfirmEmailUserStackPanel.Visibility = Visibility.Visible;
                            RegistrationEmail();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "RE004 - Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        #endregion
        #region Действие
        private void RegistrationEmail() // Метод для отправки сообщение - подтверждение для регистрации (на указанную почту приходит код)
        {
            try
            {
                CodeString = RandomTextSender().ToString("D6"); // в переменную засунули случайное число для подтверждения регистрации

                string SNUser = SurnameTextBox.Text + " " + NameTextBox.Text;

                string EmailUser = Convert.ToString(EmailTextBox.Text); // почта жертвы
                string LoginMail = "orangeblood@rambler.ru"; // электронная почта приложения
                string PasswordMail = "OrangeBlood123"; // пароль почты приложения
                string smtpMail = "smtp.rambler.ru"; // smtp адресс почты приложения
                string FromWhom = "Автоматизированная информационная система 'Cool Bible Library'";
                string TopicLetters = "Подтверждение регистрации";
                string ToSend =
                    "<div style=\"position: relative; width:1120; height: 630; border:1px solid #fff; background-color: #fff;\">\r\n" +
                        "<div style=\"border-radius: 10px; position: relative; width: 780; left: 50%; right: 50%; top: 50%; bottom: 50%; height: 340; transform: translate(-50%, -50%); background-color: #232324;\">\r\n" +
                            "<p style=\"color: #e1e3e6; font-size: 40; text-align: center; font-weight:bold; padding-top: 20;\">РЕГИСТРАЦИЯ</p>\r\n" +
                            "<p style=\"color: #e1e3e6; font-size: 20; padding-top: 10; padding-left: 10px; padding-right: 10px;\">Здравствуйте " + SNUser + "! Вы запросили код для регистрации своей учётной записи в 'Cool Bible Library'</p>\r\n" +
                            "<p style=\"color: #529ef4; font-size: 40; padding-top: 15; padding-left: 50px; font-weight:bold;\">Код для завершения регистрации: " + CodeString + "</p>\r\n" +
                            "<p style=\"color: #e1e3e6; font-size: 15; padding-top: 15; padding-left: 10px; padding-right: 10px;\">Если вы этого не делали, обратитесь в службу поддержки 8 (916)-178-23-43. С уважением Cool Bible Library</p>\r\n" +
                        "</div>\r\n" +
                    "</div>"; // HTML code для сообщения

                SmtpClient mySmtpClient = new SmtpClient(smtpMail);

                mySmtpClient.UseDefaultCredentials = false;
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(LoginMail, PasswordMail);
                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress(LoginMail, FromWhom);
                MailAddress to = new MailAddress(EmailUser, TopicLetters);
                MailMessage myMail = new MailMessage(from, to);

                MailAddress replyTo = new MailAddress(LoginMail);
                myMail.ReplyToList.Add(replyTo);
               
                myMail.IsBodyHtml = true;
                myMail.Subject = TopicLetters;
                myMail.Body = ToSend;

                myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                mySmtpClient.Send(myMail);
            }
            catch (SmtpException ex) 
            { 
                throw new ApplicationException("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "RE008 - Ошибка регистрации",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        public void EnterUser() // Метод который сохраняет данные пользователя в таблицу
        {
            try
            {
                var AddUser = new UserTable()
                {
                    Surname_User = SurnameTextBox.Text,
                    Name_User = NameTextBox.Text,
                    Middlename_User = MiddlenameTextBox.Text,
                    Phone_User = PhoneTextBox.Text,
                    Login_User = EmailTextBox.Text,
                    Password_User = NewPasswordTextBox.Text,
                    pnRole_User = 1,
                    pnImage_User = 1,
                    pnTicket_User = 1
                };

                AppConnectClass.DataBase.UserTable.Add(AddUser); 
                AppConnectClass.DataBase.SaveChanges();

                MessageBox.Show(
                    "Регистрация пройдена успешно. Авторизируйтесь в приложении для работы в нём", 
                    "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show(); 
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "RE002 - Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error); 
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
                    "REBU001 - Ошибка",
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
                    "REBU002 - Ошибка",
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
                    "REBU003 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error); 
            }
        }
        #endregion
    }
}
