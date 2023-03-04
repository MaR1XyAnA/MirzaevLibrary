using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    BigDate();
                }
                else
                {
                    BigDate2();
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

        private async void BigDate()
        {
            AnimLoadingStart();
            var GetCategory = await AppConnectClass.DataBase.BookTable.Where(data => data.pnCategory == TypeId).ToListAsync();
            BookListBox.ItemsSource = GetCategory;
            AnimLoadingEnd();
        }
        private async void BigDate2()
        {
            AnimLoadingStart();
            var GetCategory = await AppConnectClass.DataBase.BookTable.ToListAsync();
            BookListBox.ItemsSource = GetCategory;
            AnimLoadingEnd();
        }

        private void AnimLoadingStart() { LoadingApplicationProgressBar.Visibility = Visibility.Visible; LoadingApplicationProgressBar.IsIndeterminate = true; } // Запуск анимации загрузки
        private void AnimLoadingEnd() { LoadingApplicationProgressBar.Visibility = Visibility.Collapsed; LoadingApplicationProgressBar.IsIndeterminate = false; } // Остановка анимации загрузки
    }
}
