using BeluStore.Models;
using BeluStore.Util;
using BeluStore.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BeluStore.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private Supplier selectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                if (selectedSupplier != value)
                {
                    selectedSupplier = value;
                    EditedSupplier = selectedSupplier != null ? CloneSupplier(selectedSupplier) : null;
                    OnPropertyChanged();
                }
            }
        }

        private Supplier editedSupplier;
        public Supplier EditedSupplier
        {
            get { return editedSupplier; }
            set
            {
                if (editedSupplier != value)
                {
                    editedSupplier = value;
                    OnPropertyChanged();
                }
            }
        }



        private string searchKeyword;
        public string SearchKeyword
        {
            get { return searchKeyword; }
            set
            {
                if (searchKeyword != value)
                {
                    searchKeyword = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchSupplierCommand { get; set; }

        public ObservableCollection<Supplier> Suppliers { get; set; }

        public ICommand AddSupplierCommand { get; set; }
        public ICommand DeleteSupplierCommand { get; set; }
        public ICommand UpdateSupplierCommand { get; set; }
        public ICommand ClearSupplierCommand { get; set; }

        public SupplierViewModel()
        {
            LoadSuppliers();
            SelectedSupplier = new Supplier();
            EditedSupplier = new Supplier();

            AddSupplierCommand = new RelayCommand(AddSupplier);
            DeleteSupplierCommand = new RelayCommand(DeleteSupplier);
            UpdateSupplierCommand = new RelayCommand(UpdateSupplier);
            SearchSupplierCommand = new RelayCommand(SearchSupplier);
            ClearSupplierCommand = new RelayCommand(ClearSupplier);
        }

        private void LoadSuppliers()
        {
            using (var context = new BeluStoreContext())
            {
                var supplierList = context.Suppliers.ToList();
                Suppliers = new ObservableCollection<Supplier>(supplierList);
            }
        }

        public void AddSupplier(object param)
        {


            if (EditedSupplier != null)
            {
                bool check = ValidateInputs();
                if (check)
                {
                    using (var context = new BeluStoreContext())
                    {
                        EditedSupplier.SupplierId = 0;
                        context.Suppliers.Add(EditedSupplier);
                        context.SaveChanges();
                        Suppliers.Add(EditedSupplier);
                    }

                    ClearSupplier(null);
                }
            }


        }

        public void UpdateSupplier(object param)
        {
            if (SelectedSupplier != null && EditedSupplier != null)
            {
                bool check = ValidateInputs();
                if (check)
                {
                    using (var context = new BeluStoreContext())
                    {
                        var supplierToUpdate = context.Suppliers.Find(SelectedSupplier.SupplierId);
                        if (supplierToUpdate != null)
                        {
                            supplierToUpdate.SupplierName = EditedSupplier.SupplierName;
                            supplierToUpdate.SupplierPhone = EditedSupplier.SupplierPhone;
                            supplierToUpdate.SupplierDescription = EditedSupplier.SupplierDescription;
                            supplierToUpdate.Address = EditedSupplier.Address;

                            context.SaveChanges();

                            var index = Suppliers.IndexOf(SelectedSupplier);
                            Suppliers[index] = supplierToUpdate;
                        }
                    }
                    ClearSupplier(null);

                }
            }
        }

        public void DeleteSupplier(object param)
        {
            if (SelectedSupplier != null)
            {
   
                var confirmationResult = MessageBox.Show(
                    "Are you sure you want to delete this supplier?", 
                    "Confirm Deletion",
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning 
                );

                if (confirmationResult == MessageBoxResult.Yes)
                {
                    using (var context = new BeluStoreContext())
                    {
                        var supplierToDelete = context.Suppliers.Find(SelectedSupplier.SupplierId);
                        if (supplierToDelete != null)
                        {
                            context.Suppliers.Remove(supplierToDelete);
                            context.SaveChanges();

                            Suppliers.Remove(SelectedSupplier);
                            ClearSupplier(null);
                        }
                    }
                }
 
            }
        }


        public void ClearSupplier(object param)
        {
            SearchKeyword = "";
            SearchSupplier(SearchKeyword);
            SelectedSupplier = null;
            EditedSupplier = new Supplier();
            OnPropertyChanged(nameof(SelectedSupplier));
            OnPropertyChanged(nameof(EditedSupplier));

        }

        private Supplier CloneSupplier(Supplier supplier)
        {
            return new Supplier
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                SupplierPhone = supplier.SupplierPhone,
                SupplierDescription = supplier.SupplierDescription,
                Address = supplier.Address
            };
        }

        private void SearchSupplier(object param)
        {
            using (var context = new BeluStoreContext())
            {
                var filteredSuppliers = context.Suppliers
                    .Where(s => s.SupplierName.Contains(SearchKeyword) || s.Address.Contains(SearchKeyword) || s.SupplierPhone.Contains(SearchKeyword) || s.SupplierDescription.Contains(SearchKeyword))
                    .ToList();
                Suppliers = new ObservableCollection<Supplier>(filteredSuppliers);
                OnPropertyChanged(nameof(Suppliers));
            }
        }

        private bool ValidateInputs()
        {
            var errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(EditedSupplier.SupplierName) || EditedSupplier.SupplierName.Trim().Length < 5)
            {
                errorMessages.Add("Supplier Name must be at least 5 characters long.");
            }

            if (!Regex.IsMatch(EditedSupplier.SupplierPhone, @"^(\+?\d{10,})$"))
            {
                errorMessages.Add("Supplier Phone must be at least 10 digits long and contain only numbers (optionally starting with '+').");
            }

            if (string.IsNullOrWhiteSpace(EditedSupplier.SupplierDescription))
            {
                errorMessages.Add("Supplier Description cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(EditedSupplier.Address))
            {
                errorMessages.Add("Address cannot be empty.");
            }

            // Show error messages if any
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // All validations passed
            return true;
        }
    }
}
