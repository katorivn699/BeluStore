using BeluStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeluStore.ViewModels
{
    public class EditProductViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }

        public EditProductViewModel(Product product)
        {
            Product = product;
            LoadCategories();
            LoadSuppliers();
        }

        private void LoadCategories()
        {
            using (var context = new BeluStoreContext())
            {
                Categories = new ObservableCollection<Category>(context.Categories.ToList());
            }
        }

        private void LoadSuppliers()
        {
            using (var context = new BeluStoreContext())
            {
                Suppliers = new ObservableCollection<Supplier>(context.Suppliers.ToList());
            }
        }
    }
}
