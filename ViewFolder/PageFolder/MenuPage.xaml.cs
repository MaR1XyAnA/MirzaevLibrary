using MirzaevLibrary.AppDataFolder.ClassFolder;
using System.Windows;
using System.Windows.Controls;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void FilecabinetToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage());

            FilecabinetToggleButton.IsChecked = true;
            CategoryToggleButton.IsChecked = false;
            AuthorsToggleButton.IsChecked = false;
            MyProfilToggleButton.IsChecked = false;

            FilecabinetToggleButton.IsEnabled = false;
            CategoryToggleButton.IsEnabled = true;
            AuthorsToggleButton.IsEnabled = true;
            MyProfilToggleButton.IsEnabled = true;
        }

        private void CategoryToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.BodyFNC.Navigate(new CategoriesPage());

            FilecabinetToggleButton.IsChecked = false;
            CategoryToggleButton.IsChecked = true;
            AuthorsToggleButton.IsChecked = false;
            MyProfilToggleButton.IsChecked = false;

            FilecabinetToggleButton.IsEnabled = true;
            CategoryToggleButton.IsEnabled = false;
            AuthorsToggleButton.IsEnabled = true;
            MyProfilToggleButton.IsEnabled = true;
        }

        private void AuthorsToggleButton_Click(object sender, RoutedEventArgs e)
        {

            FilecabinetToggleButton.IsChecked = false;
            CategoryToggleButton.IsChecked = false;
            AuthorsToggleButton.IsChecked = true;
            MyProfilToggleButton.IsChecked = false;

            FilecabinetToggleButton.IsEnabled = true;
            CategoryToggleButton.IsEnabled = true;
            AuthorsToggleButton.IsEnabled = false;
            MyProfilToggleButton.IsEnabled = true;
        }

        private void MyProfilToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.BodyFNC.Navigate(new ProfilePage());

            FilecabinetToggleButton.IsChecked = false;
            CategoryToggleButton.IsChecked = false;
            AuthorsToggleButton.IsChecked = false;
            MyProfilToggleButton.IsChecked = true;

            FilecabinetToggleButton.IsEnabled = true;
            CategoryToggleButton.IsEnabled = true;
            AuthorsToggleButton.IsEnabled = true;
            MyProfilToggleButton.IsEnabled = false;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility== Visibility.Visible)
            {
                FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage());

                FilecabinetToggleButton.IsChecked = true;
                CategoryToggleButton.IsChecked = false;
                AuthorsToggleButton.IsChecked = false;
                MyProfilToggleButton.IsChecked = false;

                FilecabinetToggleButton.IsEnabled = false;
                CategoryToggleButton.IsEnabled = true;
                AuthorsToggleButton.IsEnabled = true;
                MyProfilToggleButton.IsEnabled = true;
            }
        }
    }
}
