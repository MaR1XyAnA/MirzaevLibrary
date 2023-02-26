using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
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
    public partial class ProfilePage : Page
    {
        public ProfilePage(UserTable GetUserTable)
        {
            try
            {
                InitializeComponent();
                if (GetUserTable != null)
                {
                    DataContext = GetUserTable;
                    HintHistoryTextBlock.Visibility = Visibility.Collapsed;
                    AppConnectClass.DataBase = new LibraryMirzayevaEntities();
                    HistoryBookListBox.ItemsSource = AppConnectClass.DataBase.BookTable.ToList();
                    if (UserClass.GetUserTable.pnTicketUser == null || UserClass.GetUserTable.pnTicketUser == 1)
                    {
                        InfoTicketThoTextBlock.Text = "У вас нет читательского билета, но вы можите его преобрести";
                        InfoTicketOneTextBlock.Visibility= Visibility.Collapsed;
                    }
                }
                else
                {
                    ImageSource userImage = new BitmapImage(new Uri("/ContentFolder/ImageFolder/NoImage.png", UriKind.RelativeOrAbsolute));
                    UserImage.Source = userImage;
                    HintInfoTicketTextBlock.Visibility = Visibility.Visible;
                    InfoTicketOneTextBlock.Visibility = Visibility.Collapsed;
                    InfoTicketThoTextBlock.Visibility = Visibility.Collapsed;
                    EditProfilButton.IsEnabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "PR001 - Ошибка акторизации", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                
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

        private void PasswordPasswordBox_LayoutUpdated(object sender, EventArgs e)
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

        }
    }
}
