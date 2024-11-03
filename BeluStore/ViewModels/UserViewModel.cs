using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BeluStore.Models;
using BeluStore.Util;

namespace BeluStore.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                if (_selectedUser != null)
                {
                    NewUser.Username = _selectedUser.Username;
                    NewUser.Email = _selectedUser.Email;
                    NewUser.Role = _selectedUser.Role;
                    NewUser.Password = _selectedUser.Password;
                    NewUser.FullName = _selectedUser.FullName;
                    NewUser.PhoneNumber = _selectedUser.PhoneNumber;
                    NewUser.Address = _selectedUser.Address;
                    OnPropertyChanged(nameof(NewUser));
                }
            }
        }

        private User _newUser;
        public User NewUser
        {
            get { return _newUser; }
            set
            {
                _newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<string> Roles { get; set; } // New property for roles
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ClearCommand { get; }

        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            Roles = new ObservableCollection<string> { "customer", "manager" }; // Populate roles
            NewUser = new User();
            LoadUsers();
            AddCommand = new RelayCommand(AddUser);
            RemoveCommand = new RelayCommand(RemoveUser);
            UpdateCommand = new RelayCommand(UpdateUser);
            ClearCommand = new RelayCommand(ClearFields);
        }

        private void LoadUsers()
        {
            using (var context = new BeluStoreContext())
            {
                var users = context.Users.ToList();
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
        }

        private void AddUser(object parameter)
        {
            if (!IsUserValid(NewUser))
            {
                MessageBox.Show("Please fill in all required fields with valid data.");
                return;
            }

            try
            {
                using (var context = new BeluStoreContext())
                {
                    if (context.Users.Any(u => u.Email == NewUser.Email))
                    {
                        MessageBox.Show("A user with this email already exists.");
                        return;
                    }

                    NewUser.UserId = 0;
                    context.Users.Add(NewUser);
                    context.SaveChanges();

                    Users.Add(NewUser);  // Thêm vào ObservableCollection ngay sau khi lưu
                    OnPropertyChanged(nameof(Users)); // Đảm bảo cập nhật UI
                    ClearFields(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private bool IsUserValid(User user)
        {
            if (user == null) return false;
            return !string.IsNullOrWhiteSpace(user.Username) &&
                   IsValidEmail(user.Email) &&
                   !string.IsNullOrWhiteSpace(user.PhoneNumber) &&
                   !string.IsNullOrWhiteSpace(user.Address) &&
                   !string.IsNullOrWhiteSpace(user.Role);
        }

        // Phương thức kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }


        private void RemoveUser(object parameter)
        {
            if (SelectedUser != null)
            {
                using (var context = new BeluStoreContext())
                {
                    context.Users.Remove(SelectedUser);
                    context.SaveChanges();
                    Users.Remove(SelectedUser);
                    ClearFields(null);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void UpdateUser(object parameter)
        {
            if (SelectedUser != null)
            {
                using (var context = new BeluStoreContext())
                {
                    var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == SelectedUser.UserId);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Username = NewUser.Username;
                        userToUpdate.Email = NewUser.Email;
                        userToUpdate.Role = NewUser.Role;
                        userToUpdate.FullName = NewUser.FullName;
                        userToUpdate.PhoneNumber = NewUser.PhoneNumber;
                        userToUpdate.Address = NewUser.Address;

                        context.SaveChanges();

                        int index = Users.IndexOf(SelectedUser);
                        Users[index] = userToUpdate;
                        OnPropertyChanged(nameof(Users));
                        LoadUsers();
                        ClearFields(null);
                    }
                    else
                    {
                        MessageBox.Show("User not found in the database.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        public void ClearFields(object parameter)
        {
            SelectedUser = null;
            NewUser = new User();
            OnPropertyChanged(nameof(SelectedUser));
            OnPropertyChanged(nameof(NewUser));
        }
    }
}
