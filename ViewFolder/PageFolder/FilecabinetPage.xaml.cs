﻿using MirzaevLibrary.AppDataFolder.ClassFolder;
using MirzaevLibrary.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MirzaevLibrary.ViewFolder.PageFolder
{
    public partial class FilecabinetPage : Page
    {
        public static int CategoryNumber = 0;
        public FilecabinetPage(CategoryTable categoryTable)
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new LibraryMirzayevaEntities();

                if (categoryTable != null)
                {
                    DataContext = categoryTable;
                    CategoryNumber = categoryTable.PersonalNumber_Category;

                    var SelectedCategory = AppConnectClass.DataBase.BookTable.Where(InformationCategory => InformationCategory.pnCategory_Book == CategoryNumber).ToList();
                    BookListBox.ItemsSource = SelectedCategory;
                }
                else
                {
                    var NullCategory = AppConnectClass.DataBase.BookTable.ToList();
                    BookListBox.ItemsSource = NullCategory;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(), 
                    "Ошибка входа",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        private void BookListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
