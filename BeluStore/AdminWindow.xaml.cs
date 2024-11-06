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
using BeluStore.Windows;

namespace BeluStore
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow
    {
        public AdminWindow()
        {
            InitializeComponent();
            LogoutButton.Click += LogoutButton_Click;
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {


            var loginWindow = new Login();
            loginWindow.Show();

            this.Close();
        }


    }
}