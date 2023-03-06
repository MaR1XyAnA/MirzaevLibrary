using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class BuyTicketPage : Page
    {
        public BuyTicketPage(UserTable userTable)
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();
                PriseTicketListBox.ItemsSource = AppConnectClass.DataBase.BuyTicketTable.ToList();

                if (userTable != null)
                {
                    DataContext = userTable;
                    var TicketUser = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicket_User);

                    if (TicketUser != null || UserClass.GetUserTable.pnTicket_User != 1) 
                    {
                        
                        DateTime DateStart = TicketUser.DateStart_Ticket;
                        DateTime DateEnd = TicketUser.DateEnd_Ticket;
                        DateTime ToDayDay = DateTime.Today;

                        TimeSpan SummaDate = DateEnd.Subtract(DateStart);
                        TimeSpan GetDay = DateEnd.Subtract(ToDayDay);

                        double DateDouble = SummaDate.TotalDays;
                        double GetDateDouble = GetDay.TotalDays;

                        SummaDataTicketTextBlock.Text = "(" + DateDouble.ToString() + " дней)";
                        GetDataTicketTextBlock.Text = "(" + GetDateDouble.ToString() + " дней осталось)";

                        if (DateDouble <=0)
                        {
                            SummaDataTicketTextBlock.Text = "( читательский билет просрочен на:" + DateDouble.ToString() + " дней)";
                        }

                    }
                    else { TextNull(); }
                }
                else { TextNull(); }
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

        private void TextNull()
        {
            TicketTextBlock.Text = "NULL";
            DataStartTicketTextBlock.Text = "NULL";
            DataEndTicketTextBlock.Text = "NULL";
        }

        private void PriseTicketListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try 
            {
                if (UserClass.GetUserTable != null)
                {
                    BuyTicketTable buyTicketTable = (BuyTicketTable)PriseTicketListBox.SelectedItem; // Получаем услугу из списка услуг

                    if (UserClass.GetUserTable.pnTicket_User == 1)
                    {
                        DateTime MayDay = DateTime.Today; // Получаем сегодняшнюю дату
                        int GetDay = buyTicketTable.QwentyYear_Buy; // Получаем количество дней
                        DateTime CityDate = MayDay.AddDays(GetDay); // Прибавляем полученное количество дней к полученной дате

                        var GetTicketAdd = new TicketTable() // Создаём "коробку" в которой будем хранить информацию о билете
                        {
                            DateStart_Ticket = MayDay,
                            DateEnd_Ticket = CityDate,
                            pnBuy_Ticket = buyTicketTable.Personalnumber_Buy
                        };

                        var GetUserUpdate = new UserTable() // Создаём "коробку" в которой будем хранить информацию о пользователе
                        {
                            PersonalNumber_User = UserClass.GetUserTable.PersonalNumber_User,
                            Surname_User = UserClass.GetUserTable.Surname_User,
                            Name_User = UserClass.GetUserTable.Name_User,
                            Middlename_User = UserClass.GetUserTable.Middlename_User,
                            Address_User = UserClass.GetUserTable.Address_User,
                            Phone_User = UserClass.GetUserTable.Phone_User,
                            pnTicket_User = GetTicketAdd.PersonalNumber_Ticket,
                            Login_User = UserClass.GetUserTable.Login_User,
                            Password_User = UserClass.GetUserTable.Password_User,
                            pnRole_User = UserClass.GetUserTable.pnRole_User,
                            pnImage_User = UserClass.GetUserTable.pnImage_User
                        };

                        try
                        {
                            AppConnectClass.DataBase.TicketTable.Add(GetTicketAdd); // Добавляем читательскй билет
                            AppConnectClass.DataBase.UserTable.AddOrUpdate(GetUserUpdate); // Обновляем пользователя
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
                            DateTime Smile = DateTime.Today;
                            var Skolkovo = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicket_User);
                            DateTime Letter = Skolkovo.DateEnd_Ticket;

                            if (Letter > Smile) // Если дата конца больше сегодняшней даты
                            {
                                MessageBox.Show(
                                    "Вы пока что не можите приобрести читательский билет, так как у предыдущего читательского билета ещё не истёк срок", 
                                    "Уведомление", 
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                            else
                            {
                                DateTime MayBar = DateTime.Today; // Получаем сегодняшнюю дату
                                int GetDay = buyTicketTable.QwentyYear_Buy; // Получаем количество дней
                                DateTime HomeDate = MayBar.AddDays(GetDay); // Прибавляем полученное количество дней к полученной дате

                                var GetTicket = new TicketTable() // Создаём "коробку" в которой будем хранить информацию о билете
                                {
                                    PersonalNumber_Ticket = Skolkovo.PersonalNumber_Ticket,
                                    DateStart_Ticket = MayBar,
                                    DateEnd_Ticket = HomeDate,
                                    pnBuy_Ticket = buyTicketTable.Personalnumber_Buy
                                };
                                try
                                {
                                    AppConnectClass.DataBase.TicketTable.AddOrUpdate(GetTicket); // Обновляем читательский билет
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
    }
}
