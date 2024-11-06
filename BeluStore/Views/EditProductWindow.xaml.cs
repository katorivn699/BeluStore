using BeluStore.Models;
using BeluStore.ViewModels;
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

namespace BeluStore.Views
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow
    {
        public EditProductWindow(Product product)
        {
            InitializeComponent();
            DataContext = new EditProductViewModel(product);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            // Perform validation before saving
            if (ValidateInputs())
            {
                // If validation passes, set the dialog result and close the window
                DialogResult = true;
                Close();
            }
            else
            {
                // Show validation error message
                System.Windows.MessageBox.Show("Validation Error: \n" +
                                "- Product name must be at least 7 characters long.\n" +
                                "- Price must be greater than 0 and less than 999.\n" +
                                "- Quantity must be greater than 0 and less than 999.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Get the values from the bound Product object
            var product = (DataContext as EditProductViewModel)?.Product;

            // Validate product name
            bool isProductNameValid = !string.IsNullOrWhiteSpace(product?.ProductName) && product.ProductName.Length >= 7;

            // Validate price
            bool isPriceValid = product?.Price > 0 && product.Price < 999;

            // Validate quantity
            bool isQuantityValid = product?.QuantityInStock > 0 && product.QuantityInStock < 999;

            // Return true only if all conditions are met
            return isProductNameValid && isPriceValid && isQuantityValid;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;  // Sets DialogResult to close the dialog without saving
            Close();
        }
    }
}
