using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class BuyTicketPage : Page
    {
        DateTime TodayDate = DateTime.Today; // Получаем сегодняшнюю дату
        public BuyTicketPage(UserTable userTable)
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();

                var ListServices = AppConnectClass.DataBase.BuyTicketTable;
                PriseTicketListBox.ItemsSource = ListServices.ToList();

                if (userTable != null)
                {
                    DataContext = userTable;
                }
                else 
                {
                    InformationTicketStavkPanel.Visibility = Visibility.Collapsed;
                    NullTicketUsetTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "TI001 - Ошибка",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
        #region Color
        SolidColorBrush RedColor = new SolidColorBrush(Color.FromRgb(255, 7, 58));
        #endregion
        #region MouseDoubleClick
        private void PriseTicketListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (UserClass.GetUserTable != null)
                {
                    GiveUserTicket();
                }
                else
                {
                    MessageBox.Show(
                        "Для преобритения читательского билета, вам нужно авторизоваться",
                        "Уведомление",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "TI002 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        #endregion
        #region Действие
        private void GetTicketUser()
        {
            var WatchTicketUser = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicket_User);

            if (WatchTicketUser != null || UserClass.GetUserTable.pnTicket_User != 1)
            {

                DateTime WatchDateStart = WatchTicketUser.DateStart_Ticket; // Берём дату приобритения билета
                DateTime WatchDateEnd = WatchTicketUser.DateEnd_Ticket; // Берём посчитанную дату конца "подписки"

                TimeSpan WatchSummaDate = WatchDateEnd.Subtract(WatchDateStart); // Вычесляем разницу между Началом и концом "Подписки"
                TimeSpan WatchGetDay = WatchDateEnd.Subtract(TodayDate); // Получаем количество дней "осталось"

                double WatchDateDouble = WatchSummaDate.TotalDays; // Получаем количество дней между началом и концом в числах
                double WatchGetDateDouble = WatchGetDay.TotalDays; // получаем количество дней в числах

                SummaDataTicketTextBlock.Text = "(" + WatchTicketUser.BuyTicketTable.Name_Buy + ", на " + WatchDateDouble.ToString() + " дней)";
                GetDataTicketTextBlock.Text = "(" + WatchGetDateDouble.ToString() + " дней осталось)";

                if (WatchGetDateDouble <= 0)
                {
                    GetDataTicketTextBlock.Text = "( читательский билет просрочен на: " + Math.Abs(WatchGetDateDouble).ToString() + " дней)";
                    GetDataTicketTextBlock.Foreground = RedColor;
                }

            }
            else if (UserClass.GetUserTable.pnTicket_User == 1)
            { 
                TextNull(); 
            }
        }

        private void GiveUserTicket()
        {
            BuyTicketTable buyTicketTable = (BuyTicketTable)PriseTicketListBox.SelectedItem; // Получаем услугу из списка услуг

            if (UserClass.GetUserTable.pnTicket_User == 1)
            {
                int QuantityDays = buyTicketTable.QwentyYear_Buy; // Получаем количество дней
                DateTime DateDays = TodayDate.AddDays(QuantityDays); // Прибавляем полученное количество дней к полученной дате

                var NewTicketAdd = new TicketTable() // Создаём "коробку" в которой будем хранить информацию о билете
                {
                    DateStart_Ticket = TodayDate,
                    DateEnd_Ticket = DateDays,
                    pnBuy_Ticket = buyTicketTable.Personalnumber_Buy
                };

                var UpdateUser = UserClass.GetUserTable; // Создаём переменную в которой будем хранить информацию о пользователе
                    UpdateUser.pnTicket_User = NewTicketAdd.PersonalNumber_Ticket; // Обновляем данные в переменной

                try
                {
                    AppConnectClass.DataBase.TicketTable.Add(NewTicketAdd); // Добавляем читательскй билет
                    AppConnectClass.DataBase.UserTable.AddOrUpdate(UpdateUser); // Обновляем пользователя
                    AppConnectClass.DataBase.SaveChanges();

                    MessageBox.Show(
                        "Читательский билет успешно приобретён",
                        "Уведомление",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message.ToString(),
                        "TI003 - Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                if (UserClass.GetUserTable.pnTicket_User != 1)
                {
                    var UpdateTicketUser = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicket_User);
                    DateTime UpdateDateEnd = UpdateTicketUser.DateEnd_Ticket;

                    if (UpdateDateEnd > TodayDate) // Если дата конца больше сегодняшней даты
                    {
                        MessageBox.Show(
                            "Вы пока что не можите приобрести читательский билет, так как у предыдущего читательского билета ещё не истёк срок",
                            "Уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        int UpdateQuantityDays = buyTicketTable.QwentyYear_Buy; // Получаем количество дней
                        DateTime HomeDate = TodayDate.AddDays(UpdateQuantityDays); // Прибавляем полученное количество дней к полученной дате

                        try
                        {
                                UpdateTicketUser.DateStart_Ticket = TodayDate;
                                UpdateTicketUser.DateEnd_Ticket = HomeDate;
                                UpdateTicketUser.pnBuy_Ticket = buyTicketTable.Personalnumber_Buy;

                            AppConnectClass.DataBase.TicketTable.AddOrUpdate(UpdateTicketUser); // Обновляем читательский билет
                            AppConnectClass.DataBase.SaveChanges();

                            MessageBox.Show(
                                "Читательский билет успешно приобретён",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                            /// TODO: Нужно как то обновлять данные на странице (перезагрузка страницы не работает, нужно как то перезагружать массиф данных UserClass)
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(
                                ex.Message.ToString(),
                                "TI004 - Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void TextNull()
        {
            InformationTicketStavkPanel.Visibility = Visibility.Collapsed;
            NullTicketUsetTextBlock.Visibility = Visibility.Visible;
        }
        #endregion

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible) 
            {
                if (UserClass.GetUserTable.pnTicket_User == 1)
                {
                    InformationTicketStavkPanel.Visibility = Visibility.Collapsed;
                    NullTicketUsetTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    GetTicketUser();
                }
            }
        }
    }
}
