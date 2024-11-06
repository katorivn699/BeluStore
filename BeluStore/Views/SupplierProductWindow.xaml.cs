using BeluStore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BeluStore.Views
{
    public partial class SupplierProductWindow
    {
        public string SupplierName { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public SupplierProductWindow(ObservableCollection<Product> products)
        {
            InitializeComponent();

            //DataContext = products;

            Products = products;

            if (products.Count > 0 && products[0].Supplier != null)
            {
                SupplierName = products[0].Supplier.SupplierName;
            }
            else
            {
                SupplierName = "Supplier Product List";
            }

            DataContext = this;
        }
    }
}
