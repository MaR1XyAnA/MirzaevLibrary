using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class InformationBookPage : Page
    {
        int NumberBook = 0; // Переменная для хранения номера книжки
        public InformationBookPage(BookTable bookTable)
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();

                if (bookTable != null)
                {
                    DataContext = bookTable;
                    NumberBook = bookTable.PersonalNumber_Book;
                }
                else
                {
                    FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(null));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "BO001 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MenuPage menuPage = new MenuPage();
                menuPage.BackButton.Visibility = Visibility.Visible;
                if (UserClass.GetUserTable == null || UserClass.GetUserTable.pnTicket_User == 1)
                {
                    TakereadButton.IsEnabled = false; // Выключаем кнопку, если зашёл неавторизированный пользователь
                }
            }
        }

        #region Click
        private void TakereadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TakeRead();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "BO002 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        #endregion
        #region Действие
        public void TakeRead()
        {
            if (UserClass.GetUserTable.pnTicket_User == 1)
            {
                TakereadButton.IsEnabled = false;
            }
            else
            {
                var ObjectTicket = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicket_User); // Получаем читательский билет по номеру 
                var ObjectBook = AppConnectClass.DataBase.BookTable.Find(NumberBook);

                ObjectTicket.BookTable.Add(ObjectBook); // Добавления данных в "связь многие ко многим"
                AppConnectClass.DataBase.SaveChanges();

                FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(null));
                MessageBox.Show(
                    "Книжка добавленна в историю\nПриятного чтения!",
                    "Добавление");
            }
        }
        #endregion
    }
}
