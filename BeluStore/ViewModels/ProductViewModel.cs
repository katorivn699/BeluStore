using BeluStore.Models;
using BeluStore.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace BeluStore.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<Product> products { get; set; }
        private List<Product> _originalProducts; // Store original products
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    FilterProducts(); // Filter when the search query changes
                }
            }
        }

        public ProductViewModel()
        {
            products = new ObservableCollection<Product>();
            _originalProducts = new List<Product>(); // Initialize original products list
            LoadData();
            AddCommand = new RelayCommand<Product>(AddProduct);
            UpdateCommand = new RelayCommand<Product>(UpdateProduct);
            DeleteCommand = new RelayCommand<Product>(DeleteProduct);
        }

        public void LoadData()
        {
            using (var context = new BeluStoreContext())
            {
                _originalProducts = context.Products.Include(r => r.Category).Include(r => r.Supplier).ToList(); // Load original products
                products = new ObservableCollection<Product>(_originalProducts); // Initialize Products with original list
            }
        }

        private void FilterProducts()
        {
            products.Clear(); // Clear the current products in the view

            // Filter the original product list based on the SearchQuery
            var filtered = string.IsNullOrWhiteSpace(SearchQuery)
                ? _originalProducts
                : _originalProducts.Where(p => p.ProductName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            // Add the filtered products to the ObservableCollection
            foreach (var product in filtered)
            {
                products.Add(product);
            }
        }

        private void AddProduct(object obj)
        {
            var addProductWindow = new AddProductWindow();
            bool? result = addProductWindow.ShowDialog();

            if (result == true)
            {
                var newProduct = addProductWindow.NewProduct;

                using (var context = new BeluStoreContext())
                {
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                }

                // Update the original list and ObservableCollection
                _originalProducts.Add(newProduct);
                products.Add(newProduct);
                LoadData();
                OnPropertyChanged();
            }
        }

        private void UpdateProduct(Product product)
        {
            var editWindow = new EditProductWindow(product);
            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                using (var context = new BeluStoreContext())
                {
                    context.Products.Update(product);
                    context.SaveChanges();

                    // Refresh the products collection
                    LoadData(); // Reload the data to reflect changes
                }
            }
        }

        private void DeleteProduct(Product product)
        {
            using (var context = new BeluStoreContext())
            {
                context.Products.Remove(product);
                context.SaveChanges();
                products.Remove(product);
                _originalProducts.Remove(product); // Ensure original list is updated
                OnPropertyChanged();
            }
        }
    }
}
