using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using MirzaevLibrary.ViewFolder.WindowsFolder;
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
    public partial class ProfilePage : Page
    {
        private UserClass userClass;
        public ProfilePage()
        {
            InitializeComponent(); 
            userClass = new UserClass();
            AppConnectClass.DataBase = new LibraryMirzayevaEntities();
            HistoryBookListBox.ItemsSource = AppConnectClass.DataBase.BookTable.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                SurnameTextBox.Text = UserClass.SurnameUser;
                NameTextBox.Text = UserClass.NameUser;
                MiddlenameTextBox.Text = UserClass.MiddlenameUser;
            }
        }
    }
}
