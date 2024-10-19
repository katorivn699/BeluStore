using System.Linq;
using System.Windows;
using BeluStore.Models;
using BeluStore.Util;

namespace BeluStore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object? parameter)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin(object? parameter)
        {
            var user = GetUser();

            if (user != null)
            {
                if (user.Role == "manager") 
                {
                    AdminWindow adminWindow = new AdminWindow();
                    Application.Current.MainWindow = adminWindow;
                    adminWindow.Show();
                }
                else if (user.Role == "customer") 
                {
                    MainWindow mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();
                }

                if (parameter is Window loginWindow)
                {
                    loginWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }

        private User GetUser()
        {
            using (var context = new BeluStoreContext())
            {
                return context.Users
                    .FirstOrDefault(u => u.Username == Username && u.Password == Password);
            }
        }
    }
}
