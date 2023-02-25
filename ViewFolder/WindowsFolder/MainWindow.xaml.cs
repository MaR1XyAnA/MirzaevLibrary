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
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }} // Для того, что бы окно перетаскивать 
        private void ExitButton_Click(object sender, RoutedEventArgs e) 
        { 
            UserClass.GetUserTable = null; 
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();  authorizationWindow.Show(); this.Close(); 
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void RollUpButton_Click(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }
        #endregion
    }
}
