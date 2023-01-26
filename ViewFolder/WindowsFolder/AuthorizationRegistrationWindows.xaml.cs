using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.ViewFolder.PageFolder;
using System.Windows;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class AuthorizationRegistrationWindows : Window
    {
        public AuthorizationRegistrationWindows()
        {
            InitializeComponent();
            FrameNavigationClass.BodyAuthorizationRegistration = MainAuthorizationRegistrationFrame;
            FrameNavigationClass.BodyAuthorizationRegistration.Navigate(new AuthorizationPage());
        }
        #region Управление окном
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e) // Для того, что бы окно перетаскивать
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion
    }
}
