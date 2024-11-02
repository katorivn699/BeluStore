using BeluStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace BeluStore.Views
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow
    {
        public Product NewProduct { get; private set; }
        public AddProductWindow()
        {
            InitializeComponent();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                NewProduct = new Product
                {
                    ProductName = ProductNameTextBox.Text,
                    CategoryId = int.Parse(CategoryIdTextBox.Text),
                    SupplierId = int.Parse(SupplierIdTextBox.Text),
                    Price = decimal.Parse(PriceTextBox.Text),
                    ProductImage = ImageUrlTextBox.Text,
                    QuantityInStock = int.Parse(QuantityInStockTextBox.Text)
                };

                DialogResult = true; // Set the dialog result to true
                Close(); // Close the window
            }
            else
            {
                System.Windows.MessageBox.Show("Please fill in all fields correctly.", "Validation Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the window without adding a product
        }

        private bool ValidateInputs()
        {
            // Simple validation for required fields
            return !string.IsNullOrWhiteSpace(ProductNameTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(CategoryIdTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(SupplierIdTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(PriceTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(ImageUrlTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(QuantityInStockTextBox.Text);
        }
    }
}
