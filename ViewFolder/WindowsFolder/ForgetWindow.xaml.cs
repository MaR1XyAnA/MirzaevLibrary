using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class ForgetWindow : Window
    {
        string RandomCodeString = null; // Создаём переменную для дальнейшей записи в неё и для получения из неё рандомного кода для регистрации пользователя

        private int RandomTextSender() { Random random = new Random(); return random.Next(1000000); } // Метод, который генерирует рандомное число для подтверждения регистрации

        public ForgetWindow()
        {
            try { InitializeComponent(); AppConnectClass.DataBase = new LibraryMirzayevaEntities(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO001 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 0) { NextCodeButton.IsEnabled = false; HintLoginTextBlock.Visibility = Visibility.Collapsed; }
            else { NextCodeButton.IsEnabled = true; HintLoginTextBlock.Visibility = Visibility.Visible; }
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

        private void NextCodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginTextBox.Text != "")
                {
                    var SearthUser = AppConnectClass.DataBase.UserTable.FirstOrDefault(data => data.LoginUser == LoginTextBox.Text);
                    string LoginUserString = Convert.ToString(LoginTextBox.Text);
                    if (SearthUser == null)
                    {
                        MessageBox.Show("Пользователь с логином (" + LoginUserString + ") не найден, попробуйте другой логин", "FO002 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        CodeGrid.Visibility = Visibility.Visible;
                        NextCodeButton.Visibility = Visibility.Collapsed;
                        CheckCodeButton.Visibility = Visibility.Visible;
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
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO004 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO003 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CheckCodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CodeTextBox.Text != RandomCodeString)
                {
                    MessageBox.Show("Код введён не правильно", "FO006 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "FO005 - Ошибка сброса пароля", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
