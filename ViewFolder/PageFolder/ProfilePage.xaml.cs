using Microsoft.Win32;
using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using MirzaevLibrary.ViewFolder.WindowsFolder;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class ProfilePage : Page
    {
        string PathImage;
        public ProfilePage(UserTable GetUserTable)
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();

                if (GetUserTable != null) // Если передоваемое значение не пустое
                {
                    DataContext = GetUserTable;

                    if (UserClass.GetUserTable.pnTicket_User == 1) // Если у пользователя стандартный номер читательского билета
                    {
                        InfoTicketThoTextBlock.Text = "У вас нет читательского билета, но вы можете его преобрести";
                        InfoTicketOneTextBlock.Visibility = Visibility.Collapsed;
                        HistoryBookListBox.Visibility = Visibility.Collapsed;
                        HintHistoryTextBlock.Visibility = Visibility.Visible;
                        HintHistoryTextBlock.Text = "У вас нет читательского билета";
                    }
                    else // Если у пользователя приобритён читательский билет
                    {
                        int NumberTicket = UserClass.GetUserTable.pnTicket_User; // Получаем номер читательского билета
                        AppConnectClass.DataBase.TicketTable.Include(Ticket_Book => Ticket_Book.BookTable).Load(); //Реализовываем связь многие ко многим между таблицей TicketTable и таблицей BookTable
                        var ObjectTicket = AppConnectClass.DataBase.TicketTable.Find(NumberTicket); // Ищем в таблице TicketTable читательский билет по номеру
                        HistoryBookListBox.ItemsSource = ObjectTicket.BookTable.ToList();

                        if (HistoryBookListBox.Items.Count == 0)
                        {
                            HistoryBookListBox.Visibility = Visibility.Collapsed;
                            HintHistoryTextBlock.Visibility = Visibility.Visible;
                            HintHistoryTextBlock.Text = "Вы ещё не прочитали не одной книжки";
                        }
                        else
                        {
                            HintHistoryTextBlock.Visibility = Visibility.Collapsed;
                        }
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
                    "PR001 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #region Color
        SolidColorBrush RedColor = new SolidColorBrush(Color.FromRgb(255, 7, 58));
        SolidColorBrush GreenColor = new SolidColorBrush(Color.FromRgb(57, 255, 20));
        SolidColorBrush StandardColor = new SolidColorBrush(Color.FromRgb(62, 62, 63));
        #endregion
        #region Click
        private void PhotoButton_Click(object sender, RoutedEventArgs e) // ПРи нажатии на кнопку открываем FileDialog и получаем путь к картинке
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true) // Если пользователь выбрал содержимое
            {
                PathImage = openFileDialog.FileName; // Получение пути к выбранному файлу и записываем в переменную
                UserImage.Source = new BitmapImage(new Uri(openFileDialog.FileName)); ; // Вставить фото в пользовательский элемент управления
            }
        }
        private void AuthorizationButton_Click(object sender, RoutedEventArgs e) // Когда зашел не авторизированный пользователь
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e) // Когда зашел не авторизированный пользователь
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
        private void SaveProfilButton_Click(object sender, RoutedEventArgs e) // Сохранение профиля пользователя
        {
            try
            {
                if (UserClass.GetUserTable == null) // Если пользователь не авторизированный
                {
                    MessageBox.Show(
                        "Вам нужно сначала авторизоваться",
                        "PR004 - Ошибка сохранения",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    SaveProfilUser();
                    NewPhotoUser();
                    UIIsEnabled();
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
        private void EditProfilButton_Click(object sender, RoutedEventArgs e) // Просто включает элементы управления и включает\отключает некоторые кнопки
        {
            SurnameTextBox.IsEnabled = true;
            NameTextBox.IsEnabled = true;
            MiddlenameTextBox.IsEnabled = true;
            AddresTextBox.IsEnabled = true;
            PhoneTextBox.IsEnabled = true;

            EditPasswordButton.Visibility = Visibility.Visible;
            SaveProfilButton.Visibility = Visibility.Visible;
            PhotoButton.Visibility = Visibility.Visible;
            EditProfilButton.Visibility = Visibility.Collapsed;
        }
        private void EditPasswordButton_Click(object sender, RoutedEventArgs e) // Делает видемым некоторые элементы управления, а некоторые не видемым
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
        private void SavePasswordButton_Click(object sender, RoutedEventArgs e) // Метод для сохранения пароля
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

                    SavePasswordUser();
                    UIIsEnabled();
                    SurnameStackPanel.Visibility = Visibility.Visible;
                    NameStackPanel.Visibility = Visibility.Visible;
                    MiddlenameStackPanel.Visibility = Visibility.Visible;
                    AddresStackPanel.Visibility = Visibility.Visible;
                    PhoneStackPanel.Visibility = Visibility.Visible;

                    //OldPasswordStackPanel.Visibility = Visibility.Collapsed;
                    //NewPasswordStackPanel.Visibility = Visibility.Collapsed;
                    //PasswordStackPanel.Visibility = Visibility.Collapsed;

                    //SurnameTextBox.IsEnabled = false;
                    //NameTextBox.IsEnabled = false;
                    //MiddlenameTextBox.IsEnabled = false;
                    //AddresTextBox.IsEnabled = false;
                    //PhoneTextBox.IsEnabled = false;

                    //EditPasswordButton.Visibility = Visibility.Collapsed;
                    //SaveProfilButton.Visibility = Visibility.Collapsed;
                    //SavePasswordButton.Visibility = Visibility.Collapsed;
                    //EditProfilButton.Visibility = Visibility.Visible;
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
        #endregion
        #region Действие
        private void UIIsEnabled()
        {
            SurnameTextBox.IsEnabled = false;
            NameTextBox.IsEnabled = false;
            MiddlenameTextBox.IsEnabled = false;
            AddresTextBox.IsEnabled = false;
            PhoneTextBox.IsEnabled = false;

            EditPasswordButton.Visibility = Visibility.Collapsed;
            SaveProfilButton.Visibility = Visibility.Collapsed;
            EditProfilButton.Visibility = Visibility.Visible;
        }
        private void NewPhotoUser() // Метод, который сохраняет информацию об картинке пользователя
        {
            try
            {
                if (PathImage != "")
                {
                    // Конвертация изображения в байты
                    byte[] imageData;
                    using (FileStream fs = new FileStream(PathImage, FileMode.Open, FileAccess.Read))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }

                    var newPhoto = new ImageUserTable() // Создаём "коробку" в которой будем хранить информацию о фотографии
                    {
                        Image_Image = imageData
                    };

                    if (UserClass.GetUserTable.pnImage_User == 1) // Если у пользователя нет ФОТО
                    {
                        AppConnectClass.DataBase.ImageUserTable.Add(newPhoto);
                    }
                    else
                    {
                        AppConnectClass.DataBase.ImageUserTable.AddOrUpdate(newPhoto);
                    }
                    AppConnectClass.DataBase.SaveChanges();

                    var UpdateImageUser = UserClass.GetUserTable; // Создаём переменную в которой будем хранить информацию о пользователе
                    UpdateImageUser.pnImage_User = newPhoto.Personalnumber_Image;
                    AppConnectClass.DataBase.UserTable.AddOrUpdate(UpdateImageUser);
                    AppConnectClass.DataBase.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "PR005 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private void SaveProfilUser() // Метод для обновления текстовой информации профиля пользователя (Да, в тупую постоянно перезаписываем информацию о пользователе)
        {
            string MessageNull = "";
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text)) MessageNull += "Поле с ФАМИЛИЕЙ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text)) MessageNull += "Поле с ИМЕНЕМ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text)) MessageNull += "Поле с НОМЕРОМ ТЕЛЕФОНА не должно быть пустым";

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
                var UpdateInformationUser = UserClass.GetUserTable;

                UpdateInformationUser.Surname_User = SurnameTextBox.Text;
                UpdateInformationUser.Name_User = NameTextBox.Text;
                UpdateInformationUser.Middlename_User = MiddlenameTextBox.Text;
                UpdateInformationUser.Phone_User = PhoneTextBox.Text;
                UpdateInformationUser.Address_User = AddresTextBox.Text;

                AppConnectClass.DataBase.UserTable.AddOrUpdate(UpdateInformationUser);
                AppConnectClass.DataBase.SaveChanges();

                MessageBox.Show("Данные сохранены успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void SavePasswordUser() // Метод для обновления пароля профиля пользователя (Да, в тупую постоянно проверяем, совподает ли пароль из переменной с тем, что лежит в TextBox или PasswordBox, если нет то перезаписываем)
        {
            string MessageNullPasswoerd = "";
            if (string.IsNullOrWhiteSpace(NewPasswordTextBox.Text)) MessageNullPasswoerd += "Поле с ПАРОЛЕМ не должно быть пустым \n";
            if (string.IsNullOrWhiteSpace(PasswordPasswordBox.Password)) MessageNullPasswoerd += "Поле с ПОВТОРНЫМ ПАРОЛЕМ не должно быть пустым";

            if (MessageNullPasswoerd != "")
            {
                MessageBox.Show(
                    MessageNullPasswoerd,
                    "Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                MessageNullPasswoerd = null;
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
                    var UpdatePasswordUser = UserClass.GetUserTable;
                    UpdatePasswordUser.Password_User = NewPasswordTextBox.Text;

                    AppConnectClass.DataBase.UserTable.AddOrUpdate(UpdatePasswordUser);
                    AppConnectClass.DataBase.SaveChanges();

                    MessageBox.Show("Пароль сменён успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion
        #region TextChanged_PasswordChanged
        private void OldPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (OldPasswordTextBox.Text == UserClass.GetUserTable.Password_User)
                {
                    OldPasswordTextBox.BorderBrush = GreenColor;
                    NewPasswordTextBox.IsEnabled = true;
                    PasswordPasswordBox.IsEnabled = true;
                }
                else
                {
                    OldPasswordTextBox.BorderBrush = RedColor;
                    NewPasswordTextBox.IsEnabled = false;
                    PasswordPasswordBox.IsEnabled = false;
                }
                if (OldPasswordTextBox.Text == "")
                {
                    OldPasswordTextBox.BorderBrush = StandardColor;
                    NewPasswordTextBox.IsEnabled = false;
                    PasswordPasswordBox.IsEnabled = false;
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
                if (NewPasswordTextBox.Text == "" || PasswordPasswordBox.Password != NewPasswordTextBox.Text)
                {
                    PasswordPasswordBox.BorderBrush = RedColor;
                    SavePasswordButton.IsEnabled = false;
                }
                else
                {
                    PasswordPasswordBox.BorderBrush = GreenColor;
                    SavePasswordButton.IsEnabled = true;
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
                if (NewPasswordTextBox.Text == "")
                {
                    PasswordPasswordBox.BorderBrush = StandardColor;
                    SavePasswordButton.IsEnabled = false;
                }
                else if (PasswordPasswordBox.Password == "")
                {
                    PasswordPasswordBox.BorderBrush = StandardColor;
                    SavePasswordButton.IsEnabled = false;
                }
                else if (PasswordPasswordBox.Password != NewPasswordTextBox.Text)
                {
                    PasswordPasswordBox.BorderBrush = RedColor;
                    SavePasswordButton.IsEnabled = false;
                }
                else
                {
                    PasswordPasswordBox.BorderBrush = GreenColor;
                    SavePasswordButton.IsEnabled = true;
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
        #endregion
    }
}
