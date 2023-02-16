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

        }
    }
}
