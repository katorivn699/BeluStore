using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using BeluStore.Models;
using BeluStore.Util;
using BeluStore.Views;
using BeluStore.Windows;

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

            if (!IsUserValid())
            {
                MessageBox.Show("Please fill in all required fields with valid data.");
                return;
            }

            using (var context = new BeluStoreContext())
            {

                var existingUser = context.Users.FirstOrDefault(u => u.Username == Username);
                if (existingUser != null)
                {
                    MessageBox.Show("Username already exists. Please choose another.");
                    return;
                }


                if (context.Users.Any(u => u.Email == Email))
                {
                    MessageBox.Show("A user with this email already exists.");
                    return;
                }

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

                var loginWindow = new Login();
                loginWindow.Show();

                var registerWindow = Application.Current.Windows.OfType<Register>().FirstOrDefault();
                registerWindow?.Close();



            }
        }

        private bool IsUserValid()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(FullName) &&
                   !string.IsNullOrWhiteSpace(PhoneNumber) &&
                   !string.IsNullOrWhiteSpace(Address);
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }


        private bool IsValidPassword(string password)
        {

            return password.Length >= 8 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[a-z]") &&
                   Regex.IsMatch(password, @"[0-9]");
        }
    }
}
