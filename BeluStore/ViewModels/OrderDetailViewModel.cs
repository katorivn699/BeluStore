using BeluStore.Models;
using BeluStore.Util;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BeluStore.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        //private OrderDetail selectedOrderDetail;
        //public OrderDetail SelectedOrderDetail
        //{
        //    get { return selectedOrderDetail; }
        //    set
        //    {
        //        if (selectedOrderDetail != value)
        //        {
        //            selectedOrderDetail = value;
        //            EditedOrderDetail = selectedOrderDetail != null ? CloneOrderDetail(selectedOrderDetail) : null;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //private OrderDetail editedOrderDetail;
        //public OrderDetail EditedOrderDetail
        //{
        //    get { return editedOrderDetail; }
        //    set
        //    {
        //        if (editedOrderDetail != value)
        //        {
        //            editedOrderDetail = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public ObservableCollection<OrderDetail> OrderDetails { get; set; }

        public ICommand AddOrderDetailCommand { get; set; }
        public ICommand UpdateOrderDetailCommand { get; set; }
        public ICommand DeleteOrderDetailCommand { get; set; }
        public ICommand ClearOrderDetailCommand { get; set; }
        public ICommand RefreshUnitPriceCommand { get; set; }
        public OrderDetailViewModel()
        {
            LoadOrderDetails();
            //SelectedOrderDetail = new OrderDetail();
            //EditedOrderDetail = new OrderDetail();

            //AddOrderDetailCommand = new RelayCommand(AddOrderDetail);
            //UpdateOrderDetailCommand = new RelayCommand(UpdateOrderDetail);
            //DeleteOrderDetailCommand = new RelayCommand(DeleteOrderDetail);
            //ClearOrderDetailCommand = new RelayCommand(ClearOrderDetail);
            //RefreshOrderDetailsUnitPrice();
        }

        private void LoadOrderDetails()
        {
            using (var context = new BeluStoreContext())
            {
                var orderDetailList = context.OrderDetails.ToList();
                OrderDetails = new ObservableCollection<OrderDetail>(orderDetailList);
            }
        }
        //    private void RefreshOrderDetailsUnitPrice()
        //    {
        //        using (var context = new BeluStoreContext())
        //        {
        //            foreach (var orderDetail in OrderDetails)
        //            {
        //                // Get the latest price from the Product table
        //                var product = context.Products.Find(orderDetail.ProductId);
        //                if (product != null)
        //                {
        //                    // Update UnitPrice in OrderDetail
        //                    orderDetail.UnitPrice = product.Price;
        //                }
        //            }

        //            // Save changes to the database
        //            context.SaveChanges();

        //            // Refresh the UI
        //            OnPropertyChanged(nameof(OrderDetails));
        //        }
        //    }
        //}

        //public void AddOrderDetail(object param)
        //{
        //    if (EditedOrderDetail != null)
        //    {
        //        using (var context = new BeluStoreContext())
        //        {
        //            EditedOrderDetail.OrderDetailId = 0; // Ensure ID is not set manually
        //            context.OrderDetails.Add(EditedOrderDetail);
        //            context.SaveChanges();

        //            OrderDetails.Add(EditedOrderDetail);
        //        }

        //        ClearOrderDetail(null);
        //    }
        //}

        //    public void UpdateOrderDetail(object param)
        //    {
        //        if (SelectedOrderDetail != null && EditedOrderDetail != null)
        //        {
        //            using (var context = new BeluStoreContext())
        //            {
        //                var orderDetailToUpdate = context.OrderDetails.Find(SelectedOrderDetail.OrderDetailId);
        //                if (orderDetailToUpdate != null)
        //                {
        //                    orderDetailToUpdate.Quantity = EditedOrderDetail.Quantity;
        //                    orderDetailToUpdate.UnitPrice = EditedOrderDetail.UnitPrice;

        //                    context.SaveChanges();

        //                    var index = OrderDetails.IndexOf(SelectedOrderDetail);
        //                    OrderDetails[index] = orderDetailToUpdate;
        //                }
        //            }
        //        }
        //    }

        //    public void DeleteOrderDetail(object param)
        //    {
        //        if (SelectedOrderDetail != null)
        //        {
        //            using (var context = new BeluStoreContext())
        //            {
        //                var orderDetailToDelete = context.OrderDetails.Find(SelectedOrderDetail.OrderDetailId);
        //                if (orderDetailToDelete != null)
        //                {
        //                    context.OrderDetails.Remove(orderDetailToDelete);
        //                    context.SaveChanges();

        //                    OrderDetails.Remove(SelectedOrderDetail);
        //                    ClearOrderDetail(null);
        //                }
        //            }
        //        }
        //    }

        //    public void ClearOrderDetail(object param)
        //    {
        //        SelectedOrderDetail = null;
        //        EditedOrderDetail = new OrderDetail();
        //        OnPropertyChanged(nameof(SelectedOrderDetail));
        //        OnPropertyChanged(nameof(EditedOrderDetail));
        //    }

        //    private OrderDetail CloneOrderDetail(OrderDetail orderDetail)
        //    {
        //        return new OrderDetail
        //        {
        //            OrderDetailId = orderDetail.OrderDetailId,
        //            OrderId = orderDetail.OrderId,
        //            ProductId = orderDetail.ProductId,
        //            Quantity = orderDetail.Quantity,
        //            UnitPrice = orderDetail.UnitPrice
        //        };
        //    }
        //}
    }
}