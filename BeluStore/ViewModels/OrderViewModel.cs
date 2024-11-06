using BeluStore.Models;
using BeluStore.Util;
using BeluStore.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BeluStore.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (selectedOrder != value)
                {
                    selectedOrder = value;
                    EditedOrder = selectedOrder != null ? CloneOrder(selectedOrder) : null;
                    OnPropertyChanged();
                }
            }
        }

        private Order editedOrder;
        public Order EditedOrder
        {
            get { return editedOrder; }
            set
            {
                if (editedOrder != value)
                {
                    editedOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Order> orders { get; set; }

        public ICommand AddOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }
        public ICommand ClearOrderCommand { get; set; }
        public ICommand ViewOrderDetailCommand { get; set; }  // Added ViewOrderDetailCommand

        public OrderViewModel()
        {
            LoadOrder();
            SelectedOrder = new Order();
            EditedOrder = new Order();

            AddOrderCommand = new RelayCommand(AddOrder);
            DeleteOrderCommand = new RelayCommand(DeleteOrder);
            UpdateOrderCommand = new RelayCommand(UpdateOrder);
            ClearOrderCommand = new RelayCommand(ClearOrder);
            ViewOrderDetailCommand = new RelayCommand(ViewOrderDetail);  // Initialize the command
        }

        private void LoadOrder()
        {
            using (var context = new BeluStoreContext())
            {
                var OrderList = context.Orders.ToList();
                orders = new ObservableCollection<Order>(OrderList);
            }
        }

        public void AddOrder(object param)
        {
            if (EditedOrder != null)
            {
                using (var context = new BeluStoreContext())
                {
                    EditedOrder.OrderId = 0;
                    context.Orders.Add(EditedOrder);
                    context.SaveChanges();

                    orders.Add(EditedOrder);
                }

                ClearOrder(null);
            }
        }

        public void UpdateOrder(object param)
        {
            if (SelectedOrder != null && EditedOrder != null)
            {
                using (var context = new BeluStoreContext())
                {
                    var orderToUpdate = context.Orders.Find(SelectedOrder.OrderId);
                    if (orderToUpdate != null)
                    {
                        orderToUpdate.OrderId = EditedOrder.OrderId;
                        orderToUpdate.UserId = EditedOrder.UserId;
                        orderToUpdate.OrderDate = EditedOrder.OrderDate;
                        orderToUpdate.TotalAmount = EditedOrder.TotalAmount;
                        orderToUpdate.Status = EditedOrder.Status;
                        orderToUpdate.PaymentMethod = EditedOrder.PaymentMethod;

                        context.SaveChanges();

                        var index = orders.IndexOf(SelectedOrder);
                        orders[index] = orderToUpdate;
                    }
                }
            }
        }

        public void DeleteOrder(object param)
        {
            if (SelectedOrder != null)
            {
                using (var context = new BeluStoreContext())
                {
                    var orderToDelete = context.Orders.Find(SelectedOrder.OrderId);
                    if (orderToDelete != null)
                    {
                        context.Orders.Remove(orderToDelete);
                        context.SaveChanges();

                        orders.Remove(SelectedOrder);
                        ClearOrder(null);
                    }
                }
            }
        }

        public void ClearOrder(object param)
        {
            SelectedOrder = null;
            EditedOrder = new Order();
            OnPropertyChanged(nameof(SelectedOrder));
            OnPropertyChanged(nameof(EditedOrder));
        }

        private Order CloneOrder(Order order)
        {
            return new Order
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
            };
        }

        // Method for viewing order details
        public void ViewOrderDetail(object param)
        {
            if (param is int orderId)
            {
                // Find the selected order and load its details
                var selectedOrder = orders.FirstOrDefault(o => o.OrderId == orderId);

                if (selectedOrder != null)
                {
                    using (var context = new BeluStoreContext())
                    {
                        // Load order details based on the OrderId
                        var orderDetails = context.OrderDetails
                                                  .Where(od => od.OrderId == orderId)
                                                  .ToList();
                        selectedOrder.OrderDetails = new ObservableCollection<OrderDetail>(orderDetails);
                    }

                    // Open the detail window with the loaded order and order details
                    var orderDetailWindow = new OrderDetailWindow
                    {
                        DataContext = selectedOrder  // Set DataContext to the order with loaded OrderDetails
                    };
                    orderDetailWindow.ShowDialog();
                }
            }
        }

    }
}
