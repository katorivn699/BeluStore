using System;
using System.Linq;
using System.Windows;
using BeluStore.Models;
using BeluStore.Util;

namespace BeluStore.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username;
        private string _email;
        private string _password;
        private string _fullName;
        private string _phoneNumber;
        private string _address;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
        }

        private bool CanExecuteRegister(object? parameter)
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(FullName) &&
                   !string.IsNullOrEmpty(PhoneNumber) &&
                   !string.IsNullOrEmpty(Address);
        }

        private void ExecuteRegister(object? parameter)
        {
            using (var context = new BeluStoreContext())
            {
                // Kiểm tra xem username có tồn tại không
                var existingUser = context.Users.FirstOrDefault(u => u.Username == Username);
                if (existingUser != null)
                {
                    MessageBox.Show("Username already exists. Please choose another.");
                    return;
                }

                // Tạo mới người dùng và lưu vào cơ sở dữ liệu
                var newUser = new User
                {
                    Username = Username,
                    Email = Email,
                    Password = Password, 
                    FullName = FullName,
                    PhoneNumber = PhoneNumber,
                    Address = Address,
                    Role = "customer" 
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Registration successful!");
            }
        }
    }
}
