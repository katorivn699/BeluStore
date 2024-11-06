using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BeluStore.Models;
using BeluStore.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                    NewUser.Status = _selectedUser.Status;
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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    ExecuteSearch(null);
                }
            }
        }

        private ObservableCollection<User> _allUsers;
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<string> Roles { get; set; }
        public ObservableCollection<string> Statuses { get; set; }
        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ClearCommand { get; }

        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            _allUsers = new ObservableCollection<User>();
            Roles = new ObservableCollection<string> { "customer", "manager" };
            Statuses = new ObservableCollection<string> { "Active", "Inactive" };
            NewUser = new User();
            LoadUsers();
            AddCommand = new RelayCommand(AddUser);
            RemoveCommand = new RelayCommand(RemoveUser);
            UpdateCommand = new RelayCommand(UpdateUser);
            ClearCommand = new RelayCommand(ClearFields);
            SearchCommand = new RelayCommand(ExecuteSearch);
        }

        private void LoadUsers()
        {
            using (var context = new BeluStoreContext())
            {
                var users = context.Users.ToList();
                Users.Clear();
                _allUsers.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                    _allUsers.Add(user);
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

                    Users.Add(NewUser);
                    OnPropertyChanged(nameof(Users));
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
                   !string.IsNullOrWhiteSpace(user.Email) &&
                   !string.IsNullOrWhiteSpace(user.PhoneNumber) &&
                   !string.IsNullOrWhiteSpace(user.Address) &&
                   !string.IsNullOrWhiteSpace(user.Role);
        }


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
                        userToUpdate.Status = NewUser.Status;
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

        private void ExecuteSearch(object parameter)
        {

            Users.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {

                foreach (var user in _allUsers)
                {
                    Users.Add(user);
                }
            }
            else
            {

                var filteredUsers = _allUsers.Where(u =>
                    (u.Username?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.Email?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.FullName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.Role?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.Status?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.PhoneNumber?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.Address?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                );


                foreach (var user in filteredUsers)
                {
                    Users.Add(user);
                }
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
