using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.ViewFolder.PageFolder;
using System;
using System.Windows;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                FrameNavigationClass.MenuFNC = MenuFrame; FrameNavigationClass.BodyFNC = BodyFrame; 
                FrameNavigationClass.MenuFNC.Navigate(new MenuPage());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "MA001 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        #region Управление окном
        private void ExitButton_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                UserClass.GetUserTable = null;
                AuthorizationWindow authorizationWindow = new AuthorizationWindow(); authorizationWindow.Show(); this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "MABU004 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) // Для того, что бы окно перетаскивать 
        {
            try { if (e.ChangedButton == MouseButton.Left) { this.DragMove(); } }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "MABU001 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try { Application.Current.Shutdown(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "MABU002 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            try { WindowState = WindowState.Minimized; }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "MABU003 - Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion
    }
}
