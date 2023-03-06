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

                    if (UserClass.GetUserTable.pnTicket_User == 1) // Если у пользователя стандартный номер читательского билета
                    {
                        InfoTicketThoTextBlock.Text = "У вас нет читательского билета, но вы можете его преобрести";
                        InfoTicketOneTextBlock.Visibility = Visibility.Collapsed;
                        HistoryBookListBox.Visibility = Visibility.Collapsed;
                        HintHistoryTextBlock.Visibility = Visibility.Visible;
                        HintHistoryTextBlock.Text = "У вас нет читательского билета";
                    }
                    else // Если у пользователя не стандартный читательский билет
                    {
                        int IdTicket = UserClass.GetUserTable.pnTicket_User; // Получаем номер читательского билета
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
            catch (Exception ex) 
            {
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "PR001 - Ошибка авторизации", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
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

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserClass.GetUserTable == null)
                {
                    MessageBox.Show(
                        "Вам нужно сначала авторизоваться", 
                        "PR002 - Ошибка сохранения", 
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    var UserVar = UserClass.GetUserTable;
                    UserVar.Password_User = NewPasswordTextBox.Text;
                    //UserTable PasswordUser = new UserTable()
                    //{
                    //    PersonalNumber_User = UserClass.GetUserTable.PersonalNumber_User,
                    //    Surname_User = UserClass.GetUserTable.Surname_User,
                    //    Name_User = UserClass.GetUserTable.Name_User,
                    //    Middlename_User = UserClass.GetUserTable.Middlename_User,
                    //    Address_User = UserClass.GetUserTable.Address_User,
                    //    Phone_User = UserClass.GetUserTable.Phone_User,
                    //    pnTicket_User = UserClass.GetUserTable.pnTicket_User,
                    //    Login_User = UserClass.GetUserTable.Login_User,
                    //    Password_User = NewPasswordTextBox.Text,
                    //    pnRole_User = UserClass.GetUserTable.pnRole_User,
                    //    pnImage_User = UserClass.GetUserTable.pnImage_User
                    //};

                    AppConnectClass.DataBase.UserTable.AddOrUpdate(UserVar); AppConnectClass.DataBase.SaveChanges();
                    MessageBox.Show("Пароль сменён успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    SurnameStackPanel.Visibility = Visibility.Visible;
                    NameStackPanel.Visibility = Visibility.Visible;
                    MiddlenameStackPanel.Visibility = Visibility.Visible;
                    AddresStackPanel.Visibility = Visibility.Visible;
                    PhoneStackPanel.Visibility = Visibility.Visible;

                    OldPasswordStackPanel.Visibility = Visibility.Collapsed;
                    NewPasswordStackPanel.Visibility = Visibility.Collapsed;
                    PasswordStackPanel.Visibility = Visibility.Collapsed;

                    SurnameTextBox.IsEnabled = false;
                    NameTextBox.IsEnabled = false;
                    MiddlenameTextBox.IsEnabled = false;
                    AddresTextBox.IsEnabled = false;
                    PhoneTextBox.IsEnabled = false;

                    EditPasswordButton.Visibility = Visibility.Collapsed;
                    SaveProfilButton.Visibility = Visibility.Collapsed;
                    SavePasswordButton.Visibility = Visibility.Collapsed;
                    EditProfilButton.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(),
                    "PR003 - Ошибка сохранения", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error); 
            }
        }

        private void SaveProfilButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserClass.GetUserTable == null)
                {
                    MessageBox.Show(
                        "Вам нужно сначала авторизоваться", 
                        "PR004 - Ошибка сохранения",
                        MessageBoxButton.OK, 
                        MessageBoxImage.Information);
                }
                else
                {
                    if (NewPasswordTextBox.Text == UserClass.GetUserTable.Password_User || PasswordPasswordBox.Password == UserClass.GetUserTable.Password_User)
                    {
                        MessageBox.Show(
                            "Вы уже используете данный пароль",
                            "PR005 - ошибка сохранения",
                            MessageBoxButton.OK, 
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        var UpDateUser = UserClass.GetUserTable;
                        UpDateUser.Surname_User = SurnameTextBox.Text;
                        UpDateUser.Name_User = NameTextBox.Text;
                        UpDateUser.Surname_User = MiddlenameTextBox.Text;
                        UpDateUser.Address_User = AddresTextBox.Text;
                        UpDateUser.Phone_User = PhoneTextBox.Text;
                        //UserTable EditUser = new UserTable()
                        //{
                        //    PersonalNumberUser = UserClass.GetUserTable.PersonalNumberUser,
                        //    SurnameUser = SurnameTextBox.Text,
                        //    NameUser = NameTextBox.Text,
                        //    MiddlenameUser = MiddlenameTextBox.Text,
                        //    AddressUser = AddresTextBox.Text,
                        //    PhoneUser = PhoneTextBox.Text,
                        //    pnTicketUser = UserClass.GetUserTable.pnTicketUser,
                        //    LoginUser = UserClass.GetUserTable.LoginUser,
                        //    PasswordUser = UserClass.GetUserTable.PasswordUser,
                        //    pnRoleUser = UserClass.GetUserTable.pnRoleUser,
                        //    pnImageUser = UserClass.GetUserTable.pnImageUser
                        //};

                        AppConnectClass.DataBase.UserTable.AddOrUpdate(UpDateUser);
                        AppConnectClass.DataBase.SaveChanges();

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

            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "PR006 - Ошибка сохранения",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e) 
        { 
            AuthorizationWindow authorizationWindow = new AuthorizationWindow(); 
            authorizationWindow.Show(); 
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e) 
        { 
            RegistrationWindow registrationWindow = new RegistrationWindow(); 
            registrationWindow.Show();
        }

        private void OldPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (OldPasswordTextBox.Text == "") 
                { 
                    OldPasswordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63)); 
                    NewPasswordTextBox.IsEnabled = false; 
                    PasswordPasswordBox.IsEnabled = false; 
                }
                else
                {
                    if (OldPasswordTextBox.Text != UserClass.GetUserTable.Password_User) 
                    { 
                        OldPasswordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58)); 
                        NewPasswordTextBox.IsEnabled = false; 
                        PasswordPasswordBox.IsEnabled = false; 
                    }
                    else
                    {
                        if (OldPasswordTextBox.Text == UserClass.GetUserTable.Password_User)
                        { 
                            OldPasswordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20)); 
                            NewPasswordTextBox.IsEnabled = true; 
                            PasswordPasswordBox.IsEnabled = true; 
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "PR007 - Ошибка сохранения", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error); 
            }
        }

        private void PasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewPasswordTextBox.Text != "" && PasswordPasswordBox.Password == "") 
                { 
                    PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63)); 
                    SavePasswordButton.IsEnabled = false; 
                }
                else
                {
                    if (NewPasswordTextBox.Text == "") 
                    { 
                        PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));
                        SavePasswordButton.IsEnabled = false; 
                    }
                    else
                    {
                        if (PasswordPasswordBox.Password != NewPasswordTextBox.Text)
                        { 
                            PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58));
                            SavePasswordButton.IsEnabled = false; 
                        }
                        if (PasswordPasswordBox.Password == NewPasswordTextBox.Text) 
                        { 
                            PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20)); 
                            SavePasswordButton.IsEnabled = true; 
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "PR008 - Ошибка регистрации",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error); 
            }
        }

        private void NewPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (NewPasswordTextBox.Text != "" && PasswordPasswordBox.Password == "")
                { 
                    PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63)); 
                    SavePasswordButton.IsEnabled = false; 
                }
                else
                {
                    if (NewPasswordTextBox.Text == "") 
                    { 
                        PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 63));
                        SavePasswordButton.IsEnabled = false; 
                    }
                    else
                    {
                        if (PasswordPasswordBox.Password != NewPasswordTextBox.Text)
                        { 
                            PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 7, 58));
                            SavePasswordButton.IsEnabled = false; 
                        }
                        if (PasswordPasswordBox.Password == NewPasswordTextBox.Text)
                        { 
                            PasswordPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 255, 20)); 
                            SavePasswordButton.IsEnabled = true; 
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "PR009 - Ошибка регистрации", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error); 
            }
        }
    }
}
