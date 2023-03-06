using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.Entity.Migrations;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class ForgetWindow : Window
    {
        public ForgetWindow()
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
                    "FO001 - Ошибка сброса пароля",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private int RandomTextSender() // Метод, который генерирует рандомное число для подтверждения регистрации
        {
            Random random = new Random();
            return random.Next(1000000);
        }
        #region CommonVariables
        string RandomCodeString = null; // Создаём переменную для дальнейшей записи в неё и для получения из неё рандомного кода для регистрации пользователя
        int ErrorInt = 0; // Создаём переменную для подсчёта ошибок
        string LoginUserString = null; // Создаём переменную для записи Email пользователя
        #endregion
        #region Click
        private void BackHyperlink_Click(object sender, RoutedEventArgs e) // Переход по нажатию на ссылку
        { 
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show(); 
            this.Close();
        } 

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        { 
            SaveNewPassword(); 
        }

        private void NextCodeButton_Click(object sender, RoutedEventArgs e) 
        { 
            SendTheCode(); 
        }

        private void CheckCodeButton_Click(object sender, RoutedEventArgs e) 
        {
            CheckЕheСode();
        }
        #endregion
        #region KeyDown
        private void LoginTextBox_KeyDown(object sender, KeyEventArgs e) 
        { 
            if (e.Key == Key.Enter)
            { 
                SendTheCode(); 
            } 
        }

        private void CodeTextBox_KeyDown(object sender, KeyEventArgs e) 
        { 
            if (e.Key == Key.Enter) 
            { 
                CheckЕheСode(); 
            } 
        }

        private void ReplayPasswordPasswordBox_KeyDown(object sender, KeyEventArgs e) 
        { 
            if (e.Key == Key.Enter) 
            { 
                SaveNewPassword(); 
            } 
        }
        #endregion
        #region Действие
        private void SendTheCode() // Отправка кода
        {
            try
            {
                if (LoginTextBox.Text != "")
                {
                    var SearthUser = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.Login_User == LoginTextBox.Text);
                    LoginUserString = Convert.ToString(LoginTextBox.Text);
                    if (SearthUser == null)
                    {
                        MessageBox.Show(
                            "Пользователь с логином (" + LoginUserString + ") не найден, попробуйте другой логин", 
                            "FO002 - Ошибка сброса пароля", 
                            MessageBoxButton.OK, 
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        try
                        {
                            RandomCodeString = RandomTextSender().ToString("D6"); // в переменную засунули случайное число для подтверждения регистрации

                            string SurnameUserString = SearthUser.Surname_User;
                            string NameUserString = SearthUser.Name_User;
                            string MiddlrnameUserString = SearthUser.Middlename_User;

                            string SNMUser = SurnameUserString + " " + NameUserString + " " + MiddlrnameUserString;

                            string LoginMail = "orangeblood@rambler.ru"; // электронная почта приложения
                            string PasswordMail = "OrangeBlood123"; // пароль почты приложения
                            string smtpMail = "smtp.rambler.ru"; // smtp адресс почты приложения
                            string FromWhom = "Автоматизированная информационная система 'Cool Bible Library'";
                            string TopicLetters = "Сброс пароля";
                            string ToSend =
                                "<div style=\"position: relative; width:1120; height: 630; border:1px solid #fff; background-color: #fff;\">\r\n" +
                                    "<div style=\"border-radius: 10px; position: relative; width: 780; left: 50%; right: 50%; top: 50%; bottom: 50%; height: 340; transform: translate(-50%, -50%); background-color: #232324;\">\r\n" +
                                        "<p style=\"color: #e1e3e6; font-size: 40; text-align: center; font-weight:bold; padding-top: 20;\">СБРОС ПАРОЛЯ</p>\r\n" +
                                        "<p style=\"color: #e1e3e6; font-size: 20; padding-top: 10; padding-left: 10px; padding-right: 10px;\">Здравствуйте " + SNMUser + "! Вы запросили сброс пароля для своей учётной записи в 'Cool Bible Library'</p>\r\n" +
                                        "<p style=\"color: #529ef4; font-size: 40; padding-top: 15; padding-left: 50px; font-weight:bold;\">Код для сброса пароля: " + RandomCodeString + "</p>\r\n" +
                                        "<p style=\"color: #e1e3e6; font-size: 15; padding-top: 15; padding-left: 10px; padding-right: 10px;\">Если вы этого не делали, обратитесь в службу поддержки 8 (916)-178-23-43. С уважением Cool Bible Library</p>\r\n" +
                                    "</div>\r\n" +
                                "</div>"; // HTML code для сообщения

                            SmtpClient mySmtpClient = new SmtpClient(smtpMail);

                            mySmtpClient.UseDefaultCredentials = false;
                            NetworkCredential basicAuthenticationInfo = new NetworkCredential(LoginMail, PasswordMail);
                            mySmtpClient.Credentials = basicAuthenticationInfo;

                            MailAddress from = new MailAddress(LoginMail, FromWhom);
                            MailAddress to = new MailAddress(LoginUserString, TopicLetters);
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
                            MessageBox.Show
                                (ex.Message.ToString(),
                                "FO004 - Ошибка сброса пароля",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            ErrorInt++; 
                        }

                        if (ErrorInt == 0) // Если ошибок нет
                        {
                            CodeGrid.Visibility = Visibility.Visible;
                            NextCodeButton.Visibility = Visibility.Collapsed;
                            LoginTextBox.IsEnabled = false;
                            CheckCodeButton.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "FO003 - Ошибка сброса пароля",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error); 
            }
        }

        private void SaveNewPassword() // Сохранение пароля
        {
            try
            {
                string MessageLack = "";
                if (NewPasswordTextBox.Text.Length <= 3)
                    MessageLack += "Пароль не может быть меньше 3 символов, придумайте более длинный пароль";

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
                    if (NewPasswordTextBox.Text != "" && ReplayPasswordPasswordBox.Password != "")
                    {
                        var UpdatePasswordUser = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.Login_User == LoginUserString);

                        if (UpdatePasswordUser != null)
                        {
                            try
                            {
                                    UpdatePasswordUser.Password_User = NewPasswordTextBox.Text;
                                AppConnectClass.DataBase.UserTable.AddOrUpdate(UpdatePasswordUser);
                                AppConnectClass.DataBase.SaveChanges();

                                MessageBox.Show(
                                    "Пароль был успешно сменён. Авторизируйтесь в приложении для работы в нём.",
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
                                    "FO007 - Ошибка сброса пароля",
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Error); 
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                "Пользователь не найден", 
                                "FO008 - Ошибка сброса пароля", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex) 
            { MessageBox.Show(
                ex.Message.ToString(), 
                "FO009 - Ошибка сброса пароля", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error); 
            }
        }

        private void CheckЕheСode() // Проверка, совподает ли код который ввел пользователь, с тем, что отправленно
        {
            try
            {
                if (CodeTextBox.Text != RandomCodeString)
                {
                    MessageBox.Show(
                        "Код введён не правильно",
                        "FO006 - Ошибка сброса пароля",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    CodeGrid.Visibility = Visibility.Collapsed;
                    LoginGrid.Visibility = Visibility.Collapsed;
                    NextCodeButton.Visibility = Visibility.Collapsed;
                    CheckCodeButton.Visibility = Visibility.Collapsed;

                    NewPasswordGrid.Visibility = Visibility.Visible;
                    ReplayPasswordGrid.Visibility = Visibility.Visible;
                    SavePasswordButton.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "FO005 - Ошибка сброса пароля",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
        #endregion
        #region LayoutUpdated
        private void ReplayPasswordPasswordBox_LayoutUpdated(object sender, EventArgs e)
        {
            try
            {
                string PasswordText, PasswordPasword;
                PasswordText = Convert.ToString(NewPasswordTextBox.Text);
                PasswordPasword = Convert.ToString(ReplayPasswordPasswordBox.Password);

                if (PasswordText == "")
                {
                    ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));
                }
                else if (PasswordPasword != PasswordText)
                {
                    ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58));
                }
                else
                {
                    ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20));
                    SavePasswordButton.IsEnabled = true;
                }

                SavePasswordButton.IsEnabled = !(PasswordText == "" || PasswordPasword != PasswordText);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                ex.Message.ToString(), 
                "FO006 - Ошибка регистрации", 
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }
        #endregion
        #region TextChanged_PasswordChanged
        private void ReplayPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ReplayPasswordPasswordBox.Password.Length > 0) 
            { 
                SavePasswordButton.IsEnabled = true;
                HintReplayPasswordTextBlock.Visibility = Visibility.Collapsed; 
            }
            else 
            { 
                HintReplayPasswordTextBlock.Visibility = Visibility.Visible; 
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
                HintReplayPasswordTextBlock.Visibility = Visibility.Visible; 
            }
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 0) 
            {
                NextCodeButton.IsEnabled = true; 
                HintLoginTextBlock.Visibility = Visibility.Collapsed;
            }
            else 
            { 
                NextCodeButton.IsEnabled = false; 
                HintLoginTextBlock.Visibility = Visibility.Visible; 
            }
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CodeTextBox.Text.Length > 0)
            {
                CheckCodeButton.IsEnabled = true; 
                HintCodeTextBlock.Visibility = Visibility.Collapsed; 
            }
            else 
            { 
                CheckCodeButton.IsEnabled = false; 
                HintCodeTextBlock.Visibility = Visibility.Visible;
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
                    "FOBU001 - Ошибка",
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
                    "FOBU002 - Ошибка",
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
                    "FOBU003 - Ошибка",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
