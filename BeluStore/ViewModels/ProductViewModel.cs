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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;
using System.IO;
using System.Windows;
using Wpf.Ui;

namespace BeluStore.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<Product> products { get; set; }
        public ObservableCollection<Category> categories { get; set; }
        public ObservableCollection<Supplier> suppliers { get; set; }
        private List<Product> _originalProducts { get; set; }
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

        public ProductViewModel(Product product)
        {
            products = new ObservableCollection<Product>();
            categories = new ObservableCollection<Category>();
            suppliers = new ObservableCollection<Supplier>();
            LoadData();
        }

        public void LoadData()
        {
            using (var context = new BeluStoreContext())
            {
                _originalProducts = context.Products.Include(r => r.Category).Include(r => r.Supplier).ToList();
                var categoryList = context.Categories.ToList();
                var supplierList = context.Suppliers.ToList();
                products = new ObservableCollection<Product>(_originalProducts);
                categories = new ObservableCollection<Category>(categoryList);
                suppliers = new ObservableCollection<Supplier>(supplierList);
                OnPropertyChanged();
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

                try
                {
                    using (var context = new BeluStoreContext())
                    {
                        newProduct.Category = context.Categories.Find(newProduct.CategoryId);
                        newProduct.Supplier = context.Suppliers.Find(newProduct.SupplierId);
                        context.Products.Add(newProduct);
                        context.SaveChanges();
                    }

                    // Cập nhật lại danh sách
                    LoadData();
                    OnPropertyChanged(nameof(products));
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Failed to add product: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateProduct(Product product)
        {
            var editWindow = new EditProductWindow(product);
            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                try
                {
                    using (var context = new BeluStoreContext())
                    {
                        // Tìm sản phẩm gốc trong cơ sở dữ liệu
                        var existingProduct = context.Products
                            .Include(p => p.Category)
                            .Include(p => p.Supplier)
                            .FirstOrDefault(p => p.ProductId == product.ProductId);

                        if (existingProduct != null)
                        {
                            // Cập nhật các thuộc tính cơ bản của sản phẩm
                            existingProduct.ProductName = product.ProductName;
                            existingProduct.Price = product.Price;
                            existingProduct.QuantityInStock = product.QuantityInStock;

                            // Cập nhật các thuộc tính liên kết (Category, Supplier)
                            existingProduct.Category = context.Categories.Find(product.CategoryId);
                            existingProduct.Supplier = context.Suppliers.Find(product.SupplierId);

                            context.SaveChanges();

                            // Cập nhật trực tiếp trong danh sách ObservableCollection
                            var observableProduct = products.FirstOrDefault(p => p.ProductId == product.ProductId);
                            if (observableProduct != null)
                            {
                                observableProduct.ProductName = product.ProductName;
                                observableProduct.Price = product.Price;
                                observableProduct.QuantityInStock = product.QuantityInStock;
                                observableProduct.Category = existingProduct.Category;
                                observableProduct.Supplier = existingProduct.Supplier;

                                // Gọi OnPropertyChanged cho từng thuộc tính
                                OnPropertyChanged(nameof(observableProduct.ProductName));
                                OnPropertyChanged(nameof(observableProduct.Price));
                                OnPropertyChanged(nameof(observableProduct.QuantityInStock));
                                OnPropertyChanged(nameof(observableProduct.Category));
                                OnPropertyChanged(nameof(observableProduct.Supplier));
                            }

                            OnPropertyChanged(nameof(products));
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Failed to update product: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void DeleteProduct(Product product)
        {
            try
            {
                using (var context = new BeluStoreContext())
                {
                    var existingProduct = context.Products.Find(product.ProductId);
                    if (existingProduct != null)
                    {
                        // Ask the user for confirmation before deleting (using System.Windows.MessageBox)
                        System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show(
                            $"Are you sure you want to delete {existingProduct.ProductName}?", // Confirmation message
                            "Confirm Delete",
                            System.Windows.MessageBoxButton.YesNo,
                            System.Windows.MessageBoxImage.Warning
                        );

                        if (result == System.Windows.MessageBoxResult.Yes)
                        {
                            context.Products.Remove(existingProduct);
                            context.SaveChanges();

                            // Cập nhật lại danh sách
                            LoadData();
                            OnPropertyChanged(nameof(products)); // Assuming 'products' is your ObservableCollection
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Failed to delete product: {ex.Message}",
                    "Error",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error
                );
            }
        }
    }
}
