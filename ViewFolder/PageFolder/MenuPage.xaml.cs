using MirzaevLibrary.AppDataFolder.ClassFolder;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            try{ InitializeComponent(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ME001 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void FilecabinetToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(null));

                FilecabinetToggleButton.IsChecked = true;
                CategoryToggleButton.IsChecked = false;
                BuyTicketButton.IsChecked = false;
                MyProfilToggleButton.IsChecked = false;

                FilecabinetToggleButton.IsEnabled = false;
                CategoryToggleButton.IsEnabled = true;
                BuyTicketButton.IsEnabled = true;
                MyProfilToggleButton.IsEnabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ME002 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CategoryToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new CategoriesPage());

                FilecabinetToggleButton.IsChecked = false;
                CategoryToggleButton.IsChecked = true;
                BuyTicketButton.IsChecked = false;
                MyProfilToggleButton.IsChecked = false;

                FilecabinetToggleButton.IsEnabled = true;
                CategoryToggleButton.IsEnabled = false;
                BuyTicketButton.IsEnabled = true;
                MyProfilToggleButton.IsEnabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ME003 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void MyProfilToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new ProfilePage(UserClass.GetUserTable));

                FilecabinetToggleButton.IsChecked = false;
                CategoryToggleButton.IsChecked = false;
                BuyTicketButton.IsChecked = false;
                MyProfilToggleButton.IsChecked = true;

                FilecabinetToggleButton.IsEnabled = true;
                CategoryToggleButton.IsEnabled = true;
                BuyTicketButton.IsEnabled = true;
                MyProfilToggleButton.IsEnabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ME005 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility== Visibility.Visible)
            {
                try
                {
                    FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(null));

                    FilecabinetToggleButton.IsChecked = true;
                    CategoryToggleButton.IsChecked = false;
                    BuyTicketButton.IsChecked = false;
                    MyProfilToggleButton.IsChecked = false;

                    FilecabinetToggleButton.IsEnabled = false;
                    CategoryToggleButton.IsEnabled = true;
                    BuyTicketButton.IsEnabled = true;
                    MyProfilToggleButton.IsEnabled = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ME006 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilecabinetToggleButton.IsChecked = false;
                CategoryToggleButton.IsChecked = false;
                BuyTicketButton.IsChecked = true;
                MyProfilToggleButton.IsChecked = false;

                FilecabinetToggleButton.IsEnabled = true;
                CategoryToggleButton.IsEnabled = true;
                BuyTicketButton.IsEnabled = false;
                MyProfilToggleButton.IsEnabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ME004 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
