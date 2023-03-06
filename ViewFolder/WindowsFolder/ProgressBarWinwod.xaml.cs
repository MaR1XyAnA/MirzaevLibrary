using System.Windows;

namespace MirzaevLibrary.ViewFolder.WindowsFolder
{
    public partial class ProgressBarWinwod : Window
    {
        public ProgressBarWinwod()
        {
            InitializeComponent();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible) 
            {
                AppDownloadProgressProgressBar.IsIndeterminate = true;
            }
            else
            {
                AppDownloadProgressProgressBar.IsIndeterminate = false;
            }
        }
    }
}
