using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            InitializeComponent();
            AppConnectClass.DataBase = new LibraryMirzayevaEntities();

            CategoriesListBox.ItemsSource = AppConnectClass.DataBase.CategoryTable.ToList();
        }

        private void CategoriesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CategoryTable ReceiveCategory = (CategoryTable)CategoriesListBox.SelectedItem;
            FrameNavigationClass.BodyFNC.Navigate(new FilecabinetPage(ReceiveCategory));
        }
    }
}
