   M S B A M L   `   `   `  ����D  @BeluStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BeluStore.ViewsBeluStore.Views  5      ;"clr-namespace:BeluStore.ViewModelsBeluStore.ViewModels  5      !    BeluStore.Views.ProductPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      )localclr-namespace:BeluStore.Views  5      +vm"clr-namespace:BeluStore.ViewModels  5        N� Title$  ProductPage��5       � DataContext 5      +   %BeluStore.ViewModels.ProductViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $
��Left=�6;   $��10q�6V   .+�5      �� $��0,0,10,0q�64   ��6   �� *
AddCommand+.��5      �� $ 
Horizontal=�6   .+�5         Wpf.Ui.Controls.SymbolIcon     Symbol$ AddCircle24=�6$   �� 5      $��Add new product��6    $��5,0,0,0q�67   5      5      �� 5      $��Center=�6   
�������$��0,0,10,0q�6F   Search by Name:6i   �� 5      $	��200��6   $��40��6"   ��5      �� *SearchQuery+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��Hot Reload:��6   
�������$��Center=�6>   $��10,0,0,0q�6Y   �� 5       $��3,0,0,0q�6b   ��6   '  !Wpf.Ui.Markup.SymbolIconExtension  *ArrowClockwise24+��6@   �� *ReloadCommand+6b   5!      �� 5"   
   $��1��6   .+�5#      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��6      CanUserAddRows$ False��6a   $��Center=�6x   ��63   �� *
products+ �� Mode$ TwoWay=�	   Columns	 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *	ProductId+  5&      $
 Product Name��6)   $ * 6?    6I   �� *ProductName+  5'      $
 Category��6)   $ * 6;    6E   �� *Category.CategoryName+  5(      $
 Supplier��6)   $ * 6;    6E   �� *Supplier.SupplierName+  5)      $
 Price��6)   $ * 68    6B   �� *Price+4	  .System.Windows.Controls.DataGridTemplateColumn	  5+      $
 Image��6-   $ * 6<    	  CellTemplate 5,      �� 5-      .R�5.   "   �� $��50��6N   $��50��6Y   $��UniformToFill=�6e   ��6(   �� *ProductImageSource+6e   5/      51        52      $
 Quantity��6)   $ * 6;    6F   �� *QuantityInStock+	  53      $
 Actions��6-   $ * 6>    54      �� 55      .R�56   "   �� $ 
Horizontal=�6-   $��3q�6F   .+�57   &   �� $	��5,0q�5:   !   ��57   -     *Edit16+��58   !   �� *DataContext.UpdateCommand+ �� RelativeSource ��  �� AncestorType Page��59   !   �� 5:   !   �� 5;   &   $	��5,0q�5>   !   ��5;   -     *
delete16+��5<   !   �� *DataContext.DeleteCommand+ ��  Page��5=   !   �� 5>   !   5?   #   5@      5B      5D      5E      5F      5G      