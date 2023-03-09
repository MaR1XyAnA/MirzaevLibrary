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
            try { InitializeComponent(); }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "ME001 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        #region Действие
        private void ControlsIsChecked()
        {
            FilecabinetToggleButton.IsChecked = false;
            CategoryToggleButton.IsChecked = false;
            BuyTicketButton.IsChecked = false;
            MyProfilToggleButton.IsChecked = false;
        }
        private void ControlsIsEnabled()
        {
            FilecabinetToggleButton.IsEnabled = true;
            CategoryToggleButton.IsEnabled = true;
            BuyTicketButton.IsEnabled = true;
            MyProfilToggleButton.IsEnabled = true;
        }
        #endregion
        #region Click
        private void FilecabinetToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(null));

                ControlsIsChecked();
                FilecabinetToggleButton.IsChecked = true;

                ControlsIsEnabled();
                FilecabinetToggleButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "ME002 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void CategoryToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new CategoriesPage());

                ControlsIsChecked();
                CategoryToggleButton.IsChecked = true;

                ControlsIsEnabled();
                CategoryToggleButton.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "ME003 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new BuyTicketPage(UserClass.GetUserTable));

                ControlsIsChecked();
                BuyTicketButton.IsChecked = true;

                ControlsIsEnabled();
                BuyTicketButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "ME004 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void MyProfilToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.BodyFNC.Navigate(new ProfilePage(UserClass.GetUserTable));

                ControlsIsChecked();
                MyProfilToggleButton.IsChecked = true;

                ControlsIsEnabled();
                MyProfilToggleButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "ME005 - Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                try
                {
                    FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(null));

                    ControlsIsChecked();
                    FilecabinetToggleButton.IsChecked = true;

                    ControlsIsEnabled();
                    FilecabinetToggleButton.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message.ToString(),
                        "ME006 - Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.BodyFNC.GoBack();
        }
    }
}
