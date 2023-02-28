using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MirzaevLibrary.ViewFolder.WindowsFolder;
using System.Data.Entity.Migrations;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class ProfilePage : Page
    {
        public ProfilePage(UserTable GetUserTable)
        {
            try
            {
                InitializeComponent();
                if (GetUserTable != null) // Если передоваемое значение не пустое
                {
                    AppConnectClass.DataBase = new LibraryMirzayevaEntities(); DataContext = GetUserTable;
                    if (UserClass.GetUserTable.pnTicketUser == 1) // Если у пользователя стандартный номер читательского билеиа
                    {
                        InfoTicketThoTextBlock.Text = "У вас нет читательского билета, но вы можите его преобрести";
                        InfoTicketOneTextBlock.Visibility = Visibility.Collapsed;
                        HistoryBookListBox.Visibility = Visibility.Collapsed;
                        HintHistoryTextBlock.Visibility = Visibility.Visible;
                        HintHistoryTextBlock.Text = "У вас нет читательского билета";
                    }
                    else // Если у пользователя не стандартный читательский билет
                    {
                        int IdTicket = UserClass.GetUserTable.pnTicketUser; // Получаем номер читательского билета
                        AppConnectClass.DataBase.TicketTable.Include(Data => Data.BookTable).Load(); //Реализовываем связь многие ко многим между таблицей TicketTable и таблицей BookTable
                        var Ticket = AppConnectClass.DataBase.TicketTable.Find(IdTicket); // Ищем в таблице TicketTable читательский билет по номеру

                        HistoryBookListBox.ItemsSource = Ticket.BookTable.ToList();
                        HintHistoryTextBlock.Visibility = Visibility.Collapsed;
                    }
                }
                else // Если зашёл не авторизированный пользователь
                {
                    ImageSource userImage = new BitmapImage(new Uri("/ContentFolder/ImageFolder/NoImage.png", UriKind.RelativeOrAbsolute)); // Вывод стандартного фото
                    UserImage.Source = userImage;
                    HintInfoTicketTextBlock.Visibility = Visibility.Visible;
                    InfoTicketOneTextBlock.Visibility = Visibility.Collapsed;
                    InfoTicketThoTextBlock.Visibility = Visibility.Collapsed;
                    EditProfilButton.Visibility = Visibility.Collapsed;
                    AuthorizationButton.Visibility = Visibility.Visible;
                    RegistrationButton.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "PR001 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void EditProfilButton_Click(object sender, RoutedEventArgs e)
        {
            SurnameTextBox.IsEnabled = true;
            NameTextBox.IsEnabled = true;
            MiddlenameTextBox.IsEnabled = true;
            AddresTextBox.IsEnabled = true;
            PhoneTextBox.IsEnabled = true;

            EditPasswordButton.Visibility = Visibility.Visible;
            SaveProfilButton.Visibility = Visibility.Visible;
            EditProfilButton.Visibility = Visibility.Collapsed;
        }

        private void EditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            SurnameStackPanel.Visibility = Visibility.Collapsed;
            NameStackPanel.Visibility = Visibility.Collapsed;
            MiddlenameStackPanel.Visibility = Visibility.Collapsed;
            AddresStackPanel.Visibility = Visibility.Collapsed;
            PhoneStackPanel.Visibility = Visibility.Collapsed;

            OldPasswordStackPanel.Visibility = Visibility.Visible;
            NewPasswordStackPanel.Visibility = Visibility.Visible;
            PasswordStackPanel.Visibility = Visibility.Visible;

            EditPasswordButton.Visibility = Visibility.Collapsed;
            SaveProfilButton.Visibility = Visibility.Collapsed;
            SavePasswordButton.Visibility = Visibility.Visible;
        }

        private void PasswordPasswordBox_LayoutUpdated(object sender, EventArgs e) // Постоянная проверка PasswordBox на соответствии контента
        {
            string PasswordText, PasswordPasword;
            PasswordText = Convert.ToString(NewPasswordTextBox.Text); PasswordPasword = Convert.ToString(PasswordPasswordBox.Password);
            if (PasswordPasword == "" && PasswordText != "") 
            { 
                PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58));
                SavePasswordButton.IsEnabled = false;
            }
            else
            {
                if (PasswordText == "") 
                {
                    PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));
                    SavePasswordButton.IsEnabled = false; 
                }
                else
                {
                    if (PasswordPasword != PasswordText)
                    {
                        PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58));
                        SavePasswordButton.IsEnabled = false;
                    }
                    if (PasswordPasword == PasswordText)
                    {
                        PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20));
                        SavePasswordButton.IsEnabled = true;
                    }
                }
            }
        }

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveProfilButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserClass.GetUserTable == null)
                {
                    MessageBox.Show("Вам нужно сначала авторизоваться", "SA001 - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UserTable userTable = new UserTable()
                    {
                        PersonalNumberUser = UserClass.GetUserTable.PersonalNumberUser,
                        SurnameUser = SurnameTextBox.Text,
                        NameUser = NameTextBox.Text,
                        MiddlenameUser = MiddlenameTextBox.Text,
                        AddressUser = AddresTextBox.Text,
                        PhoneUser = PhoneTextBox.Text,
                        pnTicketUser = UserClass.GetUserTable.pnTicketUser,
                        LoginUser = UserClass.GetUserTable.LoginUser,
                        PasswordUser = UserClass.GetUserTable.PasswordUser,
                        pnRoleUser = UserClass.GetUserTable.pnRoleUser,
                        pnImageUser = UserClass.GetUserTable.pnImageUser
                    };

                    AppConnectClass.DataBase.UserTable.AddOrUpdate(userTable); AppConnectClass.DataBase.SaveChanges();
                    MessageBox.Show("Данные сохранены успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    SurnameTextBox.IsEnabled = false;
                    NameTextBox.IsEnabled = false;
                    MiddlenameTextBox.IsEnabled = false;
                    AddresTextBox.IsEnabled = false;
                    PhoneTextBox.IsEnabled = false;

                    EditPasswordButton.Visibility = Visibility.Collapsed;
                    SaveProfilButton.Visibility = Visibility.Collapsed;
                    EditProfilButton.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "SA002 - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e) { AuthorizationWindow authorizationWindow = new AuthorizationWindow(); authorizationWindow.Show(); }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e) { RegistrationWindow registrationWindow = new RegistrationWindow(); registrationWindow.Show(); }
    }
}
