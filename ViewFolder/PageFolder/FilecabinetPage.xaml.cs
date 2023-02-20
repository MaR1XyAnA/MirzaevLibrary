using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class FilecabinetPage : Page
    {
        public static int TypeId = 0;
        public FilecabinetPage(CategoryTable categoryTable)
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();
                if (categoryTable != null)
                {
                    DataContext = categoryTable;
                    TypeId = categoryTable.PersonalNumberCategory;
                    BookListBox.ItemsSource = AppConnectClass.DataBase.BookTable.Where(data => data.pnCategory == TypeId).ToList();
                }
                else
                {
                    BookListBox.ItemsSource = AppConnectClass.DataBase.BookTable.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BookListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
