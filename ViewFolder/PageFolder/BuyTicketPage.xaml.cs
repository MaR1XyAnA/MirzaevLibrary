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

                if (userTable != null)
                {
                    DataContext = userTable;
                    GetTicketUser();
                }
                else { TextNull(); }

                var ListServices = AppConnectClass.DataBase.BuyTicketTable;
                PriseTicketListBox.ItemsSource = ListServices.ToList();
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

                DateTime WatchDateStart = WatchTicketUser.DateStart_Ticket;
                DateTime WatchDateEnd = WatchTicketUser.DateEnd_Ticket;
                DateTime WatchToDayDay = DateTime.Today;

                TimeSpan WatchSummaDate = WatchDateEnd.Subtract(WatchDateStart);
                TimeSpan WatchGetDay = WatchDateEnd.Subtract(WatchToDayDay);

                double WatchDateDouble = WatchSummaDate.TotalDays;
                double WatchGetDateDouble = WatchGetDay.TotalDays;

                SummaDataTicketTextBlock.Text = "(" + WatchDateDouble.ToString() + " дней)";
                GetDataTicketTextBlock.Text = "(" + WatchGetDateDouble.ToString() + " дней осталось)";

                if (WatchDateDouble <= 0)
                {
                    SummaDataTicketTextBlock.Text = "( читательский билет просрочен на:" + WatchDateDouble.ToString() + " дней)";
                    SummaDataTicketTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(225, 7, 58));
                }

            }
            else { TextNull(); }
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
            TicketTextBlock.Text = "NULL";
            DataStartTicketTextBlock.Text = "NULL";
            DataEndTicketTextBlock.Text = "NULL";
        }
        #endregion
    }
}
