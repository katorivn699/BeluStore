using BeluStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using System.Diagnostics;

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
                    CategoryId = (int)CategoryComboBox.SelectedValue,
                    SupplierId = (int)SupplierComboBox.SelectedValue,
                    Price = decimal.Parse(PriceTextBox.Text),
                    ProductImage = SaveImageToProject(ImageUrlTextBox.Text),
                    QuantityInStock = int.Parse(QuantityInStockTextBox.Text),
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                };

                DialogResult = true; // Set the dialog result to true
                Close(); // Close the window
            }
            // Nếu ValidateInputs() trả về false, không làm gì cả và để thông báo lỗi hiển thị ra ngoài.
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the window without adding a product
        }

        private bool ValidateInputs()
        {
            // Reset error messages
            var errorMessages = new List<string>();

            // Ensure required fields are populated and valid
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) || ProductNameTextBox.Text.Trim().Length < 8)
            {
                errorMessages.Add("Product Name must be at least 8 characters long.");
            }

            if (CategoryComboBox.SelectedValue == null)
            {
                errorMessages.Add("Please select a Category.");
            }

            if (SupplierComboBox.SelectedValue == null)
            {
                errorMessages.Add("Please select a Supplier.");
            }

            if (string.IsNullOrWhiteSpace(PriceTextBox.Text) || !decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0 || price >= 999)
            {
                errorMessages.Add("Price must be a valid number greater than 0 and less than 999.");
            }

            if (string.IsNullOrWhiteSpace(ImageUrlTextBox.Text))
            {
                errorMessages.Add("Please enter an Image URL.");
            }

            if (string.IsNullOrWhiteSpace(QuantityInStockTextBox.Text) || !int.TryParse(QuantityInStockTextBox.Text, out int quantity) || quantity <= 0 || quantity >= 999)
            {
                errorMessages.Add("Quantity In Stock must be a valid integer greater than 0 and less than 999.");
            }

            // Show error messages if any
            if (errorMessages.Any())
            {
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Validation Errors", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }

            // All validations passed
            return true;
        }


        private string SaveImageToProject(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                System.Windows.MessageBox.Show("Image file does not exist.", "Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagesDirectory = System.IO.Path.Combine(projectDirectory, "Images");

            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            string fileName = System.IO.Path.GetFileName(imagePath);
            string destinationPath = System.IO.Path.Combine(imagesDirectory, fileName);

            try
            {
                File.Copy(imagePath, destinationPath, true);
                return destinationPath; // Return the absolute path
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error saving image: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }



        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog from Microsoft.Win32 namespace, not System.Windows.Forms
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            // ShowDialog() returns a nullable bool (true if the user clicked OK)
            if (openFileDialog.ShowDialog() == true)
            {
                // Set the selected image path in the text box
                ImageUrlTextBox.Text = openFileDialog.FileName;
            }
        }

    }
}
