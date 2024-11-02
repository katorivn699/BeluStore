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
            DataContext = product;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;  // Sets DialogResult to close the dialog with a positive response
            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;  // Sets DialogResult to close the dialog without saving
            Close();
        }
    }
}
