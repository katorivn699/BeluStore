   M S B A M L   `   `   `  ����D  @BeluStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BeluStore.ViewsBeluStore.Views  5      ;"clr-namespace:BeluStore.ViewModelsBeluStore.ViewModels  5      %    BeluStore.Views.OrderDetailPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:BeluStore.Views     5      1vm"clr-namespace:BeluStore.ViewModels     5        N� Title$  OrderDetailPage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $��4* �6   �� 5      $��6* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��6* �6   .+�5   
   &   System.Windows.Controls.DataGrid  $��0��6   $��0��6    $��2��60      AutoGenerateColumns$ False��5         CanUserAddRows$ False��5         
IsReadOnly$
 True��5      ��5      �� *OrderDetails+ �� SelectedItem 5      �� *SelectedOrderDetail+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ OrderDetailId��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 1* 6^   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6<   �� *OrderDetailId+6^     5!      $ OrderId��6%   $	 1* 6R   
 66   �� *	OrderId+6R     5"      $ 	ProductId��6%   $	 1* 6V   
 68   �� *	ProductId+6V     5#      $ Quantity��6%   $	 1* 6T   
 67   �� *
Quantity+6T     5$      $ 	UnitPrice��6%   $	 1* 6V   
 68   �� *	UnitPrice+6V   5&      5(      5*      