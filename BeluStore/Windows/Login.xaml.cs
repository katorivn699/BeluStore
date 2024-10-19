using BeluStore.ViewModels;
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
using System.Windows.Shapes;
using Wpf.Ui.Appearance;

namespace BeluStore.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(); // Đảm bảo bạn có ViewModel cho đăng nhập
        }

        private void Register_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show(); // Mở cửa sổ đăng ký
        }
    }
}