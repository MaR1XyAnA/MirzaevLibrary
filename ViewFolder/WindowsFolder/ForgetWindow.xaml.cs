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
        #region CommonVariables
        string RandomCodeString = null; // Создаём переменную для дальнейшей записи в неё и для получения из неё рандомного кода для регистрации пользователя
        int ErrorInt = 0; // Создаём переменную для подсчёта ошибок
        string LoginUserString = null; // Создаём переменную для записи Email пользователя
        #endregion

        private int RandomTextSender() { Random random = new Random(); return random.Next(1000000); } // Метод, который генерирует рандомное число для подтверждения регистрации

        public ForgetWindow()
        {
            try { InitializeComponent(); AppConnectClass.DataBase = new LibraryMirzayevaEntities(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO001 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

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

        #region События кнопок
        private void BackHyperlink_Click(object sender, RoutedEventArgs e) { AuthorizationWindow authorizationWindow = new AuthorizationWindow(); authorizationWindow.Show(); this.Close(); } // Переход по нажатию на ссылку

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e) { SaveNewPassword(); }

        private void NextCodeButton_Click(object sender, RoutedEventArgs e) { SendTheCode(); }

        private void CheckCodeButton_Click(object sender, RoutedEventArgs e) { CheckЕheСode(); } 
        #endregion

        #region События при нажатии на Enter
        private void LoginTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { SendTheCode(); } }

        private void CodeTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { CheckЕheСode(); } }

        private void ReplayPasswordPasswordBox_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) { SaveNewPassword(); } }
        #endregion

        #region Действие
        private void SendTheCode() // Отправка кода
        {
            try
            {
                if (LoginTextBox.Text != "")
                {
                    var SearthUser = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.LoginUser == LoginTextBox.Text);
                    LoginUserString = Convert.ToString(LoginTextBox.Text);
                    if (SearthUser == null)
                    {
                        MessageBox.Show("Пользователь с логином (" + LoginUserString + ") не найден, попробуйте другой логин", "FO002 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        try
                        {
                            RandomCodeString = RandomTextSender().ToString("D6"); // в переменную засунули случайное число для подтверждения регистрации

                            string LoginMail = "orangeblood@rambler.ru"; // электронная почта приложения
                            string PasswordMail = "OrangeBlood123"; // пароль почты приложения
                            string smtpMail = "smtp.rambler.ru"; // smtp адресс почты приложения
                            string FromWhom = "Автоматизированная информационная система 'Cool Bible Library'";
                            string TopicLetters = "Подтверждение регистрации";
                            string ToSend = RandomCodeString;

                            SmtpClient mySmtpClient = new SmtpClient(smtpMail);

                            mySmtpClient.UseDefaultCredentials = false;
                            NetworkCredential basicAuthenticationInfo = new NetworkCredential(LoginMail, PasswordMail);
                            mySmtpClient.Credentials = basicAuthenticationInfo;

                            MailAddress from = new MailAddress(LoginMail, FromWhom);
                            MailAddress to = new MailAddress(LoginUserString, TopicLetters);
                            MailMessage myMail = new MailMessage(from, to);

                            MailAddress replyTo = new MailAddress(LoginMail);
                            myMail.ReplyToList.Add(replyTo);

                            myMail.Subject = TopicLetters;
                            myMail.Body = ToSend;

                            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                            myMail.BodyEncoding = System.Text.Encoding.UTF8;

                            mySmtpClient.Send(myMail);
                        }
                        catch (SmtpException ex) { throw new ApplicationException("SmtpException has occured: " + ex.Message); }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO004 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); ErrorInt++; }

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
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO003 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void SaveNewPassword() // Сохранение пароля
        {
            try
            {
                if (NewPasswordTextBox.Text != "" && ReplayPasswordPasswordBox.Password != "")
                {
                    var UserVar = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.LoginUser == LoginUserString);
                    if (UserVar != null)
                    {
                        var userTable = new UserTable()
                        {
                            PersonalNumberUser = UserVar.PersonalNumberUser,
                            SurnameUser = UserVar.SurnameUser,
                            NameUser = UserVar.NameUser,
                            MiddlenameUser = UserVar.MiddlenameUser,
                            pnTicketUser = UserVar.pnTicketUser,
                            AddressUser = UserVar.AddressUser,
                            PhoneUser = UserVar.PhoneUser,
                            LoginUser = UserVar.LoginUser,
                            PasswordUser = NewPasswordTextBox.Text,
                            pnRoleUser = UserVar.pnRoleUser,
                            pnImageUser = UserVar.pnImageUser
                        };

                        try
                        {
                            AppConnectClass.DataBase.UserTable.AddOrUpdate(userTable);
                            AppConnectClass.DataBase.SaveChanges();
                            MessageBox.Show("Пароль был успешно сменён. Авторизируйтесь в приложении для работы в нём.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AuthorizationWindow authorizationWindow = new AuthorizationWindow(); authorizationWindow.Show(); this.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO007 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден", "FO008 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO009 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CheckЕheСode() // Проверка, совподает ли код который ввел пользователь, с тем, что отправленно
        {
            try
            {
                if (CodeTextBox.Text != RandomCodeString)
                {
                    MessageBox.Show("Код введён не правильно", "FO006 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Information);
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
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO005 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        #region Changed
        private void ReplayPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ReplayPasswordPasswordBox.Password.Length > 0)
            {
                HintReplayPasswordTextBlock.Visibility = Visibility.Collapsed;

                string PasswordText, PasswordPasword;
                PasswordText = Convert.ToString(NewPasswordTextBox.Text); PasswordPasword = Convert.ToString(ReplayPasswordPasswordBox.Password);

                if (PasswordPasword == "" && PasswordText != "") { ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63)); SavePasswordButton.IsEnabled = false; }
                else
                {
                    if (PasswordText == "") { ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63)); SavePasswordButton.IsEnabled = false; }
                    else
                    {
                        if (PasswordPasword != PasswordText) { ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58)); SavePasswordButton.IsEnabled = false; }
                        if (PasswordPasword == PasswordText) { ReplayPasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20)); SavePasswordButton.IsEnabled = true; }
                    }
                }
            }
            else { ReplayPasswordPasswordBox.IsEnabled = false; HintNewPasswordTextBlock.Visibility = Visibility.Visible; }
        }

        private void NewPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewPasswordTextBox.Text.Length > 0) { ReplayPasswordPasswordBox.IsEnabled = true; HintNewPasswordTextBlock.Visibility = Visibility.Collapsed; }
            else { ReplayPasswordPasswordBox.IsEnabled = false; HintNewPasswordTextBlock.Visibility = Visibility.Visible; }
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 0) { NextCodeButton.IsEnabled = true; HintLoginTextBlock.Visibility = Visibility.Collapsed; }
            else { NextCodeButton.IsEnabled = false; HintLoginTextBlock.Visibility = Visibility.Visible; }
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CodeTextBox.Text.Length > 0) { CheckCodeButton.IsEnabled = true; HintCodeTextBlock.Visibility = Visibility.Collapsed; }
            else { CheckCodeButton.IsEnabled = false; HintCodeTextBlock.Visibility = Visibility.Visible; }
        }
        #endregion
    }
}
