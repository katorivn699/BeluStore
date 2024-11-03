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
                    //CategoryId = int.Parse(CategoryIdTextBox.Text),
                    //SupplierId = int.Parse(SupplierIdTextBox.Text),
                    CategoryId = (int)CategoryComboBox.SelectedValue,
                    SupplierId = (int)SupplierComboBox.SelectedValue,
                    Price = decimal.Parse(PriceTextBox.Text),
                    ProductImage = SaveImageToProject(ImageUrlTextBox.Text), // Save image to project folder
                    QuantityInStock = int.Parse(QuantityInStockTextBox.Text),
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
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
                   //!string.IsNullOrWhiteSpace(CategoryIdTextBox.Text) &&
                   //!string.IsNullOrWhiteSpace(SupplierIdTextBox.Text) &&
                   CategoryComboBox.SelectedValue != null &&
                   SupplierComboBox.SelectedValue != null &&
                   !string.IsNullOrWhiteSpace(PriceTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(ImageUrlTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(QuantityInStockTextBox.Text);
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
