using BeluStore.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Appearance;

namespace BeluStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationThemeManager.Apply(this);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Tạo một cửa sổ Login mới
            var loginWindow = new Login();
            loginWindow.Show();

            // Đóng cửa sổ hiện tại (MainWindow)
            this.Close();
        }

    }
}