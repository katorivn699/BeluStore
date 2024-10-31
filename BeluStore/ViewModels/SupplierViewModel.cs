using BeluStore.Models;
using BeluStore.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void UpdateSupplier(object param)
        {
            if (SelectedSupplier != null && EditedSupplier != null)
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
            }
        }

        public void DeleteSupplier(object param)
        {
            if (SelectedSupplier != null)
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

        public void ClearSupplier(object param)
        {
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
    }
}
