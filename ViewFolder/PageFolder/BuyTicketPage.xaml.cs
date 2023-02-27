using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class BuyTicketPage : Page
    {
        public BuyTicketPage()
        {
            try { InitializeComponent(); AppConnectClass.DataBase = new LibraryMirzayevaEntities(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "TI001 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void PriseTicketListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try { PriseTicketListBox.ItemsSource = AppConnectClass.DataBase.BuyTicketTable.ToList(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "TI002 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
