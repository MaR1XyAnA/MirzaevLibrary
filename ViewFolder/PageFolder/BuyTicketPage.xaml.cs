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
                    var TicketUser = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicketUser);

                    if (TicketUser != null || UserClass.GetUserTable.pnTicketUser != 1) 
                    {
                        
                        DateTime DateStart = TicketUser.DateStartTicket;
                        DateTime DateEnd = TicketUser.DateEndTicket;
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

                    if (UserClass.GetUserTable.pnTicketUser == 1)
                    {
                        DateTime MayDay = DateTime.Today; // Получаем сегодняшнюю дату
                        int GetDay = buyTicketTable.QwentyYearBuy; // Получаем количество дней
                        DateTime CityDate = MayDay.AddDays(GetDay); // Прибавляем полученное количество дней к полученной дате

                        var GetTicketAdd = new TicketTable() // Создаём "коробку" в которой будем хранить информацию о билете
                        {
                            DateStartTicket = MayDay,
                            DateEndTicket = CityDate,
                            pnBuy = buyTicketTable.PersonalnumberBuy
                        };

                        var GetUserUpdate = new UserTable() // Создаём "коробку" в которой будем хранить информацию о пользователе
                        {
                            PersonalNumberUser = UserClass.GetUserTable.PersonalNumberUser,
                            SurnameUser = UserClass.GetUserTable.SurnameUser,
                            NameUser = UserClass.GetUserTable.NameUser,
                            MiddlenameUser = UserClass.GetUserTable.MiddlenameUser,
                            AddressUser = UserClass.GetUserTable.AddressUser,
                            PhoneUser = UserClass.GetUserTable.PhoneUser,
                            pnTicketUser = GetTicketAdd.PersonalNumberTicket,
                            LoginUser = UserClass.GetUserTable.LoginUser,
                            PasswordUser = UserClass.GetUserTable.PasswordUser,
                            pnRoleUser = UserClass.GetUserTable.pnRoleUser,
                            pnImageUser = UserClass.GetUserTable.pnImageUser
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
                        if (UserClass.GetUserTable.pnTicketUser != 1)
                        {
                            DateTime Smile = DateTime.Today;
                            var Skolkovo = AppConnectClass.DataBase.TicketTable.Find(UserClass.GetUserTable.pnTicketUser);
                            DateTime Letter = Skolkovo.DateEndTicket;

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
                                int GetDay = buyTicketTable.QwentyYearBuy; // Получаем количество дней
                                DateTime HomeDate = MayBar.AddDays(GetDay); // Прибавляем полученное количество дней к полученной дате

                                var GetTicket = new TicketTable() // Создаём "коробку" в которой будем хранить информацию о билете
                                {
                                    PersonalNumberTicket = Skolkovo.PersonalNumberTicket,
                                    DateStartTicket = MayBar,
                                    DateEndTicket = HomeDate,
                                    pnBuy = buyTicketTable.PersonalnumberBuy
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
