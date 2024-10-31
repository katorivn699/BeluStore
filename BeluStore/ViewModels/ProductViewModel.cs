using BeluStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeluStore.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<Product> products { get; set; }

        public ProductViewModel()
        {
            products = new ObservableCollection<Product>();
            LoadData();
        }

        public void LoadData()
        {
            using (var context = new BeluStoreContext())
            {
                var st = context.Products.ToList();
                products = new ObservableCollection<Product>(st);
            }
        }
    }
}
